using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using OFX.Application.Dto;
using OFX.Application.Dto.Account;
using OFX.Application.Dto.Status;
using OFX.Application.Dto.Transaction;
using OFX.Application.Services.Interfaces;
using OFX.Application.Services.Interfaces.Account;
using OFX.Application.Services.Interfaces.Statement;
using OFX.Application.Services.Interfaces.Status;
using OFX.Application.Services.Interfaces.Transaction;
using OFX.Domain.Enums;

namespace OFX.Application.Util
{
    public class ParserService : IParserService
    {
        private readonly IStatementService _statementService;
        private readonly IStatusService _statusService;
        private readonly IAccountService _accountService;
        private readonly ITransactionService _transactionService;
        private readonly IStatementTransactionService _statementTransactionService;

        public ParserService(IStatementService statementService,
                             IStatusService statusService,
                             IAccountService accountService,
                             ITransactionService transactionService,
                             IStatementTransactionService statementTransactionService)
        {
            _statementService = statementService ?? throw new ArgumentNullException(nameof(statementService));
            _statusService = statusService ?? throw new ArgumentNullException(nameof(statusService));
            _accountService = accountService ?? throw new ArgumentNullException(nameof(accountService));
            _transactionService = transactionService ?? throw new ArgumentNullException(nameof(transactionService));
            _statementTransactionService = statementTransactionService ?? throw new ArgumentNullException(nameof(statementTransactionService));
        }

        /// <summary>
        /// Fix uploaded ofx files and parse them to xml files
        /// For reading and persistence
        /// </summary>
        /// <param name="path">Upload path</param>
        public async Task Parse(string path)
        {
            string[] readText;
            string tagName, tagValue;

            int day, month, year;
            string time = string.Empty;

            foreach (var file in Directory.GetFiles(path, "*.ofx"))
            {
                var fi = new FileInfo(file);

                var fileName = fi.Name.Substring(0, fi.Name.IndexOf("."));
                var outputFile = $"{path}\\{fileName}.xml";

                if (File.Exists(outputFile))
                    File.Delete(outputFile);

                try
                {
                    readText = File.ReadAllLines(file);

                    // Create a new file     
                    using (StreamWriter sw = File.CreateText(outputFile))
                    {
                        for (int i = 0; i < readText.Length; i++)
                        {
                            if (!string.IsNullOrEmpty(readText[i]) && readText[i].StartsWith("<"))
                            {
                                if (readText[i].IndexOf("</") >= 0)
                                    tagName = readText[i].Substring((readText[i].IndexOf("<") + 2), (readText[i].IndexOf(">") - 2));
                                else
                                    tagName = readText[i].Substring(readText[i].IndexOf("<") + 1, (readText[i].LastIndexOf(">") - 1));

                                tagValue = readText[i].Substring(readText[i].LastIndexOf(">") + 1);

                                var hasClosedTag = readText.Where(l => l.Equals($"</{tagName}>")).FirstOrDefault();

                                if (tagName.ToLower().IndexOf("dt") >= 0)
                                {
                                    DateTime dateTime = new DateTime();
                                    var date = tagValue.Substring(0, tagValue.IndexOf("["));

                                    day = Convert.ToInt32(date.Substring(6, 2));
                                    month = Convert.ToInt32(date.Substring(4, 2));
                                    year = Convert.ToInt32(date.Substring(0, 4));
                                    time = date.Substring(8);

                                    if (date.Length < 14)
                                        time = time.PadLeft(time.Length + 1, '0');

                                    if (Util.isValidDate(ref day, month, year) &&
                                        DateTime.TryParseExact(string.Concat(new string[] { year.ToString(), month.ToString().PadLeft(2, '0'), day.ToString().PadLeft(2, '0'), time }),
                                        "yyyyMMddHHmmss",
                                        CultureInfo.InvariantCulture,
                                        DateTimeStyles.None,
                                        out dateTime))
                                    {
                                        tagValue = dateTime.ToString();
                                    }
                                }

                                if (string.IsNullOrEmpty(hasClosedTag) && !string.IsNullOrEmpty(tagValue))
                                    sw.WriteLine(string.Format("<{0}>{1}</{2}>", tagName, tagValue, tagName).Trim());
                                else
                                    sw.WriteLine(readText[i].Trim());
                            }
                        }
                    }

                    await PersistData(outputFile);

                    fi.Delete();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        /// <summary>
        /// Persist data from Xml file into database
        /// </summary>
        /// <param name="file">xml file</param>
        private async Task PersistData(string file)
        {
            XmlDocument doc = new XmlDocument();

            StatusDtoCreateResult statusDtoCreateResult = null;
            AccountDtoCreateResult accountDtoCreateResult = null;

            StatementDto statementDto = null;
            TransactionDto transactionDto = null;

            try
            {
                doc.Load(file);

                var stmtTrnRs = doc.SelectSingleNode("//STMTTRNRS");
                var status = stmtTrnRs.SelectSingleNode("//STATUS");
                var stmt = stmtTrnRs.SelectSingleNode("//STMTRS");

                var statusDto = new StatusDto
                {
                    Code = Convert.ToInt32(status["CODE"].InnerText),
                    Severity = (SeverityType)Enum.Parse(typeof(SeverityType), status["SEVERITY"].InnerText)
                };

                statusDtoCreateResult = await _statusService.Post(statusDto);

                var bankAccount = stmt.SelectSingleNode("//BANKACCTFROM");

                var accountDto = new AccountCreateDto
                {
                    BankId = bankAccount["BANKID"].InnerText,
                    AccountId = bankAccount["ACCTID"].InnerText,
                    AccountType = (AccountType)Enum.Parse(typeof(AccountType), bankAccount["ACCTTYPE"].InnerText)
                };

                accountDtoCreateResult = await _accountService.Post(accountDto);

                statementDto = new StatementDto
                {
                    UId = Convert.ToInt32(stmtTrnRs["TRNUID"].InnerText),
                    Currency = stmt["CURDEF"].InnerText,
                    AccountId = accountDtoCreateResult.Id,
                    StatusId = statusDtoCreateResult.Id
                };

                await _statementService.Post(statementDto);

                var bankTransaction = stmt.SelectSingleNode("//BANKTRANLIST");

                transactionDto = new TransactionDto();

                transactionDto.Start = Convert.ToDateTime(bankTransaction["DTSTART"].InnerText);
                transactionDto.End = Convert.ToDateTime(bankTransaction["DTEND"].InnerText);

                var statementTransactionList = bankTransaction.SelectNodes("//STMTTRN");
                var statements = new List<StatementTransactionDto>();

                foreach (XmlNode node in statementTransactionList)
                {
                    var statementTransaction = new StatementTransactionDto();

                    statementTransaction.TransactionType = (TransactionType)Enum.Parse(typeof(TransactionType), node["TRNTYPE"].InnerText);
                    statementTransaction.Posted = Convert.ToDateTime(node["DTPOSTED"].InnerText);
                    statementTransaction.Amount = Convert.ToDecimal(node["TRNAMT"].InnerText.Replace(".", ",").Trim());
                    statementTransaction.Memo = node["MEMO"].InnerText;
                    statementTransaction.Transaction = transactionDto;

                    statements.Add(statementTransaction);
                }

                transactionDto.Statements = statements.AsEnumerable();
                await _transactionService.Post(transactionDto);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

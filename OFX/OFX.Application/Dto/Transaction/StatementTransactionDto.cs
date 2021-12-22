using System;
using System.ComponentModel.DataAnnotations;
using OFX.Domain.Enums;

namespace OFX.Application.Dto.Transaction
{
    public class StatementTransactionDto
    {
        [Display(Name = "Transaction")]
        public TransactionType TransactionType { get; set; }

        [Display(Name = "Transaction Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy hh:mm:ss}")]
        [DataType(DataType.DateTime)]
        public DateTime Posted { get; set; }

        [DataType(DataType.Currency)]
        public decimal Amount { get; set; }

        public string Memo { get; set; }

        public TransactionDto Transaction { get; set; }
    }
}

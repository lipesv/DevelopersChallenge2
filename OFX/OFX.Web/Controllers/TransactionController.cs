using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OFX.Application.Services.Interfaces;
using OFX.Application.Services.Interfaces.Account;
using OFX.Application.Services.Interfaces.Statement;
using OFX.Application.Services.Interfaces.Status;
using OFX.Application.Services.Interfaces.Transaction;
using OFX.Web.Models;

namespace OFX.Web.Controllers
{
    public class TransactionController : Controller
    {
        private readonly ILogger<TransactionController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;

        private readonly IParserService _parserService;
        private readonly IStatementService _statementService;
        private readonly IStatusService _statusService;
        private readonly IAccountService _accountService;
        private readonly ITransactionService _transactionService;
        private readonly IStatementTransactionService _statementTransactionService;

        public TransactionController(ILogger<TransactionController> logger,
                                     IWebHostEnvironment webHostEnvironment,
                                     IParserService parserService,
                                     IStatementService statementService,
                                     IStatusService statusService,
                                     IAccountService accountService,
                                     ITransactionService transactionService,
                                     IStatementTransactionService statementTransactionService)
        {
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;

            _parserService = parserService;
            _statementService = statementService;
            _statusService = statusService;
            _accountService = accountService;
            _transactionService = transactionService;
            _statementTransactionService = statementTransactionService;
        }

        public async Task<IActionResult> Index()
        {
            var results = await _statementTransactionService.Get(true);

            var distinct = results
                            .GroupBy(st => new { st.TransactionType, st.Posted, st.Amount })
                            .Select(st => st.FirstOrDefault())
                            .OrderBy(st => st.Posted);

            ViewData["transactions"] = distinct;

            return View();
        }

        [HttpGet]
        public IActionResult Import()
        {
            return View();
        }

        [HttpPost, ActionName("Import")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ImportFiles(List<IFormFile> files)
        {
            // checks if there are any files in list
            if (files.Count() > 0)
            {
                try
                {
                    // processes uploaded files scrolling through list of selected files
                    // scrolling through list of selected files
                    foreach (var file in files)
                    {

                        // Set folder where sented files will be saved
                        string folder = "Upload";

                        // get physical path from wwwroot folder
                        string WebRoot_path = _webHostEnvironment.WebRootPath;

                        // set folder path where sented files will be saved
                        string uploadFolder = WebRoot_path + "\\Transactions\\" + folder + "\\";

                        // include upload folder and uploaded file name
                        string destinationFilePath = uploadFolder + file.FileName;

                        //copy file to destination folder
                        using (var stream = new FileStream(destinationFilePath, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }

                        await _parserService.Parse(uploadFolder);
                    }

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ViewData["Error"] = ex.Message;
                }
            }
            else
            {
                ViewData["Error"] = "No files have been selected.";
            }

            //retorna a viewdata
            return View(ViewData);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

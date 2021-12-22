using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OFX.Application.Dto.Transaction
{
    public class TransactionDto
    {

        [Display(Name = "Data Início")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime Start { get; set; }

        [Display(Name = "Data Término")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime End { get; set; }

        public IEnumerable<StatementTransactionDto> Statements { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OFX.Application.Dto.Transaction
{
    public class TransactionDtoCreateResult
    {

        public Guid Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public IEnumerable<StatementTransactionDto> Statements { get; set; }
    }
}

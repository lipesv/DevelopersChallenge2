using System;
using System.Collections.Generic;

namespace OFX.Domain.Models
{
    public class TransactionModel : ModelBase
    {
        private DateTime _start;
        public DateTime Start
        {
            get { return _start; }
            set { _start = value; }
        }

        private DateTime _end;
        public DateTime End
        {
            get { return _end; }
            set { _end = value; }
        }

        private IEnumerable<StatementTransactionModel> _statements;
        public IEnumerable<StatementTransactionModel> Statements
        {
            get { return _statements; }
            set { _statements = value; }
        }

    }
}

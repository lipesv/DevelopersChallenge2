using System;
using System.Collections.Generic;

namespace OFX.Domain.Entities
{
    public class Transaction : BaseEntity
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public virtual IEnumerable<StatementTransaction> Statements { get; set; }

    }
}

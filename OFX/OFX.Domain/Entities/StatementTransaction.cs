using System;
using OFX.Domain.Enums;

namespace OFX.Domain.Entities
{
    public class StatementTransaction : BaseEntity
    {
        public TransactionType TransactionType { get; set; }
        public DateTime Posted { get; set; }
        public decimal Amount { get; set; }
        public string Memo { get; set; }

        public virtual Transaction Transaction { get; set; }
    }
}

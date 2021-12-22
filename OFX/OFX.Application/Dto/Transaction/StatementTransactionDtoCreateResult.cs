using System;
using OFX.Domain.Enums;

namespace OFX.Application.Dto.Transaction
{
    public class StatementTransactionDtoCreateResult
    {
        public Guid TransactionId { get; set; }
        public TransactionType TransactionType { get; set; }
        public DateTime Posted { get; set; }
        public decimal Amount { get; set; }
        public string Memo { get; set; }
    }
}

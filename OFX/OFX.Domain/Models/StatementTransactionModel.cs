using System;
using OFX.Domain.Enums;

namespace OFX.Domain.Models
{
    public class StatementTransactionModel : ModelBase
    {
        private TransactionType _transactionType;
        public TransactionType TransactionType { get => _transactionType; set => _transactionType = value; }

        private DateTime _posted;
        public DateTime Posted { get => _posted; set => _posted = value; }

        private decimal _amount;
        public decimal Amount { get => _amount; set => _amount = value; }

        private string _memo;
        public string Memo { get => _memo; set => _memo = value; }

        private TransactionModel _transaction;
        public TransactionModel Transaction { get => _transaction; set => _transaction = value; }
    }
}

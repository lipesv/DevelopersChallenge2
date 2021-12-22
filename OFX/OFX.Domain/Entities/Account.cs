using System;
using OFX.Domain.Enums;

namespace OFX.Domain.Entities
{
    public class Account : BaseEntity
    {
        public string BankId { get; set; }
        public string AccountId { get; set; }
        public AccountType AccountType { get; set; }

        public Statement Statement { get; set; }

    }
}

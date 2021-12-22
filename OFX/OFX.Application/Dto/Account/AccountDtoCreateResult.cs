using System;
using OFX.Domain.Enums;

namespace OFX.Application.Dto.Account
{
    public class AccountDtoCreateResult
    {
        public Guid Id { get; set; }
        public string BankId { get; set; }
        public string AccountId { get; set; }
        public AccountType AccountType { get; set; }
        public Guid StatementId { get; set; }
    }
}

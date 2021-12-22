using System;
using OFX.Domain.Enums;

namespace OFX.Application.Dto.Account
{
    public class AccountCreateDto
    {
        public string BankId { get; set; }
        public string AccountId { get; set; }
        public AccountType AccountType { get; set; }

    }
}

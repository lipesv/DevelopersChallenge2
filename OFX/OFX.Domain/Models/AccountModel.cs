using System;
using OFX.Domain.Enums;

namespace OFX.Domain.Models
{
    public class AccountModel : ModelBase
    {
        private string _bankId;
        public string BankId
        {
            get { return _bankId; }
            set { _bankId = value; }
        }

        private string _accountId;
        public string AccountId
        {
            get { return _accountId; }
            set { _accountId = value; }
        }

        private AccountType _type;
        public AccountType Type
        {
            get { return _type; }
            set { _type = value; }
        }

        private Guid _statementId;
        public Guid StatementId
        {

            get { return _statementId; }
            set { _statementId = value; }
        }

    }
}

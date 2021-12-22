using System;

namespace OFX.Domain.Models
{
    public class StatementModel : ModelBase
    {
        private int _uId;
        public int UId { get => _uId; set => _uId = value; }


        private string _currency;
        public string Currency { get => _currency; set => _currency = value; }


        private Guid _accountId;
        public Guid AccountId { get => _accountId; set => _accountId = value; }


        private Guid _statusId;
        public Guid StatusId { get => _statusId; set => _statusId = value; }
    }
}

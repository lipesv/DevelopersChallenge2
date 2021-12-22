using System;

namespace OFX.Domain.Entities
{
    public class Statement : BaseEntity
    {
        public int UId { get; set; }
        public string Currency { get; set; }

        public Guid StatusId { get; set; }
        public Status Status { get; set; }

        public Guid AccountId { get; set; }
        public Account Account { get; set; }
    }
}

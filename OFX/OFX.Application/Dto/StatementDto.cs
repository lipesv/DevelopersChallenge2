using System;

namespace OFX.Application.Dto
{
    public class StatementDto
    {
        public int UId { get; set; }
        public string Currency { get; set; }
        public Guid AccountId { get; set; }
        public Guid StatusId { get; set; }
    }
}

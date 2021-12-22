using System;
using OFX.Domain.Enums;

namespace OFX.Domain.Entities
{
    public class Status : BaseEntity
    {
        public int Code { get; set; }
        public SeverityType Severity { get; set; }

        public Statement Statement { get; set; }
    }
}

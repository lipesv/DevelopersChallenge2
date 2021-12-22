using System;
using OFX.Domain.Enums;

namespace OFX.Application.Dto.Status
{
    public class StatusDtoCreateResult
    {
        public Guid Id { get; set; }
        public int Code { get; set; }
        public SeverityType Severity { get; set; }
    }
}

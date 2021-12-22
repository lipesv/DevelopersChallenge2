using System;
using OFX.Domain.Enums;

namespace OFX.Application.Dto.Status
{
    public class StatusDto
    {
        public int Code { get; set; }
        public SeverityType Severity { get; set; }
    }
}

using OFX.Domain.Enums;

namespace OFX.Domain.Models
{
    public class StatusModel : ModelBase
    {
        private int _code;
        public int Code { get => _code; set => _code = value; }

        private SeverityType _severity;
        public SeverityType Severity { get => _severity; set => _severity = value; }
    }
}

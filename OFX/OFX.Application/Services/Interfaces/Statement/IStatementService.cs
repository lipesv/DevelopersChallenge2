using System.Collections.Generic;
using System.Threading.Tasks;
using OFX.Application.Dto;

namespace OFX.Application.Services.Interfaces.Statement
{
    public interface IStatementService
    {
        Task<IEnumerable<StatementDto>> Get();

        Task<StatementDto> Post(StatementDto statement);
    }
}

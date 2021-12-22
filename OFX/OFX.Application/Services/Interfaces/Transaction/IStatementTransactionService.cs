using System.Collections.Generic;
using System.Threading.Tasks;
using OFX.Application.Dto.Transaction;

namespace OFX.Application.Services.Interfaces.Transaction
{
    public interface IStatementTransactionService
    {
        Task<IEnumerable<StatementTransactionDto>> Get(bool include = false);
        Task<StatementTransactionDtoCreateResult> Post(StatementTransactionDto statementTransaction);
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;
using OFX.Application.Dto.Transaction;

namespace OFX.Application.Services.Interfaces.Transaction
{
    public interface ITransactionService
    {
        Task<IEnumerable<TransactionDto>> Get(bool include = false);
        Task<TransactionDtoCreateResult> Post(TransactionDto transaction);

    }
}

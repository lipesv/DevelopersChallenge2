using System.Threading.Tasks;
using OFX.Application.Dto.Account;

namespace OFX.Application.Services.Interfaces.Account
{
    public interface IAccountService
    {
        Task<AccountDtoCreateResult> Post(AccountCreateDto status);
    }
}

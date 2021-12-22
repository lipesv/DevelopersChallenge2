using System;
using System.Threading.Tasks;
using AutoMapper;
using OFX.Application.Dto.Account;
using OFX.Application.Services.Interfaces.Account;
using OFX.Data.Repository.Interfaces;
using OFX.Domain.Models;

namespace OFX.Application.Services.Account
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _repository;
        private readonly IMapper _mapper;

        public AccountService(IAccountRepository repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<AccountDtoCreateResult> Post(AccountCreateDto account)
        {
            try
            {
                var model = _mapper.Map<AccountModel>(account);
                var entity = await _repository.InsertAsync(_mapper.Map<Domain.Entities.Account>(account));

                return _mapper.Map<AccountDtoCreateResult>(entity);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

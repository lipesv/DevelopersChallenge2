using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OFX.Application.Dto.Transaction;
using OFX.Application.Services.Interfaces.Transaction;
using OFX.Data.Repository.Interfaces;

namespace OFX.Application.Services.Transaction
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _repository;
        private readonly IMapper _mapper;

        public TransactionService(ITransactionRepository repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<TransactionDto>> Get(bool include = false)
        {
            try
            {
                var list = await (include == true ? _repository.SelectAsync(include: i => i.Include(st => st.Statements)) : _repository.SelectAsync());
                return _mapper.Map<IEnumerable<TransactionDto>>(list);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<TransactionDtoCreateResult> Post(TransactionDto transaction)
        {
            try
            {
                var model = _mapper.Map<TransactionDto>(transaction);
                var entity = await _repository.InsertAsync(_mapper.Map<Domain.Entities.Transaction>(model));

                return _mapper.Map<TransactionDtoCreateResult>(entity);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

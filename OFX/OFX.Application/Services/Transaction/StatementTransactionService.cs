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
    public class StatementTransactionService : IStatementTransactionService
    {
        private readonly IStatementTransactionRepository _repository;
        private readonly IMapper _mapper;

        public StatementTransactionService(IStatementTransactionRepository repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<StatementTransactionDto>> Get(bool include = false)
        {
            try
            {
                var list = await (include == true ? _repository.SelectAsync(include: i => i.Include(st => st.Transaction)) : _repository.SelectAsync());
                return _mapper.Map<IEnumerable<StatementTransactionDto>>(list);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<StatementTransactionDtoCreateResult> Post(StatementTransactionDto statementTransaction)
        {
            try
            {
                var model = _mapper.Map<StatementTransactionDto>(statementTransaction);
                var entity = await _repository.InsertAsync(_mapper.Map<Domain.Entities.StatementTransaction>(model));

                return _mapper.Map<StatementTransactionDtoCreateResult>(entity);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

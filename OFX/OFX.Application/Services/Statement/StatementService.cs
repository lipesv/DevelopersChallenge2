using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using OFX.Application.Dto;
using OFX.Application.Services.Interfaces.Statement;
using OFX.Data.Repository.Interfaces;
using OFX.Domain.Models;

namespace OFX.Application.Services.Statement
{
    public class StatementService : IStatementService
    {
        private readonly IStatementRepository _repository;
        private readonly IMapper _mapper;

        public StatementService(IStatementRepository repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public Task<IEnumerable<StatementDto>> Get()
        {
            throw new NotImplementedException();
        }

        public async Task<StatementDto> Post(StatementDto statement)
        {
            try
            {
                var model = _mapper.Map<StatementModel>(statement);
                var entity = await _repository.InsertAsync(_mapper.Map<Domain.Entities.Statement>(model));

                return _mapper.Map<StatementDto>(entity);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

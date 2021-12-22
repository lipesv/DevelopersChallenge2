using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using OFX.Application.Dto.Status;
using OFX.Application.Services.Interfaces.Status;
using OFX.Data.Repository.Interfaces;
using OFX.Domain.Models;

namespace OFX.Application.Services.Status
{
    public class StatusService : IStatusService
    {
        private readonly IStatusRepository _repository;
        private readonly IMapper _mapper;

        public StatusService(IStatusRepository repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<StatusDtoCreateResult> Post(StatusDto status)
        {
            try
            {
                var model = _mapper.Map<StatusModel>(status);
                var entity = await _repository.InsertAsync(_mapper.Map<Domain.Entities.Status>(status));

                return _mapper.Map<StatusDtoCreateResult>(entity);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

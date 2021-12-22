using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OFX.Application.Dto.Status;

namespace OFX.Application.Services.Interfaces.Status
{
    public interface IStatusService
    {
        Task<StatusDtoCreateResult> Post(StatusDto status);
    }
}

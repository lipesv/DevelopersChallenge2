using AutoMapper;
using OFX.Application.Dto;
using OFX.Application.Dto.Account;
using OFX.Application.Dto.Status;
using OFX.Application.Dto.Transaction;
using OFX.Domain.Models;

namespace OFX.CrossCutting.Mapping
{
    public class DtoToModelProfile : Profile
    {
        public DtoToModelProfile()
        {
            CreateMap<StatusModel, StatusDto>()
                .ReverseMap();

            CreateMap<AccountModel, AccountDtoCreateResult>()
                .ReverseMap();

            CreateMap<AccountModel, AccountCreateDto>()
                .ReverseMap();

            CreateMap<StatementModel, StatementDto>()
                .ReverseMap();

            CreateMap<TransactionModel, TransactionDto>()
                .ReverseMap();

            CreateMap<StatementTransactionModel, StatementTransactionDto>()
                .ReverseMap();

        }
    }
}

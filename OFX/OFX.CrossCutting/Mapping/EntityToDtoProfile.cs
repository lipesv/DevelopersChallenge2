using AutoMapper;
using OFX.Application.Dto;
using OFX.Application.Dto.Account;
using OFX.Application.Dto.Status;
using OFX.Application.Dto.Transaction;
using OFX.Domain.Entities;

namespace OFX.CrossCutting.Mapping
{
    public class EntityToDtoProfile : Profile
    {
        public EntityToDtoProfile()
        {
            CreateMap<Account, AccountDtoCreateResult>()
                .ReverseMap();

            CreateMap<Account, AccountCreateDto>()
                .ReverseMap();

            CreateMap<Status, StatusDto>()
                .ReverseMap();

            CreateMap<Status, StatusDtoCreateResult>()
                .ReverseMap();

            CreateMap<Statement, StatementDto>()
                .ReverseMap();

            CreateMap<Transaction, TransactionDto>()
                .ReverseMap();

            CreateMap<Transaction, TransactionDtoCreateResult>()
                .ReverseMap();

            CreateMap<StatementTransaction, StatementTransactionDto>()
                .ReverseMap();
            CreateMap<StatementTransaction, StatementTransactionDtoCreateResult>()
                .ReverseMap();




        }
    }
}

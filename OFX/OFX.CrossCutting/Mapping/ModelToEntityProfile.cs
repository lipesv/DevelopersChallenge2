using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using OFX.Domain.Entities;
using OFX.Domain.Models;

namespace OFX.CrossCutting.Mapping
{
    public class ModelToEntityProfile : Profile
    {
        public ModelToEntityProfile()
        {
            CreateMap<AccountModel, Account>()
                .ReverseMap();

            CreateMap<TransactionModel, Transaction>()
                .ReverseMap();

            CreateMap<StatementTransactionModel, StatementTransaction>()
                .ReverseMap();

            CreateMap<StatementModel, Statement>()
                .ReverseMap();

            CreateMap<StatusModel, Status>()
                .ReverseMap();


        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OFX.Data.Context;
using OFX.Data.Repository.Interfaces;
using OFX.Domain.Entities;

namespace OFX.Data.Repository
{
    public class StatementTransactionRepository : BaseRepository<StatementTransaction>, IStatementTransactionRepository
    {
        public StatementTransactionRepository(MyContext context) : base(context) { }
    }
}

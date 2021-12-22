using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OFX.Data.Context;
using OFX.Data.Repository.Interfaces;
using OFX.Domain.Entities;

namespace OFX.Data.Repository
{
    public class TransactionRepository : BaseRepository<Transaction>, ITransactionRepository
    {
        public TransactionRepository(MyContext context) : base(context) { }
    }
}

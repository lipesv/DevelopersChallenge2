using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OFX.Data.Context;
using OFX.Data.Repository.Interfaces;
using OFX.Domain.Entities;

namespace OFX.Data.Repository
{
    public class AccountRepository : BaseRepository<Account>, IAccountRepository
    {
        public AccountRepository(MyContext context) : base(context) { }
    }
}

using Microsoft.EntityFrameworkCore;
using OFX.Data.Mapping;
using OFX.Domain.Entities;

namespace OFX.Data.Context
{
    public class MyContext : DbContext
    {

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Statement> Statements { get; set; }
        public DbSet<StatementTransaction> StatementTransactions { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public MyContext(DbContextOptions<MyContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Account>(new AccountMap().Configure);
            modelBuilder.Entity<Statement>(new StatementMap().Configure);
            modelBuilder.Entity<StatementTransaction>(new StatementTransactionMap().Configure);
            modelBuilder.Entity<Status>(new StatusMap().Configure);
            modelBuilder.Entity<Transaction>(new TransactionMap().Configure);
        }
    }
}

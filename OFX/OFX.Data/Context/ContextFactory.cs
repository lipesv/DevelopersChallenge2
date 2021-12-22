using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace OFX.Data.Context
{
    public class ContextFactory : IDesignTimeDbContextFactory<MyContext>
    {
        public MyContext CreateDbContext(string[] args)
        {
            //usado para criar as migrações

            //mysql connection string
            var connectionString = "Server=localhost;Port=3306;Database=dbDevelopersChallenge;Uid=root;Pwd=Lype@1824#";

            // sqlserver connection string
            //var connectionString = "Server=(localdb)\\MSSQLLocalDB;Initial Catalog=dbAPI;Trusted_Connection=True;MultipleActiveResultSets=true;";

            var optionsBuilder = new DbContextOptionsBuilder<MyContext>();

            // utilizando options builder para o MySql 
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));

            // utilizando options builder para o SQL Server 
            //optionsBuilder.UseSqlServer(connectionString);

            return new MyContext(optionsBuilder.Options);
        }
    }
}

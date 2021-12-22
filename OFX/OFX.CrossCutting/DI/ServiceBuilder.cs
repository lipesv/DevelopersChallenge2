using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OFX.CrossCutting.DI.Interfaces;
using AutoMapper;
using OFX.CrossCutting.Mapping;
using OFX.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using OFX.Application.Services.Interfaces.Status;
using OFX.Application.Services.Interfaces.Account;
using OFX.Application.Services.Interfaces.Statement;
using OFX.Application.Services.Interfaces.Transaction;
using OFX.Application.Services.Status;
using OFX.Application.Services.Account;
using OFX.Application.Services.Statement;
using OFX.Application.Services.Transaction;
using OFX.Data.Repository.Interfaces;
using OFX.Data.Repository;
using OFX.Application.Services.Interfaces;
using OFX.Application.Util;

namespace OFX.CrossCutting.DI
{
    public class ServiceBuilder : IApplicationService, IContextService
    {
        private readonly IServiceCollection _services;
        private readonly IConfiguration _configuration;
        private readonly IApplicationBuilder _applicationBuilder;

        private ServiceBuilder(IServiceCollection services, IConfiguration configuration)
        {
            _services = services;
            _configuration = configuration;
        }

        private ServiceBuilder(IApplicationBuilder app)
        {
            _applicationBuilder = app;
        }

        public static ServiceBuilder Create(IServiceCollection services, IConfiguration configuration)
        {
            return new ServiceBuilder(services, configuration);
        }

        public static ServiceBuilder Create(IApplicationBuilder applicationBuilder)
        {
            return new ServiceBuilder(applicationBuilder);
        }

        public IApplicationService Register()
        {
            _services.AddTransient<IStatusService, StatusService>();
            _services.AddTransient<IAccountService, AccountService>();
            _services.AddTransient<IStatementService, StatementService>();
            _services.AddTransient<ITransactionService, TransactionService>();
            _services.AddTransient<IStatementTransactionService, StatementTransactionService>();
            _services.AddTransient<IParserService, ParserService>();

            _services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));

            _services.AddScoped<IStatusRepository, StatusRepository>();
            _services.AddScoped<IAccountRepository, AccountRepository>();
            _services.AddScoped<IStatementRepository, StatementRepository>();
            _services.AddScoped<ITransactionRepository, TransactionRepository>();
            _services.AddScoped<IStatementTransactionRepository, StatementTransactionRepository>();

            return this;
        }

        public IApplicationService CreateMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ModelToEntityProfile());
                cfg.AddProfile(new DtoToModelProfile());
                cfg.AddProfile(new EntityToDtoProfile());
            });

            IMapper mapper = config.CreateMapper();
            _services.AddSingleton(mapper);

            return this;
        }

        public IContextService ApplyMigration()
        {
            var migrationConf = Environment.GetEnvironmentVariable("MIGRATION");

            if (!string.IsNullOrEmpty(migrationConf) && migrationConf.ToLower() == "apply")
            {
                using (var service = _applicationBuilder.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
                {
                    using (var context = service.ServiceProvider.GetService<MyContext>())
                    {
                        context.Database.Migrate();
                    }
                }
            }

            return this;
        }

        public IContextService ConfigureContext()
        {
            var databaseConf = Environment.GetEnvironmentVariable("DATABASE");

            if (!string.IsNullOrEmpty(databaseConf))
            {
                switch (databaseConf.ToLower())
                {
                    case "sqlserver":

                        _services.AddDbContext<MyContext>(
                            options => options.UseSqlServer(Environment.GetEnvironmentVariable("DB_CONNECTION"))
                        );

                        break;

                    case "mysql":

                        _services.AddDbContext<MyContext>(
                            options => options.UseMySql(Environment.GetEnvironmentVariable("DB_CONNECTION"), ServerVersion.AutoDetect(Environment.GetEnvironmentVariable("DB_CONNECTION")))
                        );

                        break;
                }
            }

            return this;
        }

    }
}

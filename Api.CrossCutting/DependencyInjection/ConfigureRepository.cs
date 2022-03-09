using Api.Data.Context;
using Api.Data.Implementations;
using Api.Data.Repository;
using Api.Domain.Interfaces;
using Api.Domain.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Api.CrossCutting.DependencyInjection
{
    public class ConfigureRepository
    {
        public static void ConfigureDependenciesRepository(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            serviceCollection.AddScoped(typeof(IUserRepository), typeof(UserImplementation));

            string connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION");

            if (Environment.GetEnvironmentVariable("DATABASE")?.ToLower() == "mysql")
            {
                serviceCollection.AddDbContext<MyContext>(
                    options => options.UseSqlServer("Data Source=W01807DSQL0;Initial Catalog=FTS_FI_BR;User ID=LFTS_ANALISTA;Password=Frontrade@@;")
                );
            }
            else
            {
                serviceCollection.AddDbContext<MyContext>(
                    options => options.UseSqlServer("Data Source=W01807DSQL0;Initial Catalog=FTS_FI_BR;User ID=LFTS_ANALISTA;Password=Frontrade@@;")
                );
            }
        }
    }
}

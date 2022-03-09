using Api.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Api.Data.Test
{
    public class DbTest : IDisposable
    {
        private string dataBaseName = $"dbApiTet_{Guid.NewGuid().ToString().Replace("-", "")}";
        public ServiceProvider serviceProvider { get; private set; }
        public DbTest()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddDbContext<MyContext>(o => o.UseSqlServer($"Data Source=W01807DSQL0;Initial Catalog=FTS_FI_BR;User ID=LFTS_ANALISTA;Password=Frontrade@@;"),
                ServiceLifetime.Transient
            );

            serviceProvider = serviceCollection.BuildServiceProvider();
            using (var context = serviceProvider.GetService<MyContext>())
            {
                context.Database.EnsureCreated();
            }
        }
        public void Dispose()
        {
            using (var context = serviceProvider.GetService<MyContext>())
            {
                //context.Database.EnsureDeleted();
            }
        }
    }
}

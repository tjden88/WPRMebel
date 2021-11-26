using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WPRMebel.DB.SqLite.Context;

namespace WPRMebel.TestConsole
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var s = Stopwatch.StartNew();
            var cdb = Services.GetRequiredService<CatalogDbContext>();
            await cdb.Database.EnsureDeletedAsync();
            await cdb.Database.MigrateAsync();
            await cdb.InitializeStartData();

        }


        private static IServiceProvider _Services;
        public static IServiceProvider Services => _Services ??= ConfigureServices();

        private static IServiceProvider ConfigureServices()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddDbContext<CatalogDbContext>();
            return serviceCollection.BuildServiceProvider();
        }
    }
}

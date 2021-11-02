using System;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using WPRMebel.DB.TestSqlServer.Context;
using WPRMebel.WPF.Services;

namespace WPRMebel.WPF
{
    public partial class App : Application
    {

        private static IServiceProvider _Services;
        public static IServiceProvider Services => _Services ??= ConfigureServices();

        private static IServiceProvider ConfigureServices()
        {
            var serviceCollection = new ServiceCollection();

            return serviceCollection.AddDbContext<CatalogDbContext>()
                .AddServices()
                .AddViewModels()
                .BuildServiceProvider()
                ;
        }
    }
}

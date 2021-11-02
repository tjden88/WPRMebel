using System;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using WPRMebel.WPF.Services;
using WPRMebel.WpfAPI.Services;

namespace WPRMebel.WPF
{
    public partial class App : Application
    {
        private static IServiceProvider _Services;
        public static IServiceProvider Services => _Services ??= ConfigureServices();

        private static IServiceProvider ConfigureServices()
        {
            var serviceCollection = new ServiceCollection();

            return serviceCollection
                .AddServices()
                .AddApiServices()
                .AddDb()
                .AddViewModels()
                .BuildServiceProvider()
                ;
        }
    }
}

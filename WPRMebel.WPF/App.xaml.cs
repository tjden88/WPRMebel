using System;
using System.Linq;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using WPRMebel.WPF.Services;
using WPRMebel.WpfAPI.Services;

namespace WPRMebel.WPF
{
    public partial class App
    {

        public static Window ActiveWindow => Current.Windows.Cast<Window>().FirstOrDefault(w => w.IsActive);


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

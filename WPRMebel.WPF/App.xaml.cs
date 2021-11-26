using System;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Markup;
using Microsoft.Extensions.DependencyInjection;
using WPRMebel.DB.SqLite.Context;
using WPRMebel.WPF.Services;
using WPRMebel.WPF.Views.Windows;
using WPRMebel.WpfAPI.Services;

namespace WPRMebel.WPF
{
    public partial class App
    {
        protected override async void OnStartup(StartupEventArgs e)
        {
             // Загрузка начальных данных
            LoadWindow loadWindow = new();
            loadWindow.Show();

           Thread.CurrentThread.CurrentCulture = new CultureInfo("ru-RU");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("ru-RU");
            FrameworkElement.LanguageProperty.OverrideMetadata(typeof(FrameworkElement), new FrameworkPropertyMetadata(
                XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag)));

            base.OnStartup(e);

            loadWindow.SetMessage("Загрузка баз данных...");

            using (var scope = Services.CreateScope())
            {
               await scope.ServiceProvider.GetRequiredService<CatalogDbContext>().InitializeStartData().ConfigureAwait(true);
            }


            loadWindow.SetMessage("Загрузка главного окна...");

            MainWindow mainWindow = new();
            loadWindow.Close();
            mainWindow.Show();
        }

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

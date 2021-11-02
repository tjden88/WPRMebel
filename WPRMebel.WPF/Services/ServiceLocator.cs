using Microsoft.Extensions.DependencyInjection;
using WPRMebel.WPF.ViewModels.MainPages;
using WPRMebel.WPF.ViewModels.Windows;

namespace WPRMebel.WPF.Services
{
    /// <summary>
    /// Локатор сервисов и вьюмоделей
    /// </summary>
    internal static class ServiceLocator
    {
        /// <summary> Зарегистрировать сервисы </summary>
        public static IServiceCollection AddServices(this IServiceCollection Services) => Services
        
            ;

        /// <summary> Зарегистрировать модель-представления </summary>
        public static IServiceCollection AddViewModels(this IServiceCollection Services) => Services
                .AddSingleton<MainWindowViewModel>()
                .AddSingleton<CatalogViewModel>()
                ;
    }

    internal class ViewModelLocator
    {
        public MainWindowViewModel MainWindowViewModel => App.Services.GetRequiredService<MainWindowViewModel>();
        public CatalogViewModel CatalogViewModel => App.Services.GetRequiredService<CatalogViewModel>();
    }
}

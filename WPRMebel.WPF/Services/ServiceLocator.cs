using WPR.MVVM;
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
        public static void RegisterServices()
        {
            //DependensyInjection.Registrator.AddSingleton<>()
        }

        /// <summary> Зарегистрировать модель-представления </summary>
        public static void RegisterViewModels()
        {
            DependensyInjection.Registrator
                .AddSingleton<MainWindowViewModel>()
                .AddSingleton<CatalogViewModel>()
                ;
        }

        /// <summary> Получить сервис из контейнера сервисов </summary>
        public static T Get<T>(params object[] parameters) where T : class => DependensyInjection.Get<T>(parameters);
    }

    internal class ViewModelLocator
    {
        public MainWindowViewModel MainWindowViewModel => ServiceLocator.Get<MainWindowViewModel>();
        public CatalogViewModel CatalogViewModel => ServiceLocator.Get<CatalogViewModel>();
    }
}

using WPR.MVVM;
using WPRMebel.WPF.ViewModels.Windows;

namespace WPRMebel.WPF.Services
{
    /// <summary>
    /// Локатор сервисов и вьюмоделей
    /// </summary>
    static class ServiceLocator
    {
        /// <summary> Зарегистрировать сервисы </summary>
        public static void RegisterServices()
        {
            //DependensyInjection.Registrator.AddSingleton<>()
        }

        /// <summary> Зарегистрировать модель-представления </summary>
        public static void RegisterViewModels()
        {
            DependensyInjection.Registrator.AddSingleton<MainWindowViewModel>()
                ;
        }

        /// <summary> Получить сервис из контейнера сервисов </summary>
        public static T Get<T>(params object[] parameters) where T : class => DependensyInjection.Get<T>(parameters);
    }

    class ViewModelLocator
    {
        public MainWindowViewModel MainWindowViewModel => ServiceLocator.Get<MainWindowViewModel>();
    }
}

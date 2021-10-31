using WPR.MVVM.Commands;
using WPR.MVVM.ViewModels;

namespace WPRMebel.WPF.ViewModels.Windows
{
    internal class MainWindowViewModel : WindowViewModel
    {
        public MainWindowViewModel()
        {
            Title = "WPR Мебель Alpha";
        }


        #region Commands

        #region Command SetMainPageContentCommand - Установить содержимое основной части окна

        /// <summary>Установить содержимое основной части окна</summary>
        private Command _SetMainPageContentCommand;

        /// <summary>Установить содержимое основной части окна</summary>
        public Command SetMainPageContentCommand => _SetMainPageContentCommand
            ??= new Command(OnSetMainPageContentCommandExecuted, CanSetMainPageContentCommandExecute, "Установить содержимое основной части окна");

        /// <summary>Проверка возможности выполнения - Установить содержимое основной части окна</summary>
        private bool CanSetMainPageContentCommandExecute(object p) => true;

        /// <summary>Логика выполнения - Установить содержимое основной части окна</summary>
        private void OnSetMainPageContentCommandExecuted(object p)
        {
            MainPageContent = p;
        }

        #endregion

        #endregion


        #region MainPageContent : object - Содержимое основной части окна

        /// <summary>Содержимое основной части окна</summary>
        private object _MainPageContent;

        /// <summary>Содержимое основной части окна</summary>
        public object MainPageContent
        {
            get => _MainPageContent;
            set => Set(ref _MainPageContent, value);
        }

        #endregion

        

        

        
    }
}

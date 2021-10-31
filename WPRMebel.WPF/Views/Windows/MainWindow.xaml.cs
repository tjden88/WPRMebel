using System.Windows;
using WPR;

namespace WPRMebel.WPF.Views.Windows
{
    public partial class MainWindow
    {
        public MainWindow() => InitializeComponent();

        private void ButtonBase_OnClick(object Sender, RoutedEventArgs E)
        {
            //TODO: убрать обработчик
           Design.SetNewRandomStyle();
        }
    }
}

using System.Windows;
using WPRMebel.WPF.Services;

namespace WPRMebel.WPF
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            ServiceLocator.RegisterServices();
            ServiceLocator.RegisterViewModels();
        }
    }
}

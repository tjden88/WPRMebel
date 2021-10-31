using System.Windows;
using System.Windows.Controls;

namespace WPRMebel.WPF.Controls
{
    /// <summary>
    /// Контрол для отображения контента с анимацией при изменении контента
    /// </summary>
    public class MainPageViewer : Control
    {
        static MainPageViewer()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MainPageViewer), new FrameworkPropertyMetadata(typeof(MainPageViewer)));
        }
    }
}

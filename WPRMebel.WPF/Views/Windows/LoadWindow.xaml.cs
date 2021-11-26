using System;
using System.ComponentModel;
using System.Windows;

namespace WPRMebel.WPF.Views.Windows
{
    /// <summary>
    /// Логика взаимодействия для LoadWindow.xaml
    /// </summary>
    public partial class LoadWindow
    {
        public LoadWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        #region Message : string - Сообщение загрузки

        /// <summary>Сообщение загрузки</summary>
        public static readonly DependencyProperty MessageProperty =
            DependencyProperty.Register(
                nameof(Message),
                typeof(string),
                typeof(LoadWindow),
                new PropertyMetadata("Загрузка программы..."));

        /// <summary>Сообщение загрузки</summary>
        [Category("LoadWindow")]
        [Description("Сообщение загрузки")]
        public string Message
        {
            get => (string) GetValue(MessageProperty);
            set => SetValue(MessageProperty, value);
        }

        #endregion

        public void SetMessage(string message) => this.DoDispatherAction(() => Message = message);
    }
}

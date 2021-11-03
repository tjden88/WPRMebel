using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using WPR;
using WPR.MVVM.Commands;
using WPR.MVVM.ViewModels;
using WPRMebel.WPF.Views.Dialogs;

namespace WPRMebel.WPF.ViewModels.Dialogs
{
    internal class EditCatalogSectionDialogViewModel : DataValidationViewModel, IWPRDialog
    {
        private readonly bool _CreateNewSection;
        public Action<bool> DialogResult { get; set; }
        public object DialogContent { get; set; }


        public EditCatalogSectionDialogViewModel()
        {
        }
        public EditCatalogSectionDialogViewModel(bool CreateNewSection)
        {
            _CreateNewSection = CreateNewSection;
            DialogContent = new EditCatalogSectionDialog {DataContext = this};

            var nameValidation = new List<Error>
            {
                new(() => string.IsNullOrEmpty(SectionName), "Необходимо задать имя раздела"),
                new(() => Text == "111", "Такой раздел уже существует"),
            };

            ErrorInfo.Add(nameof(SectionName), nameValidation);
        }

        #region Command OkCommand - Подтвердить результат диалога

        /// <summary>Подтвердить результат диалога</summary>
        private Command _OkCommand;

        /// <summary>Подтвердить результат диалога</summary>
        public Command OkCommand => _OkCommand
            ??= new Command(OnOkCommandExecuted, CanOkCommandExecute, "Подтвердить результат диалога");

        /// <summary>Проверка возможности выполнения - Подтвердить результат диалога</summary>
        private bool CanOkCommandExecute() => true;

        /// <summary>Логика выполнения - Подтвердить результат диалога</summary>
        private void OnOkCommandExecuted() => DialogResult?.Invoke(true);

        #endregion

        #region HeaderText : string - Текст заголовка диалога

        /// <summary>Текст заголовка диалога</summary>
        public string HeaderText => _CreateNewSection
            ? "Создать" 
            : "Изменить"
              + " раздел каталога";

        #endregion

        #region SectionName : string - Имя раздела

        /// <summary>Имя раздела</summary>
        private string _SectionName;

        /// <summary>Имя раздела</summary>
        public string SectionName
        {
            get => _SectionName;
            set => Set(ref _SectionName, value);
        }

        #endregion

        #region SectionDescription : string - Описание секции

        /// <summary>Описание секции</summary>
        private string _SectionDescription;

        /// <summary>Описание секции</summary>
        public string SectionDescription
        {
            get => _SectionDescription;
            set => Set(ref _SectionDescription, value);
        }

        #endregion


        #region Text : string - Test

        /// <summary>Test</summary>
        private string _Text;

        /// <summary>Test</summary>
        public string Text
        {
            get => _Text;
            set => Set(ref _Text, value);
        }

        #endregion

        
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WPR;
using WPR.MVVM.Validation;
using WPRMebel.WPF.Services.Interfaces;

namespace WPRMebel.WPF.Services
{
    internal class UserDialogService : IUserDialog
    {
        private static Window ActiveWindow => App.ActiveWindow;

        public Task<bool> QuestionAsync(string Caption, string Title = default) => WPRMessageBox.QuestionAsync(ActiveWindow, Caption, Title);

        public Task InformationAsync(string Caption, string Title = default) => WPRMessageBox.InformationAsync(ActiveWindow, Caption, Title);

        public void ShowBubbleMessage(string Message) =>
            WPRMessageBox.Bubble(ActiveWindow, Message);

        public bool ShowOpenFileDialog(out string FileName, string Filter)
        {
            throw new NotImplementedException();
        }

        public bool ShowSaveFileDialog(out string FileName, string Filter, string InitFileName = null)
        {
            throw new NotImplementedException();
        }

        public bool ShowFolderSelectDialog(out string SelectedPath)
        {
            throw new NotImplementedException();
        }

        public Task<string> InputTextAsync(string Caption, string DefaultText) =>
            WPRMessageBox.InputTextAsync(ActiveWindow, Caption, DefaultText);

        public Task<string> InputTextValidatedAsync(string Caption, string DefaultText, IEnumerable<PredicateValidationRule<string>> ValidationRules) =>
            WPRMessageBox.InputTextAsync(ActiveWindow, Caption, DefaultText, ValidationRules);

        public Task<bool> ShowCustomDialogAsync(IWPRDialog DialogContent, bool StaysOpen) =>
            WPRMessageBox.ShowCustomDialogAsync(ActiveWindow, DialogContent, StaysOpen);
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;
using WPR;
using WPR.MVVM.Validation;

namespace WPRMebel.WPF.Services.Interfaces
{
    internal interface IUserDialog
    {
        Task<bool> QuestionAsync(string Caption, string Title = default);

        Task InformationAsync(string Caption, string Title = default);

        void ShowBubbleMessage(string Message);

        bool ShowOpenFileDialog(out string FileName, string Filter);

        bool ShowSaveFileDialog(out string FileName, string Filter, string InitFileName = null);

        bool ShowFolderSelectDialog(out string SelectedPath);

        Task<string> InputTextAsync(string Caption, string DefaultText);

        Task<string> InputTextValidatedAsync(string Caption, string DefaultText, IEnumerable<PredicateValidationRule<string>> ValidationRules);
      
        Task<bool> ShowCustomDialogAsync(IWPRDialog DialogContent, bool StaysOpen);
    }
}
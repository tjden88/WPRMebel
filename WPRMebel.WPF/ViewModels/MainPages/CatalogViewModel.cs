using System.Threading.Tasks;
using System.Windows.Data;
using WPR.MVVM.Commands;
using WPR.MVVM.ViewModels;
using WPRMebel.Domain.Base.Catalog;
using WPRMebel.WpfAPI.Interfaces;

namespace WPRMebel.WPF.ViewModels.MainPages
{
    internal class CatalogViewModel : ViewModel
    {
        private readonly ICatalogDbViewer<Section> _SectionViewer;

        public CatalogViewModel(ICatalogDbViewer<Section> SectionViewer)
        {
            _SectionViewer = SectionViewer;
            //RefreshDataCommand.Execute();
        }

        #region SectionsView : CollectionViewSource - Секции каталога

        /// <summary>Секции каталога</summary>
        private CollectionViewSource _SectionsView;

        /// <summary>Секции каталога</summary>
        public CollectionViewSource SectionsView => _SectionsView ??= LoadData().Result;

        private async Task<CollectionViewSource> LoadData()
        {
            var oc = await _SectionViewer.LoadItemsAsync().ConfigureAwait(false);
            return new CollectionViewSource {Source = oc};
        }

        #endregion

        #region Command RefreshDataCommand - Обновить данные из БД

        /// <summary>Обновить данные из БД</summary>
        private Command _RefreshDataCommand;

        /// <summary>Обновить данные из БД</summary>
        public Command RefreshDataCommand => _RefreshDataCommand
            ??= new Command(OnRefreshDataCommandExecuted, CanRefreshDataCommandExecute, "Обновить данные из БД");

        /// <summary>Проверка возможности выполнения - Обновить данные из БД</summary>
        private bool CanRefreshDataCommandExecute() => true;

        /// <summary>Логика выполнения - Обновить данные из БД</summary>
        private void OnRefreshDataCommandExecuted()
        {
            SectionsView.View.Refresh();
        }

        #endregion

    }
}

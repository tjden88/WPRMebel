using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WPR.MVVM.Commands;
using WPR.MVVM.ViewModels;
using WPRMebel.Domain.Base.Catalog;
using WPRMebel.Domain.Base.Catalog.Abstract;
using WPRMebel.WpfAPI.Interfaces;

namespace WPRMebel.WPF.ViewModels.MainPages
{
    internal class CatalogViewModel : ViewModel
    {
        private readonly ICatalogDbRepository<Section> _SectionRepository;
        private readonly ICatalogDbRepository<CatalogElement> _ElementRepository;

        public CatalogViewModel(ICatalogDbRepository<Section> SectionRepository, ICatalogDbRepository<CatalogElement> ElementRepository)
        {
            _SectionRepository = SectionRepository;
            _ElementRepository = ElementRepository;
            LoadDataCommand.Execute();
        }

        #region ObservableCollections - Коллекции каталога

        /// <summary>Разделы каталога</summary>
        public ObservableCollection<Section> Sections { get; set; } = new();

        #endregion

        #region SelectedSection : Section - Выбранный раздел каталога

        /// <summary>Выбранный раздел каталога</summary>
        private Section _SelectedSection;

        /// <summary>Выбранный раздел каталога</summary>
        public Section SelectedSection
        {
            get => _SelectedSection;
            set => Set(ref _SelectedSection, value);
        }

        #endregion


        #region AsyncCommand LoadDataCommand - Загрузить данные

        /// <summary>Загрузить данные</summary>
        private AsyncCommand _LoadDataCommand;

        /// <summary>Загрузить данные</summary>
        public AsyncCommand LoadDataCommand => _LoadDataCommand
            ??= new AsyncCommand(OnLoadDataCommandExecutedAsync, CanLoadDataCommandExecute, "Загрузить данные");

        /// <summary>Проверка возможности выполнения - Загрузить данные</summary>
        private bool CanLoadDataCommandExecute() => true;

        /// <summary>Логика выполнения - Загрузить данные</summary>
        private async void OnLoadDataCommandExecutedAsync(CancellationToken cancel)
        {
            var array = await _SectionRepository.GetAllAsync(cancel);

            Sections = new(array);
            OnPropertyChanged(nameof(Sections));

        }

        #endregion
    }
}

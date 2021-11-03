using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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

        #region Commands

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
            Sections = await _SectionRepository.GetAllAsync(cancel);
        }

        #endregion

        #region Command ShowAllCatalogCommand - Показать весь каталог

        /// <summary>Показать весь каталог</summary>
        private Command _ShowAllCatalogCommand;

        /// <summary>Показать весь каталог</summary>
        public Command ShowAllCatalogCommand => _ShowAllCatalogCommand
            ??= new Command(OnShowAllCatalogCommandExecuted, CanShowAllCatalogCommandExecute, "Показать весь каталог");

        /// <summary>Проверка возможности выполнения - Показать весь каталог</summary>
        private bool CanShowAllCatalogCommandExecute() => true;

        /// <summary>Логика выполнения - Показать весь каталог</summary>
        private void OnShowAllCatalogCommandExecuted()
        {
            SelectedSection = null;
        }

        #endregion

        #endregion

        #region Lists

        #region Sections : IEnumerable<Section> - Секции каталога

        /// <summary>Секции каталога</summary>
        private IEnumerable<Section> _Sections;

        /// <summary>Секции каталога</summary>
        public IEnumerable<Section> Sections
        {
            get => _Sections;
            set => Set(ref _Sections, value);
        }

        #endregion

        #region ElementsView : IEnumerable<CatalogElement> - Отображаемая коллекция элементов

        /// <summary>Отображаемая коллекция элементов</summary>
        private IEnumerable<CatalogElement> _ElementsView;

        /// <summary>Отображаемая коллекция элементов</summary>
        public IEnumerable<CatalogElement> ElementsView
        {
            get => _ElementsView;
            set => Set(ref _ElementsView, value);
        }

        #endregion


        #endregion

        #region SelectedSection : Section - Выбранный раздел каталога

        /// <summary>Выбранный раздел каталога</summary>
        private Section _SelectedSection;

        /// <summary>Выбранный раздел каталога</summary>
        public Section SelectedSection
        {
            get => _SelectedSection;
            set => IfSet(ref _SelectedSection, value)
                .Then(LoadElements)
            ;
        }



        #endregion

        #region IsNowDataLoading : bool - Индикатор загрузки данных

        /// <summary>Индикатор загрузки данных</summary>
        private bool _IsNowDataLoading;

        /// <summary>Индикатор загрузки данных</summary>
        public bool IsNowDataLoading
        {
            get => _IsNowDataLoading;
            set => Set(ref _IsNowDataLoading, value);
        }

        #endregion

        private async void LoadElements(Section s)
        {
            IsNowDataLoading = true;
            var query = s != null
                ? _ElementRepository.Items
                    .Where(e => e.Category.Section == s)
                    .OrderBy(e => e.Name)
                : _ElementRepository.Items;

            ElementsView = await query.ToArrayAsync().ConfigureAwait(false);
            IsNowDataLoading = false;
        }
    }
}

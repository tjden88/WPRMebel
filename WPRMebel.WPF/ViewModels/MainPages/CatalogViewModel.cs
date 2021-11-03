﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
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
        private readonly ICatalogDbRepository<Category> _CategoriesRepository;

        public CatalogViewModel(ICatalogDbRepository<Section> SectionRepository, ICatalogDbRepository<CatalogElement> ElementRepository, ICatalogDbRepository<Category> CategoriesRepository)
        {
            _SectionRepository = SectionRepository;
            _ElementRepository = ElementRepository;
            _CategoriesRepository = CategoriesRepository;
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

        #region Categories : ObservableCollection<Category> - Список отображаемых категорий

        /// <summary>Список отображаемых категорий</summary>
        private ObservableCollection<Category> _Categories = new();

        /// <summary>Список отображаемых категорий</summary>
        public ObservableCollection<Category> Categories
        {
            get => _Categories;
            set => Set(ref _Categories, value);
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
                .Then(LoadCategories)
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

        // Загрузить категории в зависимости от выбранного раздела
        private async void LoadCategories(Section s)
        {
            IsNowDataLoading = true;
            //var query = s != null
            //    ? _ElementRepository.Items
            //        .Where(e => e.Category.Section == s)
            //        .OrderBy(e => e.Name)
            //    : _ElementRepository.Items;

            //ElementsView = await query.ToArrayAsync().ConfigureAwait(false);

            var query = s != null
                ? _CategoriesRepository.Items
                    .Where(cat => cat.Section == s)
                : _CategoriesRepository.Items;

            var result = await query.ToArrayAsync().ConfigureAwait(true);
            Categories.Clear();
            foreach (var category in result)
                Categories.Add(category);


            IsNowDataLoading = false;
        }
    }
}

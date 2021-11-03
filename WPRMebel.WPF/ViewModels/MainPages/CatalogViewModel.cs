﻿using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using Microsoft.EntityFrameworkCore;
using WPR.MVVM.Commands;
using WPR.MVVM.ViewModels;
using WPRMebel.Domain.Base.Catalog;
using WPRMebel.Domain.Base.Catalog.Abstract;
using WPRMebel.WPF.Extensions;
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

        #region Command LoadDataCommand - Загрузить данные

        /// <summary>Загрузить данные</summary>
        private Command _LoadDataCommand;

        /// <summary>Загрузить данные</summary>
        public Command LoadDataCommand => _LoadDataCommand
            ??= new Command(OnLoadDataCommandExecutedAsync, CanLoadDataCommandExecute, "Загрузить данные");

        /// <summary>Проверка возможности выполнения - Загрузить данные</summary>
        private bool CanLoadDataCommandExecute() => true;

        /// <summary>Логика выполнения - Загрузить данные</summary>
        private async void OnLoadDataCommandExecutedAsync()
        {
            Sections.AddClear(await _SectionRepository.GetAllAsync());
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

        #region Sections : ObservableCollection<Section> - Секции каталога

        /// <summary>Секции каталога</summary>
        private ObservableCollection<Section> _Sections = new();

        /// <summary>Секции каталога</summary>
        public ObservableCollection<Section> Sections
        {
            get => _Sections;
            set => Set(ref _Sections, value);
        }

        #endregion

        #region Elements : ObservableCollection<CatalogElement> - Отображаемая коллекция элементов

        /// <summary>Отображаемая коллекция элементов</summary>
        private ObservableCollection<CatalogElement> _Elements = new();

        /// <summary>Отображаемая коллекция элементов</summary>
        public ObservableCollection<CatalogElement> Elements
        {
            get => _Elements;
            set => Set(ref _Elements, value);
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
            var query = s != null
                ? _ElementRepository.Items
                    .Where(e => e.Category.Section == s)
                : _ElementRepository.Items;

            //Elements = await query.ToArrayAsync().ConfigureAwait(false);

            //var query = s != null
            //    ? _CategoriesRepository.Items
            //        .Where(cat => cat.Section == s)
            //    : _CategoriesRepository.Items;

            var result = await query.ToArrayAsync().ConfigureAwait(true);

            Elements.AddClear(result);
            IsNowDataLoading = false;
        }

        #region ElementsFilter

        #region ElementsFilterText : string - Текст для фильтрации элементов

        /// <summary>Текст для фильтрации элементов</summary>
        private string _ElementsFilterText;

        /// <summary>Текст для фильтрации элементов</summary>
        public string ElementsFilterText
        {
            get => _ElementsFilterText;
            set => IfSet(ref _ElementsFilterText, value)
                .Then(ElementsFilter.RefreshSource);
        }

        #endregion

        #region ElementsFilter : CollectionViewSourceFilter - Фильтр элементов

        /// <summary>Фильтр элементов</summary>
        private CollectionViewSourceFilter _ElementsFilter;

        /// <summary>Фильтр элементов</summary>
        public CollectionViewSourceFilter ElementsFilter =>
            _ElementsFilter ??= new CollectionViewSourceFilter(OnElementsFilter);

        #endregion

        private void OnElementsFilter(FilterEventArgs e)
        {
            var text = ElementsFilterText;
            if (string.IsNullOrEmpty(text) || e.Item is not CatalogElement element)
            {
                e.Accepted = true;
                return;
            }

            e.Accepted = element.Name.Contains(text, StringComparison.OrdinalIgnoreCase) ||
                         element.Category.Name.Contains(text, StringComparison.OrdinalIgnoreCase);
        }

        #endregion

    }
}

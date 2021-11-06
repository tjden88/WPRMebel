using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Data;
using Microsoft.EntityFrameworkCore;
using WPR.MVVM.Commands;
using WPR.MVVM.Validation;
using WPR.MVVM.ViewModels;
using WPRMebel.Domain.Base.Catalog;
using WPRMebel.Domain.Base.Catalog.Abstract;
using WPRMebel.WPF.Extensions;
using WPRMebel.WPF.Services.Interfaces;
using WPRMebel.WPF.ViewModels.Dialogs;
using WPRMebel.WpfAPI.Catalog;

namespace WPRMebel.WPF.ViewModels.MainPages
{
    /// <summary> Изначальная фильтрация просмотра элементов каталога </summary>
    public enum CatalogViewRootGrouping
    {
        None,
        SectionFilter,
        VendorFilter,
        TagFilter,
        Search
    }
    internal class CatalogViewModel : ViewModel
    {

        private readonly CatalogViewer _CatalogViewer;
        private readonly IUserDialog _UserDialog;

        public CatalogViewModel(CatalogViewer CatalogViewer, IUserDialog UserDialog)
        {
            _CatalogViewer = CatalogViewer;
            _CatalogViewer.IsNowDataLoadingChanged += _ => OnPropertyChanged(nameof(IsNowDataLoading));
            _UserDialog = UserDialog;
            LoadStartData();
        }


        #region Load
        private async void LoadStartData()
        {
            Sections.AddClear(await _CatalogViewer.LoadSections());
            Vendors.AddClear(await _CatalogViewer.LoadVendors());

            if (IsDesignMode)
            {
                Sections.Clear();
                Elements.Add(new Fitting() {Id = 1, Name = "Test"});
            }
        }


        // Загрузить элементы и категории в зависимости от выбранного раздела или поставщика
        private async void LoadElements(Section s)
        {
            if (s == null) return;
            var cat = await _CatalogViewer.GetCategories(c => c.Section == s);

            CategoriesNames.AddClear(cat.Select( c => c.Name).OrderBy(str => str));
            var result = await _CatalogViewer.GetElements(e => e.Category.Section == s);
            Elements.AddClear(result);
        }
        private async void LoadElements(Vendor v)
        {
            if (v == null) return;
            var cat = await _CatalogViewer.GetCategories(c => c.Vendor == v);

            CategoriesNames.AddClear(cat.Select(c => c.Name).OrderBy(str => str));
            var result = await _CatalogViewer.GetElements(e => e.Category.Vendor == v);
            Elements.AddClear(result);
        }

        #endregion

        #region Commands


        #region Command CreateNewSectionCommand - Создать секцию каталога

        /// <summary>Создать секцию каталога</summary>
        private Command _CreateNewSectionCommand;

        /// <summary>Создать секцию каталога</summary>
        public Command CreateNewSectionCommand => _CreateNewSectionCommand
            ??= new Command(OnCreateNewSectionCommandExecuted, CanCreateNewSectionCommandExecute, "Создать секцию каталога");

        /// <summary>Проверка возможности выполнения - Создать секцию каталога</summary>
        private bool CanCreateNewSectionCommandExecute() => true;

        /// <summary>Логика выполнения - Создать секцию каталога</summary>
        private async void OnCreateNewSectionCommandExecuted()
        {
            var dialog = new EditCatalogSectionDialogViewModel(true, Sections.Select(s => s.Name).ToArray());
            if (!await _UserDialog.ShowCustomDialogAsync(dialog, false)) return;

            var newSection = new Section
            {
                Name = dialog.SectionName?.Trim(),
                Description = dialog.SectionDescription?.Trim()
            };
            var returned = await _CatalogViewer.AddSection(newSection);
            if (returned == null)
            {
                await _UserDialog.InformationAsync("Ошибка добавления раздела");
                return;
            }
            Sections.Add(returned);
        }

        #endregion

        #region Command EditSectionCommand - Редактировать секцию

        /// <summary>Редактировать секцию</summary>
        private Command _EditSectionCommand;

        /// <summary>Редактировать секцию</summary>
        public Command EditSectionCommand => _EditSectionCommand
            ??= new Command(OnEditSectionCommandExecuted, CanEditSectionCommandExecute, "Редактировать секцию");

        /// <summary>Проверка возможности выполнения - Редактировать секцию</summary>
        private bool CanEditSectionCommandExecute() => SelectedSection != null;

        /// <summary>Логика выполнения - Редактировать секцию</summary>
        private async void OnEditSectionCommandExecuted()
        {
            var dialog = new EditCatalogSectionDialogViewModel(false, Sections
                .Select(s => s.Name)
                .Where(str => str != SelectedSection.Name)
                .ToArray())
            {
                SectionName = SelectedSection.Name,
                SectionDescription = SelectedSection.Description
            };

            if (!await _UserDialog.ShowCustomDialogAsync(dialog, false)) return;

            SelectedSection.Name = dialog.SectionName?.Trim();
            SelectedSection.Description = dialog.SectionDescription?.Trim();

            var returned = await _CatalogViewer.UpdateSection(SelectedSection);

            if (!returned)
            {
                await _UserDialog.InformationAsync("Ошибка изменения раздела");
                return;
            }

            CollectionViewSource.GetDefaultView(Sections).Refresh();
            OnPropertyChanged(nameof(SelectedSection));
        }

        #endregion

        #region Command DeleteSectionCommand - Удалить секцию

        /// <summary>Удалить секцию</summary>
        private Command _DeleteSectionCommand;

        /// <summary>Удалить секцию</summary>
        public Command DeleteSectionCommand => _DeleteSectionCommand
            ??= new Command(OnDeleteSectionCommandExecuted, CanEditSectionCommandExecute, "Удалить секцию");

        /// <summary>Логика выполнения - Удалить секцию</summary>
        private async void OnDeleteSectionCommandExecuted()
        {
            if (await _UserDialog.QuestionAsync($"Удалить раздел каталога {SelectedSection.Name}?\n" +
                                                "Также будут удалены все связанные категории и элементы каталога в них!\n" +
                                                "Это действие отменить нельзя.", "Внимание!"))
            {
                var result = await _CatalogViewer.DeleteSection(SelectedSection);
                if (!result)
                {
                    await _UserDialog.InformationAsync("Ошибка удаления раздела");
                    return;
                }

                var sIndex = Sections.IndexOf(SelectedSection);
                Sections.Remove(SelectedSection);
                SelectedSection = Sections.Count > 0
                    ? sIndex > 0
                    ? Sections[sIndex - 1]
                    : Sections[0]
                    : null;
            }
        }

        #endregion

        #region Command SetRootGroupingCommand : CatalogViewRootGrouping - Установить изначальный фильтр

        /// <summary>Установить изначальный фильтр</summary>
        private Command _SetRootGroupingCommand;

        /// <summary>Установить изначальный фильтр</summary>
        public Command SetRootGroupingCommand => _SetRootGroupingCommand
            ??= new Command(OnSetRootGroupingCommandExecuted, CanSetRootGroupingCommandExecute, "Установить изначальный фильтр");

        /// <summary>Проверка возможности выполнения - Установить изначальный фильтр</summary>
        private bool CanSetRootGroupingCommandExecute(object p) => p is CatalogViewRootGrouping cg && cg != RootGrouping;

        /// <summary>Проверка возможности выполнения - Установить изначальный фильтр</summary>
        private void OnSetRootGroupingCommandExecuted(object p) => RootGrouping = (CatalogViewRootGrouping) p;

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

        #region Vendors : ObservableCollection<Vendor> - Поставщики

        /// <summary>Поставщики</summary>
        private ObservableCollection<Vendor> _Vendors = new();

        /// <summary>Поставщики</summary>
        public ObservableCollection<Vendor> Vendors
        {
            get => _Vendors;
            set => Set(ref _Vendors, value);
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

        #region CategoriesNames : ObservableCollection<string> - Список отображаемых категорий

        /// <summary>Список отображаемых категорий</summary>
        private ObservableCollection<string> _CategoriesNames = new();

        /// <summary>Список отображаемых категорий</summary>
        public ObservableCollection<string> CategoriesNames
        {
            get => _CategoriesNames;
            set => Set(ref _CategoriesNames, value);
        }

        #endregion

        #endregion

        #region SelectedItems

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

        #region SelectedVendor : Vendor - Выбранный поставщик

        /// <summary>Выбранный поставщик</summary>
        private Vendor _SelectedVendor;

        /// <summary>Выбранный поставщик</summary>
        public Vendor SelectedVendor
        {
            get => _SelectedVendor;
            set => IfSet(ref _SelectedVendor, value)
                .Then(LoadElements)
            ;
        }

        #endregion

        #region SelectedCategoryName : string - Выбранная категория

        /// <summary>Выбранная категория</summary>
        private string _SelectedCategoryName;

        /// <summary>Выбранная категория</summary>
        public string SelectedCategoryName
        {
            get => _SelectedCategoryName;
            set => IfSet(ref _SelectedCategoryName, value)
                .Then(ElementsFilter.RefreshSourceNow)
                ;
        }

        #endregion

        #endregion

        #region RootGrouping : CatalogViewRootGrouping - Изначальный фильтр отображаемых элементов

        /// <summary>Изначальный фильтр отображаемых элементов</summary>
        private CatalogViewRootGrouping _RootGrouping = CatalogViewRootGrouping.SectionFilter;

        /// <summary>Изначальный фильтр отображаемых элементов</summary>
        public CatalogViewRootGrouping RootGrouping
        {
            get => _RootGrouping;
            set => IfSet(ref _RootGrouping, value)
                .Then(gr =>
                {
                    if (gr == CatalogViewRootGrouping.SectionFilter) LoadElements(SelectedSection);
                    if (gr == CatalogViewRootGrouping.VendorFilter) LoadElements(SelectedVendor);
                })
            ;
        }

        #endregion

        #region IsNowDataLoading : bool - Индикатор загрузки данных
        /// <summary>Индикатор загрузки данных</summary>
        public bool IsNowDataLoading => _CatalogViewer.IsNowDataLoading;

        #endregion

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
            _ElementsFilter ??= new CollectionViewSourceFilter(OnElementsFilter) { DelayBeforeRefresh = 200 };

        #endregion

        private void OnElementsFilter(FilterEventArgs e)
        {
            if (e.Item is not CatalogElement element) return;

            // Категории
            var catName = SelectedCategoryName;
            if (catName != null && element.Category.Name != catName)
            {
                e.Accepted = false;
                return;
            }

            var text = ElementsFilterText?.Trim();
            if (string.IsNullOrEmpty(text))
            {
                e.Accepted = true;
                return;
            }

            var checkString = string.Join(";", element.Name, element.Category.Name);

            e.Accepted = checkString.Contains(text, StringComparison.OrdinalIgnoreCase);
        }

        #endregion

        #region SearchInCatalog

        #region SearchText : string - Строка поиска в каталоге

        /// <summary>Строка поиска в каталоге</summary>
        private string _SearchText;

        /// <summary>Строка поиска в каталоге</summary>
        public string SearchText
        {
            get => _SearchText;
            set => Set(ref _SearchText, value);
        }

        #endregion

        #region Command SearchInCatalogCommand - Найти в каталоге

        /// <summary>Найти в каталоге</summary>
        private Command _SearchInCatalogCommand;

        /// <summary>Найти в каталоге</summary>
        public Command SearchInCatalogCommand => _SearchInCatalogCommand
            ??= new Command(OnSearchInCatalogCommandExecuted, CanSearchInCatalogCommandExecute, "Найти в каталоге");

        /// <summary>Проверка возможности выполнения - Найти в каталоге</summary>
        private bool CanSearchInCatalogCommandExecute() => !string.IsNullOrWhiteSpace(SearchText);

        /// <summary>Логика выполнения - Найти в каталоге</summary>
        private async void OnSearchInCatalogCommandExecuted()
        {
            var searchText = SearchText.Trim();
            SelectedSection = null;
            SelectedVendor = null;

           var elements = await _CatalogViewer.SearchElements(searchText);
           CategoriesNames.AddClear( elements.Select(e => e.Category.Name).Distinct());

            Elements.AddClear(elements);
            SearchText = string.Empty;
        }

        #endregion
        

        #endregion

    }
}

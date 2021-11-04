using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Data;
using WPR.MVVM.Commands;
using WPR.MVVM.ViewModels;
using WPRMebel.Domain.Base.Catalog;
using WPRMebel.Domain.Base.Catalog.Abstract;
using WPRMebel.WPF.Extensions;
using WPRMebel.WPF.Services.Interfaces;
using WPRMebel.WPF.ViewModels.Dialogs;
using WPRMebel.WpfAPI.Catalog;

namespace WPRMebel.WPF.ViewModels.MainPages
{
    internal class CatalogViewModel : ViewModel
    {
        private readonly CatalogViewer _CatalogViewer;
        private readonly IUserDialog _UserDialog;

        public CatalogViewModel(CatalogViewer CatalogViewer, IUserDialog UserDialog)
        {
            _CatalogViewer = CatalogViewer;
            _CatalogViewer.IsNowDataLoadingChanged += _ => OnPropertyChanged(nameof(IsNowDataLoading));

            _UserDialog = UserDialog;
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
            Sections.AddClear(await _CatalogViewer.LoadSections());
            //Sections.AddClear(await _SectionRepository.GetAllAsync());
            //LoadCategories(SelectedSection);
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
            OnPropertyChanged(nameof(SelectedSectionIsNotNull));
        }

        #endregion

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
        private bool CanEditSectionCommandExecute() => SelectedSectionIsNotNull;

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

        #endregion

        #region Lists

        public bool SelectedSectionIsNotNull => SelectedSection != null;

        #region Sections : RangeObservableCollection<Section> - Секции каталога

        /// <summary>Секции каталога</summary>
        private RangeObservableCollection<Section> _Sections = new();

        /// <summary>Секции каталога</summary>
        public RangeObservableCollection<Section> Sections
        {
            get => _Sections;
            set => Set(ref _Sections, value);
        }

        #endregion

        #region Elements : RangeObservableCollection<CatalogElement> - Отображаемая коллекция элементов

        /// <summary>Отображаемая коллекция элементов</summary>
        private RangeObservableCollection<CatalogElement> _Elements = new();

        /// <summary>Отображаемая коллекция элементов</summary>
        public RangeObservableCollection<CatalogElement> Elements
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
        private Section _SelectedSection = new();

        /// <summary>Выбранный раздел каталога</summary>
        public Section SelectedSection
        {
            get => _SelectedSection;
            set => IfSet(ref _SelectedSection, value)
                .CallPropertyChanged(nameof(SelectedSectionIsNotNull))
                .Then(LoadCategories)
            ;
        }

        #endregion

        #region IsNowDataLoading : bool - Индикатор загрузки данных
        /// <summary>Индикатор загрузки данных</summary>
        public bool IsNowDataLoading => _CatalogViewer.IsNowDataLoading;

        #endregion


        // Загрузить категории в зависимости от выбранного раздела
        private async void LoadCategories(Section s)
        {
            var result = s == null 
                ? await _CatalogViewer.GetElements() 
                : await _CatalogViewer.GetElements(e => e.Category.Section == s);
            Elements.AddRangeClear(result);
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
            _ElementsFilter ??= new CollectionViewSourceFilter(OnElementsFilter) { DelayBeforeRefresh = 200 };

        #endregion

        private void OnElementsFilter(FilterEventArgs e)
        {
            var text = ElementsFilterText;
            if (string.IsNullOrEmpty(text) || e.Item is not CatalogElement element)
            {
                e.Accepted = true;
                return;
            }

            var checkString = string.Join(";", element.Name, element.Category.Name);

            e.Accepted = checkString.Contains(text, StringComparison.OrdinalIgnoreCase);
        }

        #endregion

    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WPRMebel.Domain.Base.Catalog;
using WPRMebel.Domain.Base.Catalog.Abstract;
using WPRMebel.Interfaces.Base.Entityes;
using WPRMebel.WpfAPI.Interfaces;

namespace WPRMebel.WpfAPI.Catalog
{
    /// <summary>
    /// Просмотр и изменение данных БД каталога
    /// </summary>
    public class CatalogViewer
    {
        private readonly ICatalogDbRepository<Section> _SectionRepository;
        private readonly ICatalogDbRepository<CatalogElement> _ElementRepository;
        private readonly ICatalogDbRepository<Category> _CategoriesRepository;

        public CatalogViewer(ICatalogDbRepository<Section> SectionRepository,
            ICatalogDbRepository<CatalogElement> ElementRepository,
            ICatalogDbRepository<Category> CategoriesRepository)
        {
            _SectionRepository = SectionRepository;
            _ElementRepository = ElementRepository;
            _CategoriesRepository = CategoriesRepository;
        }

        public Action<bool> IsNowDataLoadingChanged { get; set; }

        #region IsNowDataLoading : bool - Выполняется запрос к БД

        /// <summary>Выполняется запрос к БД</summary>
        private bool _IsNowDataLoading;

        /// <summary>Выполняется запрос к БД</summary>
        public bool IsNowDataLoading
        {
            get => _IsNowDataLoading;
            set
            {
                if (Equals(value, _IsNowDataLoading)) return;
                _IsNowDataLoading = value;
                IsNowDataLoadingChanged?.Invoke(value);
            }
        }

        #endregion

        #region LoadData

        // Потокобезопасный метод загрузки данных из БД
        private async Task<IEnumerable<T>> LoadData<T>(IQueryable<T> query) where T : IEntity
        {
            if (IsNowDataLoading) return Enumerable.Empty<T>();
            IsNowDataLoading = true;
            var result = await query.ToArrayAsync();
            IsNowDataLoading = false;
            return result;
        }

        /// <summary> Загрузить все разделы </summary>
        public Task<IEnumerable<Section>> LoadSections() => LoadData(_SectionRepository.Items);

        /// <summary> Загрузить категории раздела </summary>
        public Task<IEnumerable<Category>> LoadCategories([MaybeNull] Section Section)
        {
            var query = Section != null
                ? _CategoriesRepository.Items
                    .Where(cat => cat.Section == Section)
                : _CategoriesRepository.Items;
            return LoadData(query);
        }

        /// <summary> Загрузить элементы каталога раздела </summary>
        public Task<IEnumerable<CatalogElement>> LoadCatalogElements([MaybeNull] Section Section)
        {
            var query = Section != null
                ? _ElementRepository.Items
                    .Where(e => e.Category.Section == Section)
                : _ElementRepository.Items;
            return LoadData(query);
        }

        #endregion


        private async Task<T> GrudEntity<T>(Task<T> task)
        {
            if (IsNowDataLoading) return default;
            IsNowDataLoading = true;
            var result = await task;
            IsNowDataLoading = false;
            return result;
        }

        #region Sections

        public Task<Section> AddSection(Section NewSection) => GrudEntity(_SectionRepository.AddAsync(NewSection));
        public Task<bool> UpdateSection(Section ChangedSection) => GrudEntity(_SectionRepository.UpdateAsync(ChangedSection));

        #endregion
    }
}

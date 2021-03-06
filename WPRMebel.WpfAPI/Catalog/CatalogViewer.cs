using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
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
        private readonly ICatalogDbRepository<Vendor> _VenorsRepository;

        public CatalogViewer(ICatalogDbRepository<Section> SectionRepository,
            ICatalogDbRepository<CatalogElement> ElementRepository,
            ICatalogDbRepository<Category> CategoriesRepository,
            ICatalogDbRepository<Vendor> VenorsRepository)
        {
            _SectionRepository = SectionRepository;
            _ElementRepository = ElementRepository;
            _CategoriesRepository = CategoriesRepository;
            _VenorsRepository = VenorsRepository;
        }

        /// <summary> Происходит при изменении статуса запроса к БД </summary>
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
        private async Task<T[]> LoadData<T>(IQueryable<T> query) where T : IEntity
        {
            if (IsNowDataLoading) return Array.Empty<T>();
            IsNowDataLoading = true;
            var result = await query.ToArrayAsync();
            IsNowDataLoading = false;
            return result;
        }

        /// <summary> Загрузить все разделы </summary>
        public Task<Section[]> LoadSections() => LoadData(_SectionRepository.Items);

        /// <summary> Загрузить всех поставщиков </summary>
        public Task<Vendor[]> LoadVendors() => LoadData(_VenorsRepository.Items.OrderBy( v => v.Name));


        /// <summary> Загрузить элементы каталога с фильтрацией </summary>
        public Task<CatalogElement[]> GetElements([MaybeNull] Expression<Func<CatalogElement, bool>> Predicate = null)
        {
            var query = Predicate != null
                ? _ElementRepository.Items
                    .Include(e => e.ChildCatalogElements)
                    .Include(e => e.Category)
                    .Where(Predicate)
                : _ElementRepository.Items;

            return LoadData(query);
        }

        #endregion

        // Работа с сущностями (добавить, изменить, удалить)
        private async Task<T> CrudEntity<T>(Task<T> task)
        {
            if (IsNowDataLoading) return default;
            try
            {
                IsNowDataLoading = true;
                var result = await task;
                IsNowDataLoading = false;
                return result;
            }
            catch (Exception)
            {
                IsNowDataLoading = false;
                return default;
            }
        }

        #region Sections

        public Task<Section> AddSection(Section NewSection) => CrudEntity(_SectionRepository.AddAsync(NewSection));
        public Task<bool> UpdateSection(Section ChangedSection) => CrudEntity(_SectionRepository.UpdateAsync(ChangedSection));
        public Task<bool> DeleteSection(Section DeletedSection) => CrudEntity(_SectionRepository.DeleteAsync(DeletedSection));

        #endregion

        public Task<CatalogElement[]> SearchElements(string SearchPattern)
        {
            var query = _ElementRepository.Items
                .Include(e => e.ChildCatalogElements)
                .Include(e => e.Category)
                .Where(e => EF.Functions
                    .Like(e.Name, $"%{SearchPattern}%"));

            return LoadData(query);
        }
    }
}

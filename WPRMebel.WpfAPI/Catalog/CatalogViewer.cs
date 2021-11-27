using System;
using System.Collections.Generic;
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

        #region LoadData

        /// <summary> Загрузить все разделы </summary>
        public IEnumerable<Section> LoadSections() => _SectionRepository.GetAll();

        /// <summary> Загрузить всех поставщиков </summary>
        public IEnumerable<Vendor> LoadVendors() => _VenorsRepository.GetAll().OrderBy( v => v.Name);


        /// <summary> Загрузить элементы каталога с фильтрацией </summary>
        public IEnumerable<CatalogElement> GetElements([MaybeNull] Expression<Func<CatalogElement, bool>> Predicate = null)
        {
            var query = Predicate != null
                ? _ElementRepository.Items
                    .Include(e => e.ChildCatalogElements)
                    .Include(e => e.Category)
                    .Where(Predicate)
                : _ElementRepository.Items;

            return query;
        }

        #endregion

        #region Sections

        public Section AddSection(Section NewSection) => _SectionRepository.Add(NewSection);
        public bool UpdateSection(Section ChangedSection) => _SectionRepository.Update(ChangedSection);
        public bool DeleteSection(Section DeletedSection) => _SectionRepository.Delete(DeletedSection);

        #endregion

        public IEnumerable<CatalogElement> SearchElements(string SearchPattern)
        {
            var query = _ElementRepository.Items
                .Include(e => e.ChildCatalogElements)
                .Include(e => e.Category)
                .Where(e => EF.Functions
                    .Like(e.Name, $"%{SearchPattern}%"));

            return query;
        }
    }
}

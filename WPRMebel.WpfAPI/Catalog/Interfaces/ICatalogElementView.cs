using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WPRMebel.Domain.Base.Catalog;
using WPRMebel.Domain.Base.Catalog.Abstract;

namespace WPRMebel.WpfAPI.Catalog.Interfaces
{
    /// <summary>
    /// Сервис просмотра элементов каталога
    /// </summary>
    public interface ICatalogElementView
    {
        Task<CatalogElement> GetById(int Id, CancellationToken Cancel = default);
        Task<CatalogElement> GetByName(string Name, CancellationToken Cancel = default);

        Task<IEnumerable<CatalogElement>> GetFromSection(Section Section, CancellationToken Cancel = default);

        Task<IEnumerable<CatalogElement>> GetFromCategory(Category Category, CancellationToken Cancel = default);
    }

    /// <summary>
    /// Сервис просмотра элементов каталога
    /// </summary>
    public interface ICatalogElementView<T> where T : CatalogElement
    {
        Task<T> GetById(int Id, CancellationToken Cancel = default);
        Task<T> GetByName(string Name, CancellationToken Cancel = default);

        Task<IEnumerable<T>> GetFromSection(Section Section, CancellationToken Cancel = default);

        Task<IEnumerable<T>> GetFromCategory(Category Category, CancellationToken Cancel = default);

    }
}

using System.Collections.Generic;
using WPRMebel.Domain.Base.Catalog;
using WPRMebel.Domain.Base.Catalog.Abstract;

namespace WPRMebel.WpfAPI.Catalog.Interfaces
{
    /// <summary>
    /// Сервис просмотра элементов каталога
    /// </summary>
    public interface ICatalogElementView
    {
        CatalogElement GetById(int Id);
        CatalogElement GetByName(string Name);

        IEnumerable<CatalogElement> GetFromSection(Section Section);

        IEnumerable<CatalogElement> GetFromCategory(Category Category);
    }

    /// <summary>
    /// Сервис просмотра элементов каталога
    /// </summary>
    public interface ICatalogElementView<out T> where T : CatalogElement
    {
        T GetById(int Id);
        T GetByName(string Name);

        IEnumerable<T> GetFromSection(Section Section);

        IEnumerable<T> GetFromCategory(Category Category);

    }
}

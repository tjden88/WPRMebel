using System.Collections.Generic;
using WPRMebel.Domain.Base.Catalog.Abstract;

namespace WPRMebel.Domain.Base.Catalog
{
    /// <summary>
    /// Категория элементов
    /// </summary>
    public class Category : NamedEntity
    {

        public Vendor Vendor
        {
            get => default;
            set
            {
            }
        }

        public Section Section
        {
            get => default;
            set
            {
            }
        }

        public ICollection<CatalogElement> CatalogElements
        {
            get => default;
            set
            {
            }
        }
    }
}
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WPRMebel.Domain.Base.Catalog.Abstract;

namespace WPRMebel.Domain.Base.Catalog
{
    /// <summary>
    /// Категория элементов
    /// </summary>
    public class Category : NamedEntity
    {

        public virtual Vendor Vendor { get; set; }

        [Required]
        public virtual Section Section { get; set; }

        public virtual ICollection<CatalogElement> CatalogElements { get; set; }
    }
}
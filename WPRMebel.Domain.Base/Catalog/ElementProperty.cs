using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WPRMebel.Domain.Base.Catalog.Abstract;

namespace WPRMebel.Domain.Base.Catalog
{
    /// <summary>
    /// Свойства элемента
    /// </summary>
    public class ElementProperty : NamedEntity
    {
        /// <summary>Элемент свойства</summary>
        [Required]
        public virtual CatalogElement CatalogElement { get; set; }

        /// <summary>Коэффициент изменения цены элемента</summary>
        public double PriceChanging { get; set; } = 1d;

        /// <summary>Список значений свойства</summary>
        public virtual ICollection<ElementPropertyValue> ElementPropertyValues { get; set; } =
            new List<ElementPropertyValue>();

        /// <summary>
        /// Указывает, что свойство обязательно для элемента
        /// </summary>
        public bool IsRequired { get; set; }
    }
}

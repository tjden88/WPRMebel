using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace WPRMebel.Domain.Base.Catalog.Abstract
{
    /// <summary>
    /// Сущность с ценой за единицу
    /// </summary>
    /// <remarks>Сущность с ценой за единицу</remarks>
    public abstract class CatalogElement : NamedEntity
    {
        /// <summary>
        /// Базовая цена
        /// </summary>
        public decimal Price { get; set; } = 0m;

        /// <summary>
        /// Коэффициент наценки
        /// </summary>
        public double Extra { get; set; } = 1d;

        [Required]
        public virtual Category Category { get; set; }

        /// <summary> Коллекция свойств элемента </summary>
        public virtual ICollection<ElementProperty> ElementProperties { get; set; } = new HashSet<ElementProperty>();

        /// <summary> Коллекция дочерних элементов </summary>
        [InverseProperty("OwnerCatalogElement")]
        public virtual ICollection<ChildCatalogElement> ChildCatalogElements { get; set; } = new HashSet<ChildCatalogElement>();

        /// <summary> Цена комплекта </summary>
        public decimal TotalPrice => CalculateTotalPrice();

        private decimal CalculateTotalPrice() => Price + ChildCatalogElements
            .Sum(childCatalogElement => childCatalogElement.CatalogElement?
                .Price * childCatalogElement.Quantity ?? 0);
    }
}
using System.ComponentModel.DataAnnotations;

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
    }
}
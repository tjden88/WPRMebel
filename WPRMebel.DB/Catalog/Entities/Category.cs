using System.ComponentModel.DataAnnotations;
using WPRMebel.DB.BaseEntities;

namespace WPRMebel.DB.Catalog.Entities
{
    /// <summary>
    /// Категория элементов каталога
    /// </summary>
    public class Category : NamedEntity
    {
        /// <summary>Поставщик данной категории</summary>
        [Required]
        public Vendor Vendor { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;
using WPRMebel.DB.Entities.Base;

namespace WPRMebel.DB.Entities.Catalog
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
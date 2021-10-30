using System.ComponentModel.DataAnnotations;
using WPRMebel.Entityes.Base;

namespace WPRMebel.Entityes.Catalog
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
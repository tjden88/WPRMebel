using System.Collections.Generic;
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

        /// <summary>Секция каталога категории</summary>
        [Required]
        public virtual Section Section { get; set; }

        /// <summary> Элементы категории </summary>
        public virtual ICollection<Element> Elements { get; set; }
    }
}
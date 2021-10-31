using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WPRMebel.Entityes.Base;

namespace WPRMebel.Entityes.Catalog
{
    /// <summary>
    /// Элемент каталога
    /// </summary>
    public class Element : NamedEntity
    {
        /// <summary>Категория элемента</summary>
        [Required]
        public virtual Category Category { get; set; }

        /// <summary> Стоимость </summary>
        public decimal Price { get; set; }

        /// <summary>Наценка</summary>
        public double ExtraPrice { get; set; } = 1.5d;

        /// <summary>Свойства элемента</summary>
        public virtual ICollection<ElementProperty> ElementProperties { get; set; }
    }
}

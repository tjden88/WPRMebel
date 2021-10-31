using System.Collections.Generic;
using WPRMebel.Entityes.Base;

namespace WPRMebel.Entityes.Catalog
{
    /// <summary>
    /// Свойства элемента
    /// </summary>
    public class ElementProperty : NamedEntity
    {
        /// <summary>Элемент свойства</summary>
        public Element Element { get; set; }

        /// <summary>Коэффициент изменения цены элемента</summary>
        public double PriceChanging { get; set; } = 1d;

        /// <summary>Список значений свойства</summary>
        public virtual ICollection<ElementPropertyValue> ElementPropertyValues { get; set; }

    }
}

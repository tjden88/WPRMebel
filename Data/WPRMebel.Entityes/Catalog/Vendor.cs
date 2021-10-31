using System.Collections.Generic;
using WPRMebel.Entityes.Base;

namespace WPRMebel.Entityes.Catalog
{
    /// <summary>
    /// Поставщик
    /// </summary>
    public class Vendor : NamedEntity
    {
        /// <summary>Описание поставщика</summary>
        public string Description { get; set; }

        /// <summary> Категории элементов поставщика </summary>
        public virtual ICollection<Category> Categories { get; }
    }
}
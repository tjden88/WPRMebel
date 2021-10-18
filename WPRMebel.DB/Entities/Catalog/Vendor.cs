using System.Collections.Generic;
using WPRMebel.DB.Entities.Base;

namespace WPRMebel.DB.Entities.Catalog
{
    /// <summary>
    /// Поставщик
    /// </summary>
    public class Vendor : NamedEntity
    {
        /// <summary>Описание поставщика</summary>
        public string Description { get; set; }

        /// <summary> Категории элементов поставщика </summary>
        public IList<Category> Categories { get; }
    }
}
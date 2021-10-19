using System.Collections.Generic;
using WPRMebel.DB.BaseEntities;

namespace WPRMebel.DB.Catalog.Entities
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
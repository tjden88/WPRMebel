using System.Collections.Generic;
using WPRMebel.Domain.Base.Catalog.Abstract;

namespace WPRMebel.Domain.Base.Catalog
{
    /// <summary>
    /// Раздел каталога
    /// </summary>
    public class Section : NamedEntity
    {
        /// <summary>
        /// Описание раздела
        /// </summary>
        public string Description { get; set; }

        public virtual ICollection<Category> Categories { get; set; }
    }
}
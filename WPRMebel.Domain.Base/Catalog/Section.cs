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
        public string Description
        {
            get => default;
            set
            {
            }
        }

        public ICollection<Category> Categories
        {
            get => default;
            set
            {
            }
        }
    }
}
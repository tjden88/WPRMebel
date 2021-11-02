using System.Collections.Generic;
using WPRMebel.Domain.Base.Catalog.Abstract;

namespace WPRMebel.Domain.Base.Catalog
{
    /// <summary>
    /// Поставщик
    /// </summary>
    public class Vendor : NamedEntity
    {
        public virtual ICollection<Category> Categories
        {
            get => default;
            set
            {
            }
        }
    }
}
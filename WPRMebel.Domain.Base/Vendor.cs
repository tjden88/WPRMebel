using System;
using System.Collections.Generic;
using System.Text;

namespace WPRMebel.Domain.Base
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
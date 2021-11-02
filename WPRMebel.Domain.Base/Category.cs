using System;
using System.Collections.Generic;
using System.Text;

namespace WPRMebel.Domain.Base
{
    /// <summary>
    /// Категория элементов
    /// </summary>
    public class Category : NamedEntity
    {

        public Vendor Vendor
        {
            get => default;
            set
            {
            }
        }

        public Section Section
        {
            get => default;
            set
            {
            }
        }

        public ICollection<CatalogElement> CatalogElements
        {
            get => default;
            set
            {
            }
        }
    }
}
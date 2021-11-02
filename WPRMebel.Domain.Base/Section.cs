using System;
using System.Collections.Generic;
using System.Text;

namespace WPRMebel.Domain.Base
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
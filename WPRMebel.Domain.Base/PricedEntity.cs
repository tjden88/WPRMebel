using System;
using System.Collections.Generic;
using System.Text;

namespace WPRMebel.Domain.Base
{
    /// <summary>
    /// Сущность с ценой за единицу
    /// </summary>
    /// <remarks>Сущность с ценой за единицу</remarks>
    public abstract class CatalogElement : NamedEntity
    {
        /// <summary>
        /// Базовая цена
        /// </summary>
        public decimal Price
        {
            get => default;
            set
            {
            }
        }

        /// <summary>
        /// Коэффициент наценки
        /// </summary>
        public double Extra
        {
            get => default;
            set
            {
            }
        }

        public Category Category
        {
            get => default;
            set
            {
            }
        }
    }
}
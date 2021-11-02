using System;
using System.Collections.Generic;
using System.Text;

namespace WPRMebel.Domain.Base
{
    public abstract class Entity
    {
        /// <summary>
        /// Идентификатор сущности
        /// </summary>
        public int Id
        {
            get => default;
            set
            {
            }
        }
    }
}
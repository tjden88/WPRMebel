using System;
using System.Collections.Generic;
using System.Text;

namespace WPRMebel.Domain.Base
{
    public abstract class NamedEntity : Entity
    {

        /// <summary>
        /// Имя сущности
        /// </summary>
        public string Name
        {
            get => default;
            set
            {
            }
        }
    }
}
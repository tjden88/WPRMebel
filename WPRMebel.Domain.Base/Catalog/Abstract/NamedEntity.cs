using System.ComponentModel.DataAnnotations;
using WPRMebel.Interfaces.Base.Entityes;

namespace WPRMebel.Domain.Base.Catalog.Abstract
{
    public abstract class NamedEntity : Entity, INamedEntity
    {

        /// <summary>
        /// Имя сущности
        /// </summary>
        [Required]
        public string Name { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;
using WPRMebel.Interfaces.Base.Entities;

namespace WPRMebel.DB.Entities.Base
{
    /// <summary>
    /// Базовый класс именованной сущности
    /// </summary>
    public abstract class NamedEntity : Entity, INamedEntity
    {
        [Required]
        public string Name { get; set; }
    }
}
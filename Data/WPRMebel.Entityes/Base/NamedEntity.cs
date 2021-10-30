using System.ComponentModel.DataAnnotations;
using WPRMebel.Interfaces.Base.Entityes;

namespace WPRMebel.Entityes.Base
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
using WPRMebel.Interfaces.Base.Entities;

namespace WPRMebel.DB.Entities.Base
{
    /// <summary>
    /// Базовый класс сущности
    /// </summary>
    public abstract class Entity : IEntity
    {
        public int Id { get; set; }
    }
}

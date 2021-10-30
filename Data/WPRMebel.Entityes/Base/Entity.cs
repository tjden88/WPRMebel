using WPRMebel.Interfaces.Base.Entityes;

namespace WPRMebel.Entityes.Base
{
    /// <summary>
    /// Базовый класс сущности
    /// </summary>
    public abstract class Entity : IEntity
    {
        public int Id { get; set; }
    }
}

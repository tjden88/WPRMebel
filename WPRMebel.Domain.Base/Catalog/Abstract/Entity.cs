using WPRMebel.Interfaces.Base.Entityes;

namespace WPRMebel.Domain.Base.Catalog.Abstract
{
    public abstract class Entity : IEntity
    {
        /// <summary>
        /// Идентификатор сущности
        /// </summary>
        public int Id { get; set; }
    }
}
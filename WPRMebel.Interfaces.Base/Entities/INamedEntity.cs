namespace WPRMebel.Interfaces.Base.Entities
{
    public interface INamedEntity : IEntity
    {
        /// <summary>Наименование сущности</summary>
        public string Name { get; set; }
    }
}
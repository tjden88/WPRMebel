namespace WPRMebel.Interfaces.Base.Entityes
{
    /// <summary>
    /// Базовый интерфейс именованной сущности
    /// </summary>

    public interface INamedEntity : IEntity
    {
        /// <summary>Наименование сущности</summary>
        public string Name { get; set; }
    }
}
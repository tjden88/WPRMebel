namespace WPRMebel.Domain.Base.Catalog.Abstract
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
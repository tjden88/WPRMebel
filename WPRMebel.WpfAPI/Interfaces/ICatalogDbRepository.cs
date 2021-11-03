using System.Linq;
using WPRMebel.Interfaces.Base.Entityes;
using WPRMebel.Interfaces.Base.Repositories;

namespace WPRMebel.WpfAPI.Interfaces
{
    /// <summary>
    /// Просмотр содержимого БД для оконного интерфейса
    /// </summary>
    public interface ICatalogDbRepository<T> : IRepository<T> where T : IEntity
    {
        IQueryable<T> Items { get; }

        void StartTransaction();
        void CommitTransaction();
    }
}

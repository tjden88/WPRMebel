using System.Threading;
using System.Threading.Tasks;
using WPRMebel.Interfaces.Base.Entityes;

namespace WPRMebel.Interfaces.Base.Repositories
{
    /// <summary>
    /// Интерфейс репозитория именованных сущностей
    /// </summary>
    /// <typeparam name="TNamedEntity">Именованная сущность</typeparam>
    public interface INamedRepository<TNamedEntity> : IRepository<TNamedEntity> where TNamedEntity : INamedEntity
    {
        /// <summary>
        /// Получить сущность по имени
        /// </summary>
        /// <param name="name">Имя искомой сущности</param>
        /// <param name="Cancel">Токен отмены</param>
        /// <returns>null, если сущность не найдена</returns>
        Task<TNamedEntity> GetByNameAsync(string name, CancellationToken Cancel = default);


        /// <summary>
        /// Существует ли сущность в репозитории
        /// </summary>
        /// <param name="name">Имя искомой сущность</param>
        /// <param name="Cancel">Токен отмены</param>
        /// <returns>Истина, если сущность есть в репозитории</returns>
        Task<bool> ExistAsync(string name, CancellationToken Cancel = default);


        /// <summary>
        /// Удалить сущность из репозитория
        /// </summary>
        /// <param name="name">имя удаляемой сущности</param>
        /// <param name="Cancel">Токен отмены</param>
        /// <returns>Истина, если удалось удалить</returns>
        Task<bool> DeleteAsync(string name, CancellationToken Cancel = default);
    }
}
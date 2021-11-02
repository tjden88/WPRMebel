using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WPRMebel.Interfaces.Base.Entityes;

namespace WPRMebel.Interfaces.Base.Repositories
{
    /// <summary>
    /// Интерфейс репозитория сущностей
    /// </summary>
    /// <typeparam name="TEntity">Сущность</typeparam>
    public interface IRepository<TEntity> where TEntity : IEntity
    {
        /// <summary> Коллекция сущностей </summary>
        IQueryable<TEntity> Items { get; }


        /// <summary> Получить количество сущностей </summary>
        Task<int> GetCountAsync(CancellationToken Cancel = default);


        /// <summary>
        /// Существует ли сущность в репозитории
        /// </summary>
        /// <param name="id">идентификатор сущности</param>
        /// <param name="Cancel">Токен отмены</param>
        /// <returns>Истина, если сущность есть в репозитории</returns>
        Task<bool> ExistAsync(int id, CancellationToken Cancel = default);


        /// <summary>
        /// Существует ли сущность в репозитории
        /// </summary>
        /// <param name="item">Искомая сущность</param>
        /// <param name="Cancel">Токен отмены</param>
        /// <returns>Истина, если сущность есть в репозитории</returns>
        Task<bool> ExistAsync(TEntity item, CancellationToken Cancel = default);


        /// <summary>
        /// Получить сущность по идентификатору
        /// </summary>
        /// <param name="id">Id сущности</param>
        /// <param name="Cancel">Токен отмены</param>
        /// <returns>null, если сущность не найдена</returns>
        Task<TEntity> GetByIdAsync(int id, CancellationToken Cancel = default);


        /// <summary>
        /// Добавить сущность в репозиторий
        /// </summary>
        /// <param name="item">Добавляемая сущность</param>
        /// <param name="Cancel">Токен отмены</param>
        /// <returns>Добавленная сущность, или null, если добавить не удалось</returns>
        Task<TEntity> AddAsync(TEntity item, CancellationToken Cancel = default);


        /// <summary>
        /// Обновить сущность в репозитории
        /// </summary>
        /// <param name="item">Изменяемая сущность</param>
        /// <param name="Cancel">Токен отмены</param>
        /// <returns>Истина, если удалось обновить</returns>
        Task<bool> UpdateAsync(TEntity item, CancellationToken Cancel = default);


        /// <summary>
        /// Удалить сущность из репозитория
        /// </summary>
        /// <param name="item">Удаляемая сущность</param>
        /// <param name="Cancel">Токен отмены</param>
        /// <returns>Истина, если удалось удалить</returns>
        Task<bool> DeleteAsync(TEntity item, CancellationToken Cancel = default);


        /// <summary>
        /// Удалить сущность из репозитория
        /// </summary>
        /// <param name="id">Id удаляемой сущности</param>
        /// <param name="Cancel">Токен отмены</param>
        /// <returns>Истина, если удалось удалить</returns>
        Task<bool> DeleteAsync(int id, CancellationToken Cancel = default);
    }
}

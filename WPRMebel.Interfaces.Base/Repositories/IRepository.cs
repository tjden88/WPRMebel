using System.Collections.Generic;
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
        /// <summary> Получить все сущности из репозитория </summary>
        Task<IEnumerable<TEntity>> GetAll(CancellationToken Cancel = default);


        /// <summary> Получить количество сущностей </summary>
        Task<int> GetCount(CancellationToken Cancel = default);


        /// <summary>
        /// Существует ли сущность в репозитории
        /// </summary>
        /// <param name="id">идентификатор сущности</param>
        /// <param name="Cancel">Токен отмены</param>
        /// <returns>Истина, если сущность есть в репозитории</returns>
        Task<bool> Exist(int id, CancellationToken Cancel = default);


        /// <summary>
        /// Существует ли сущность в репозитории
        /// </summary>
        /// <param name="item">Искомая сущность</param>
        /// <param name="Cancel">Токен отмены</param>
        /// <returns>Истина, если сущность есть в репозитории</returns>
        Task<bool> Exist(TEntity item, CancellationToken Cancel = default);


        /// <summary>
        /// Получить сущность по идентификатору
        /// </summary>
        /// <param name="id">Id сущности</param>
        /// <param name="Cancel">Токен отмены</param>
        /// <returns>null, если сущность не найдена</returns>
        Task<TEntity> GetById(int id, CancellationToken Cancel = default);


        /// <summary>
        /// Добавить сущность в репозиторий
        /// </summary>
        /// <param name="item">Добавляемая сущность</param>
        /// <param name="Cancel">Токен отмены</param>
        /// <returns>Добавленная сущность, или null, если добавить не удалось</returns>
        Task<TEntity> Add(TEntity item, CancellationToken Cancel = default);


        /// <summary>
        /// Обновить сущность в репозитории
        /// </summary>
        /// <param name="item">Изменяемая сущность</param>
        /// <param name="Cancel">Токен отмены</param>
        /// <returns>Истина, если удалось обновить</returns>
        Task<bool> Update(TEntity item, CancellationToken Cancel = default);


        /// <summary>
        /// Удалить сущность из репозитория
        /// </summary>
        /// <param name="item">Удаляемая сущность</param>
        /// <param name="Cancel">Токен отмены</param>
        /// <returns>Истина, если удалось удалить</returns>
        Task<bool> Delete(TEntity item, CancellationToken Cancel = default);


        /// <summary>
        /// Удалить сущность из репозитория
        /// </summary>
        /// <param name="id">Id удаляемой сущности</param>
        /// <param name="Cancel">Токен отмены</param>
        /// <returns>Истина, если удалось удалить</returns>
        Task<bool> Delete(int id, CancellationToken Cancel = default);
    }
}

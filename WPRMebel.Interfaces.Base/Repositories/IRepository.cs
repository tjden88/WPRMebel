using System.Collections.Generic;
using System.Threading.Tasks;
using WPRMebel.Interfaces.Base.Entities;

namespace WPRMebel.Interfaces.Base.Repositories
{
    /// <summary>
    /// Интерфейс репозитория сущностей
    /// </summary>
    /// <typeparam name="TEntity">Сущность</typeparam>
    public interface IRepository<TEntity> where TEntity : IEntity
    {
        /// <summary>
        /// Существует ли сущность в репозитории
        /// </summary>
        /// <param name="id">идентификатор сущности</param>
        /// <returns>Истина, если сущность есть в репозитории</returns>
        Task<bool> Exist(int id);

        /// <summary>
        /// Существует ли сущность в репозитории
        /// </summary>
        /// <param name="item">Искомая сущность</param>
        /// <returns>Истина, если сущность есть в репозитории</returns>
        virtual Task<bool> Exist(TEntity item) => Exist(item.Id);

        /// <summary> Получить все сущности из репозитория </summary>
        Task<IEnumerable<TEntity>> GetAll();

        /// <summary> Получить количество сущностей </summary>
        Task<int> GetCount();

        /// <summary>
        /// Получить сущность по идентификатору
        /// </summary>
        /// <param name="id">Id сущности</param>
        /// <returns>null, если сущность не найдена</returns>
        Task<IEntity> GetById(int id);

        /// <summary>
        /// Добавить сущность в репозиторий
        /// </summary>
        /// <param name="item">Добавляемая сущность</param>
        /// <returns>Добавленная сущность, или null, если добавить не удалось</returns>
        Task<TEntity> Add(TEntity item);

        /// <summary>
        /// Обновить сущность в репозитории
        /// </summary>
        /// <param name="item">Изменяемая сущность</param>
        /// <returns>Истина, если удалось обновить</returns>
        Task<bool> Update(TEntity item);

        /// <summary>
        /// Удалить сущность из репозитория
        /// </summary>
        /// <param name="item">Удаляемая сущность</param>
        /// <returns>Истина, если удалось удалить</returns>
        Task<bool> Delete(TEntity item);

        /// <summary>
        /// Удалить сущность из репозитория
        /// </summary>
        /// <param name="id">Id удаляемой сущности</param>
        /// <returns>Истина, если удалось удалить</returns>
        Task<bool> Delete(int id);
    }
}

using System.Threading.Tasks;
using WPRMebel.Interfaces.Base.Entities;

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
        /// <returns>null, если сущность не найдена</returns>
        Task<TNamedEntity> GetByName(string name);

        /// <summary>
        /// Существует ли сущность в репозитории
        /// </summary>
        /// <param name="name">Имя искомой сущность</param>
        /// <returns>Истина, если сущность есть в репозитории</returns>
        virtual Task<bool> Exist(string name) => Exist(GetByName(name).Id);

        /// <summary>
        /// Удалить сущность из репозитория
        /// </summary>
        /// <param name="name">имя удаляемой сущности</param>
        /// <returns>Истина, если удалось удалить</returns>
        Task<bool> Delete(string name);
    }
}
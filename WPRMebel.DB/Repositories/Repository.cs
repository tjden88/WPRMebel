using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WPRMebel.DB.BaseEntities;
using WPRMebel.DB.Context;
using WPRMebel.Interfaces.Base.Entities;
using WPRMebel.Interfaces.Base.Repositories;

namespace WPRMebel.DB.Repositories
{
    /// <summary>
    /// Репозиторий сущностей БД
    /// </summary>
    /// <typeparam name="T">Сущность БД</typeparam>
    public class Repository<T> : IRepository<T> where T : Entity, new()
    {
        private readonly CatalogContext _Context;

        /// <summary> Набор данных БД </summary>
        protected DbSet<T> Set { get; }

        /// <summary> Переопределяемое свойство, с которым работают методы репозитория </summary>
        protected virtual IQueryable<T> Items => Set;

        public Repository(CatalogContext Context)
        {
            _Context = Context;
            Set = _Context.Set<T>();
        }

        #region Transaction
        /// <summary> Начать транзакцию БД </summary>
        public async Task BeginTransaction(CancellationToken cancel = default) => await _Context.Database.BeginTransactionAsync(cancel).ConfigureAwait(false);

        /// <summary> Завершить транзакцию БД </summary>
        public async Task CommitTransaction(CancellationToken cancel = default) => await _Context.Database.CommitTransactionAsync(cancel).ConfigureAwait(false);
        #endregion

        #region IRepository
        public async Task<bool> Exist(int id, CancellationToken cancel = default) => await Items.AnyAsync(item => item.Id == id, cancel).ConfigureAwait(false);

        public async Task<IEnumerable<T>> GetAll(CancellationToken cancel = default) => await Items.ToArrayAsync(cancel).ConfigureAwait(false);

        public async Task<int> GetCount(CancellationToken cancel = default) => await Items.CountAsync(cancel).ConfigureAwait(false);

        public async Task<IEntity> GetById(int id, CancellationToken cancel = default) => await Items.SingleOrDefaultAsync(i => i.Id == id, cancel).ConfigureAwait(false);

        public async Task<T> Add(T item, CancellationToken cancel = default)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));

            await _Context.AddAsync(item, cancel).ConfigureAwait(false);
            await _Context.SaveChangesAsync(cancel).ConfigureAwait(false);
            return item;
        }

        public async Task<bool> Update(T item, CancellationToken cancel = default)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));

            _Context.Update(item);
            return await _Context.SaveChangesAsync(cancel).ConfigureAwait(false) > 0;
        }

        public async Task<bool> Delete(T item, CancellationToken cancel = default)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));

            if (!await Exist(item.Id, cancel).ConfigureAwait(false)) return false;

            _Context.Remove(item);
            return await _Context.SaveChangesAsync(cancel).ConfigureAwait(false) > 0;
        }

        public async Task<bool> Delete(int id, CancellationToken cancel = default)
        {
            var item = Set.Local
                           .FirstOrDefault(i => i.Id == id) ?? await Set
                           .Select(i => new T { Id = i.Id })
                           .FirstOrDefaultAsync(i => i.Id == id, cancel)
                           .ConfigureAwait(false);

            return item != null && await Delete(item, cancel).ConfigureAwait(false);
        } 
        #endregion
    }
}

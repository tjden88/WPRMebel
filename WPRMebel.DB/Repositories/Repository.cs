using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using WPRMebel.DB.BaseEntities;
using WPRMebel.DB.Context;
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

        private bool _TransactionMode;

        /// <summary> Начать транзакцию БД </summary>
        public void BeginTransaction()
        {
            _TransactionMode = true;
        }

        /// <summary> Завершить транзакцию БД </summary>
        public async Task CommitTransaction(CancellationToken cancel = default)
        {
            if (!_TransactionMode) return;
            await _Context.SaveChangesAsync(cancel).ConfigureAwait(false);
            _TransactionMode = false;
        }

        #endregion

        #region IRepository
        public async Task<bool> Exist(int id, CancellationToken cancel = default) => await Items.AnyAsync(item => item.Id == id, cancel).ConfigureAwait(false);

        public async Task<IEnumerable<T>> GetAll(CancellationToken cancel = default) => await Items.ToArrayAsync(cancel).ConfigureAwait(false);

        public async Task<int> GetCount(CancellationToken cancel = default) => await Items.CountAsync(cancel).ConfigureAwait(false);

        public async Task<T> GetById(int id, CancellationToken cancel = default) => await Items.SingleOrDefaultAsync(i => i.Id == id, cancel).ConfigureAwait(false);

        public async Task<T> Add(T item, CancellationToken cancel = default)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));

            _Context.Entry(item).State = EntityState.Added;
            //await _Context.AddAsync(item, cancel).ConfigureAwait(false);
            if (!_TransactionMode) await _Context.SaveChangesAsync(cancel).ConfigureAwait(false);
            return item;
        }

        public async Task<bool> Update(T item, CancellationToken cancel = default)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));

            _Context.Entry(item).State = EntityState.Modified;
            //_Context.Update(item);
            if (!_TransactionMode) return await _Context.SaveChangesAsync(cancel).ConfigureAwait(false) > 0;
            return true;
        }

        public async Task<bool> Delete(T item, CancellationToken cancel = default)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));

            if (!await Exist(item.Id, cancel).ConfigureAwait(false)) return false;

            //_Context.Remove(item);
            _Context.Entry(item).State = EntityState.Deleted;
            if (!_TransactionMode) return await _Context.SaveChangesAsync(cancel).ConfigureAwait(false) > 0;
            return true;
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

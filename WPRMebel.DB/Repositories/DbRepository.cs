using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WPRMebel.Domain.Base.Catalog.Abstract;
using WPRMebel.Interfaces.Base.Repositories;

namespace WPRMebel.DB.Repositories
{
    /// <summary>
    /// Репозиторий сущностей БД
    /// </summary>
    /// <typeparam name="T">Сущность БД</typeparam>
    public class DbRepository<T> : IRepository<T> where T : Entity, new()
    {
        private readonly DbContext _ContextBase; // Контекст БД

        /// <summary> Набор данных БД </summary>
        protected DbSet<T> Set { get; }

        public DbRepository(DbContext ContextBase)
        {
            _ContextBase = ContextBase;
            Set = _ContextBase.Set<T>();
        }

         /// <summary> Переопределяемое свойство, с которым работают методы репозитория </summary>
       protected virtual IQueryable<T> Items => Set;

        #region Transaction

        /// <summary> Режим транзакуии </summary>
        public bool TransactionMode { get; set; }

        /// <summary> Начать транзакцию БД </summary>
        public void BeginTransaction() => TransactionMode = true;

        /// <summary> Завершить транзакцию БД </summary>
        public async Task CommitTransaction(CancellationToken Cancel = default)
        {
            if (!TransactionMode) return;
            await _ContextBase.SaveChangesAsync(Cancel).ConfigureAwait(false);
            TransactionMode = false;
        }

        #endregion

        #region IRepository
        public virtual async  Task<IEnumerable<T>> GetAllAsync(CancellationToken Cancel = default) => await Items.ToArrayAsync(Cancel).ConfigureAwait(false);

        public async Task<int> GetCountAsync(CancellationToken Cancel = default) => await Items.CountAsync(Cancel).ConfigureAwait(false);


        public async Task<bool> ExistAsync(int id, CancellationToken Cancel = default) => await Items.AnyAsync(item => item.Id == id, Cancel).ConfigureAwait(false);


        public async Task<bool> ExistAsync(T item, CancellationToken Cancel = default) => await ExistAsync(item.Id, Cancel);


        public async Task<T> GetByIdAsync(int id, CancellationToken Cancel = default) => await Items.SingleOrDefaultAsync(i => i.Id == id, Cancel).ConfigureAwait(false);


        public async Task<T> AddAsync(T item, CancellationToken Cancel = default)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));

            _ContextBase.Entry(item).State = EntityState.Added;
            if (!TransactionMode) await _ContextBase.SaveChangesAsync(Cancel).ConfigureAwait(false);
            return item;
        }


        public async Task<bool> UpdateAsync(T item, CancellationToken Cancel = default)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));

            _ContextBase.Entry(item).State = EntityState.Modified;
            if (!TransactionMode) return await _ContextBase.SaveChangesAsync(Cancel).ConfigureAwait(false) > 0;
            return true;
        }


        public async Task<bool> DeleteAsync(T item, CancellationToken Cancel = default)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));

            if (!await ExistAsync(item.Id, Cancel).ConfigureAwait(false)) return false;

            _ContextBase.Entry(item).State = EntityState.Deleted;
            if (!TransactionMode) return await _ContextBase.SaveChangesAsync(Cancel).ConfigureAwait(false) > 0;
            return true;
        }


        public async Task<bool> DeleteAsync(int id, CancellationToken Cancel = default)
        {
            var item = Set.Local
                           .FirstOrDefault(i => i.Id == id) ?? await Set
                           .Select(i => new T { Id = i.Id })
                           .FirstOrDefaultAsync(i => i.Id == id, Cancel)
                           .ConfigureAwait(false);

            return item != null && await DeleteAsync(item, Cancel).ConfigureAwait(false);
        }

        #endregion
    }
}

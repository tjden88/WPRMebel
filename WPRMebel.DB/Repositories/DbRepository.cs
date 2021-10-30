using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WPRMebel.DB.Context;
using WPRMebel.Entityes.Base;
using WPRMebel.Interfaces.Base.Repositories;

namespace WPRMebel.DB.Repositories
{
    /// <summary>
    /// Репозиторий сущностей БД
    /// </summary>
    /// <typeparam name="T">Сущность БД</typeparam>
    public class DbRepository<T> : IRepository<T> where T : Entity, new()
    {
        private readonly CatalogContext _Context; // Контекст БД

        /// <summary> Набор данных БД </summary>
        protected DbSet<T> Set { get; }

        /// <summary> Переопределяемое свойство, с которым работают методы репозитория </summary>
        protected virtual IQueryable<T> Items => Set;

        public DbRepository(CatalogContext Context)
        {
            _Context = Context;
            Set = _Context.Set<T>();
        }


        #region Transaction

        /// <summary> Режим транзакуии </summary>
        public bool TransactionMode { get; set; }


        /// <summary> Начать транзакцию БД </summary>
        public void BeginTransaction() => TransactionMode = true;


        /// <summary> Завершить транзакцию БД </summary>
        public async Task CommitTransaction(CancellationToken Cancel = default)
        {
            if (!TransactionMode) return;
            await _Context.SaveChangesAsync(Cancel).ConfigureAwait(false);
            TransactionMode = false;
        }

        #endregion

        #region IRepository
        public async Task<IEnumerable<T>> GetAll(CancellationToken Cancel = default) => await Items.ToArrayAsync(Cancel).ConfigureAwait(false);


        public async Task<int> GetCount(CancellationToken Cancel = default) => await Items.CountAsync(Cancel).ConfigureAwait(false);


        public async Task<bool> Exist(int id, CancellationToken Cancel = default) => await Items.AnyAsync(item => item.Id == id, Cancel).ConfigureAwait(false);


        public async Task<bool> Exist(T item, CancellationToken Cancel = default) => await Exist(item.Id, Cancel);


        public async Task<T> GetById(int id, CancellationToken Cancel = default) => await Items.SingleOrDefaultAsync(i => i.Id == id, Cancel).ConfigureAwait(false);


        public async Task<T> Add(T item, CancellationToken Cancel = default)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));

            _Context.Entry(item).State = EntityState.Added;
            if (!TransactionMode) await _Context.SaveChangesAsync(Cancel).ConfigureAwait(false);
            return item;
        }


        public async Task<bool> Update(T item, CancellationToken Cancel = default)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));

            _Context.Entry(item).State = EntityState.Modified;
            if (!TransactionMode) return await _Context.SaveChangesAsync(Cancel).ConfigureAwait(false) > 0;
            return true;
        }


        public async Task<bool> Delete(T item, CancellationToken Cancel = default)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));

            if (!await Exist(item.Id, Cancel).ConfigureAwait(false)) return false;

            _Context.Entry(item).State = EntityState.Deleted;
            if (!TransactionMode) return await _Context.SaveChangesAsync(Cancel).ConfigureAwait(false) > 0;
            return true;
        }


        public async Task<bool> Delete(int id, CancellationToken Cancel = default)
        {
            //var item = Set.Local
            //               .FirstOrDefault(i => i.Id == id) ?? await Set
            //               .Select(i => new T { Id = i.Id })
            //               .FirstOrDefaultAsync(i => i.Id == id, Cancel)
            //               .ConfigureAwait(false);

            var item = new T {Id = id};

            //return item != null && await Delete(item, Cancel).ConfigureAwait(false);
            return await Delete(item, Cancel).ConfigureAwait(false);
        }

        #endregion
    }
}

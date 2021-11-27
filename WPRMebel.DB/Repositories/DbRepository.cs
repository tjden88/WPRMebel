using System;
using System.Collections.Generic;
using System.Linq;
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

        public DbRepository(DbContext ContextBase)
        {
            _ContextBase = ContextBase;
        }

         /// <summary> Переопределяемое свойство, с которым работают методы репозитория </summary>
       protected virtual IQueryable<T> Items => _ContextBase.Set<T>();

        #region Transaction

        /// <summary> Сохранять изменения в БД при каждом обращении </summary>
        public bool AutoSaveChanges { get; set; } = true;

        #endregion

        #region IRepository
        public IEnumerable<T> GetAll() => Items;
        public int GetCount() => Items.Count();

        public bool Exist(int id) => Items.Any(item => item.Id == id);

        public bool Exist(T item) => Exist(item.Id);

        public T GetById(int id) => Items.SingleOrDefault(item => item.Id == id);

        public T Add(T item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));

            _ContextBase.Entry(item).State = EntityState.Added;
            if (!AutoSaveChanges) _ContextBase.SaveChanges();
            return item;
        }

        public bool Update(T item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));

            _ContextBase.Entry(item).State = EntityState.Modified;
            if (!AutoSaveChanges) return  _ContextBase.SaveChanges() > 0;
            return true;
        }

        public bool Delete(T item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));

            if (!Exist(item.Id)) return false;

            _ContextBase.Entry(item).State = EntityState.Deleted;
            if (!AutoSaveChanges) return _ContextBase.SaveChanges() > 0;
            return true;
        }

        public bool Delete(int id)
        {
            var item = Items
                .Select(i => new T { Id = i.Id })
                .FirstOrDefault(i => i.Id == id);

            return item != null && Delete(item);
        }

        #endregion
    }
}

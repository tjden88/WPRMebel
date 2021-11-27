using System;
using System.Linq;
using WPRMebel.DB.Context;
using WPRMebel.Domain.Base.Catalog.Abstract;
using WPRMebel.Interfaces.Base.Repositories;

namespace WPRMebel.DB.Repositories
{
    /// <summary>
    /// Репозиторий именованных сущностей БД
    /// </summary>
    /// <typeparam name="T">Именованная сущность БД</typeparam>

    public class DbNamedRepository<T> : DbRepository<T>, INamedRepository<T> where T : NamedEntity, new()
    {
        public DbNamedRepository(CatalogContextBase ContextBase) : base(ContextBase)
        {
        }

        public T GetByName(string name)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));
            return Items.FirstOrDefault(i => i.Name == name);
        }

        public bool Exist(string name) => Items.Any(i => i.Name == name);

        public bool Delete(string name)
        {
            var item = Items
                .Select(i => new T { Id = i.Id, Name = i.Name })
                .FirstOrDefault(i => i.Name == name);

            return item != null && Delete(item);
        }
    }
}
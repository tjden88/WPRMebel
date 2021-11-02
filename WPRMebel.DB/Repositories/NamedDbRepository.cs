using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WPRMebel.DB.Context;
using WPRMebel.Domain.Base.Catalog.Abstract;
using WPRMebel.Interfaces.Base.Repositories;

namespace WPRMebel.DB.Repositories
{
    /// <summary>
    /// Репозиторий именованных сущностей БД
    /// </summary>
    /// <typeparam name="T">Именованная сущность БД</typeparam>

    public class NamedDbRepository<T> : DbRepository<T>, INamedRepository<T> where T : NamedEntity, new()
    {
        public NamedDbRepository(CatalogContext Context) : base(Context)
        {
        }


        public async Task<T> GetByName(string name, CancellationToken Cancel = default)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));
            return await Items.FirstOrDefaultAsync(i => i.Name == name, Cancel).ConfigureAwait(false);
        }


        public async Task<bool> Exist(string name, CancellationToken Cancel = default) => 
            await Items.AnyAsync(i => i.Name == name, Cancel).ConfigureAwait(false);


        public async Task<bool> Delete(string name, CancellationToken Cancel = default)
        {
            var item = Set.Local
                .FirstOrDefault(i => i.Name == name) ?? await Set
                .Select(i=> new T {Id = i.Id, Name = i.Name})
                .FirstOrDefaultAsync(i => i.Name == name, Cancel)
                .ConfigureAwait(false);

            return item != null && await Delete(item, Cancel).ConfigureAwait(false);
        }
    }
}
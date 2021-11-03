using System.Linq;
using WPRMebel.DB.Repositories;
using WPRMebel.DB.TestSqlServer.Context;
using WPRMebel.Domain.Base.Catalog.Abstract;
using WPRMebel.WpfAPI.Interfaces;

namespace WPRMebel.WpfAPI.Services
{
    public class CatalogCatalogDbRepository<T> : DbRepository<T>, ICatalogDbRepository<T> where T : Entity, new()
    {
        public CatalogCatalogDbRepository(CatalogDbContext ContextBase) : base(ContextBase)
        {
        }

        IQueryable<T> ICatalogDbRepository<T>.Items => Items;

        public void StartTransaction()
        {
            BeginTransaction();
        }

        public async void CommitTransaction() => await base.CommitTransaction().ConfigureAwait(false);
    }
}

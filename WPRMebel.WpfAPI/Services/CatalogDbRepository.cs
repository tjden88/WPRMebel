using System.Linq;
using Microsoft.EntityFrameworkCore;
using WPRMebel.DB.Repositories;
using WPRMebel.DB.TestSqlServer.Context;
using WPRMebel.Domain.Base.Catalog.Abstract;
using WPRMebel.WpfAPI.Interfaces;

namespace WPRMebel.WpfAPI.Services
{
    public class CatalogDbRepository<T> : DbRepository<T>, ICatalogDbRepository<T> where T : Entity, new()
    {
        public CatalogDbRepository(CatalogDbContext ContextBase) : base(ContextBase)
        {
        }

        IQueryable<T> ICatalogDbRepository<T>.Items => Items.AsNoTracking();

        public void StartTransaction() => BeginTransaction();

        public async void CommitTransaction() => await base.CommitTransaction().ConfigureAwait(false);
    }
}

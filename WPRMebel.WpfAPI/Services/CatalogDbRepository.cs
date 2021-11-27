using System.Linq;
using WPRMebel.DB.Repositories;
using WPRMebel.DB.SqLite.Context;
using WPRMebel.Domain.Base.Catalog.Abstract;
using WPRMebel.WpfAPI.Interfaces;

namespace WPRMebel.WpfAPI.Services
{
    public class CatalogDbRepository<T> : DbRepository<T>, ICatalogDbRepository<T> where T : Entity, new()
    {
        public CatalogDbRepository(CatalogDbContext ContextBase) : base(ContextBase)
        {
        }

        IQueryable<T> ICatalogDbRepository<T>.Items => Items;
    }
}

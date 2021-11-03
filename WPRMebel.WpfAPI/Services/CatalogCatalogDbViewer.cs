using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WPRMebel.DB.Repositories;
using WPRMebel.DB.TestSqlServer.Context;
using WPRMebel.Domain.Base.Catalog.Abstract;
using WPRMebel.WpfAPI.Interfaces;

namespace WPRMebel.WpfAPI.Services
{
    public class CatalogCatalogDbViewer<T> : DbRepository<T>, ICatalogDbViewer<T> where T: Entity, new()
    {
        public CatalogCatalogDbViewer(CatalogDbContext ContextBase) : base(ContextBase)
        {
        }

        public async Task<ObservableCollection<T>> LoadItemsAsync()
        {
            await Set.LoadAsync().ConfigureAwait(false);
            return Set.Local.ToObservableCollection();
        }

    }
}

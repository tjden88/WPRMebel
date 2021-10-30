using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WPRMebel.DB.Context;
using WPRMebel.Entityes.Catalog;

namespace WPRMebel.DB.TestSqlServer.Context
{
    public class CatalogDbContext : CatalogContext
    {
        protected override void Configure(DbContextOptionsBuilder optionsBuilder) =>
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=WPRMebel");

        public override async Task InitializeStartData(CancellationToken cancel = default)
        {
            await base.InitializeStartData(cancel).ConfigureAwait(false);
#if DEBUG
            // ---------------- Test Data -----------------
            var vendors = new Collection<Vendor>();
            for (var i = 0; i < 10; i++)
            {
                vendors.Add(new Vendor() { Name = $"Поставщик {i}" });
            }

            var rnd = new Random();
            var categories = new Collection<Category>();
            for (var i = 0; i < 100; i++)
            {
                categories.Add(new Category() { Name = $"Категория {i}", Vendor = vendors[rnd.Next(vendors.Count)] });
            }

            await Vendors.AddRangeAsync(vendors, cancel).ConfigureAwait(false);
            await Categories.AddRangeAsync(categories, cancel).ConfigureAwait(false);

            await SaveChangesAsync(cancel).ConfigureAwait(false);
            // ---------------- Test Data -----------------
#endif
        }
    }
}

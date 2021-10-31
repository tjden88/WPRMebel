using System;
using System.Collections.ObjectModel;
using System.Linq;
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

        public override async Task InitializeStartData(CancellationToken Cancel = default)
        {
            await base.InitializeStartData(Cancel).ConfigureAwait(false);

#if DEBUG
            if(Vendors.Any()) return;
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

            await Vendors.AddRangeAsync(vendors, Cancel).ConfigureAwait(false);
            await Categories.AddRangeAsync(categories, Cancel).ConfigureAwait(false);

            await SaveChangesAsync(Cancel).ConfigureAwait(false);
            // ---------------- Test Data -----------------
#endif
        }
    }
}

using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WPRMebel.DB.Context;
using WPRMebel.Domain.Base.Catalog;

namespace WPRMebel.DB.SqLite.Context
{
    /// <summary>
    /// Контекст БД каталога SQLite
    /// </summary>
    public class CatalogDbContext : CatalogContextBase
    {

        protected override void Configure(DbContextOptionsBuilder optionsBuilder)
        {
            if (Connection.CatalogDbPath is null)
                throw new ArgumentNullException(nameof(Connection.CatalogDbPath), "Имя БД каталога не указано");
            optionsBuilder.UseSqlite($"Data Source={Connection.CatalogDbPath};");
        }

        public override async Task InitializeStartData(CancellationToken Cancel = default)
        {
            await Database.MigrateAsync(Cancel).ConfigureAwait(false);

            await base.InitializeStartData(Cancel).ConfigureAwait(false);

#if DEBUG
            // ---------------- Test Data -----------------
            if (Vendors.Any()) return;

            var sectionCount = Sections.Count();
            var rnd = new Random();

            var vendors = new Collection<Vendor>(); // Поставщики
            for (var i = 0; i < 10; i++)
            {
                vendors.Add(new Vendor() { Name = $"Поставщик {i}" });
            }


            var categories = new Collection<Category>(); // Категории
            for (var i = 0; i < 100; i++)
            {
                categories.Add(new Category()
                {
                    Name = $"Категория {i}",
                    Vendor = vendors[rnd.Next(vendors.Count)],
                    Section = await Sections.FindAsync(rnd.Next(1, sectionCount + 1))
                });
            }

            var elements = new Collection<Fitting>(); // Элементы
            for (var i = 0; i < 1000; i++)
            {
                elements.Add(new Fitting()
                {
                    Name = $"Элемент {i}",
                    Category = categories[rnd.Next(categories.Count)],
                    Price = (decimal)(200 + rnd.NextDouble() * 5000),
                    Extra = (1 + rnd.NextDouble() * 3)
                });
            }

            var elementsprop = new Collection<ElementProperty>(); // Свойства элементов
            for (var i = 0; i < 100; i++)
            {
                elementsprop.Add(new ElementProperty()
                {
                    Name = $"Свойство {i}",
                    CatalogElement = elements[rnd.Next(elements.Count)]
                });
            }

            var elementspropvalues = new Collection<ElementPropertyValue>(); //Значение Свойства элементов

            foreach (var property in elementsprop)
            {
                for (var i = 0; i < 20; i++)
                {
                    elementspropvalues.Add(new ElementPropertyValue()
                    {
                        Name = $"Значение Свойства {property.Name} #{i}",
                        ElementProperty = property
                    });
                }
            }

            var childelements = new Collection<ChildCatalogElement>(); //Дочерние элементы

            foreach (var fitting in elements)
            {
                for (var i = 0; i < 10; i++)
                {
                    childelements.Add(new ChildCatalogElement()
                    {
                        CatalogElement = fitting,
                        Quantity = rnd.Next(20),
                        OwnerCatalogElement = fitting

                    });
                }
            }


            await Vendors.AddRangeAsync(vendors, Cancel).ConfigureAwait(false);
            await Categories.AddRangeAsync(categories, Cancel).ConfigureAwait(false);
            await Fittings.AddRangeAsync(elements, Cancel).ConfigureAwait(false);
            await ElementProperties.AddRangeAsync(elementsprop, Cancel).ConfigureAwait(false);
            await ElementPropertyValues.AddRangeAsync(elementspropvalues, Cancel).ConfigureAwait(false);
            await ChildCatalogElements.AddRangeAsync(childelements, Cancel).ConfigureAwait(false);

            await SaveChangesAsync(Cancel).ConfigureAwait(false);
            // ---------------- Test Data -----------------
#endif
        }

    }
}

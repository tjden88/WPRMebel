﻿using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WPRMebel.DB.Context;
using WPRMebel.Domain.Base.Catalog;

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
            // ---------------- Test Data -----------------
            if(Vendors.Any()) return;

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
                    Section = await Sections.FindAsync(rnd.Next(1, sectionCount+1))
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


            await Vendors.AddRangeAsync(vendors, Cancel).ConfigureAwait(false);
            await Categories.AddRangeAsync(categories, Cancel).ConfigureAwait(false);
            await Fittings.AddRangeAsync(elements, Cancel).ConfigureAwait(false);
            await ElementProperties.AddRangeAsync(elementsprop, Cancel).ConfigureAwait(false);
            await ElementPropertyValues.AddRangeAsync(elementspropvalues, Cancel).ConfigureAwait(false);

            await SaveChangesAsync(Cancel).ConfigureAwait(false);
            // ---------------- Test Data -----------------
#endif
        }
    }
}

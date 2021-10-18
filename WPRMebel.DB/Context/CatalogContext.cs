using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WPRMebel.DB.Context.Base;
using WPRMebel.DB.Entities.Catalog;

namespace WPRMebel.DB.Context
{
    public abstract class CatalogContext : BaseContext
    {
        /// <summary> Секции каталога </summary>
        public DbSet<Section> Sections { get; set; }

        /// <summary>Поставщики</summary>
        public DbSet<Vendor> Vendors { get; set; }

        /// <summary>Категории элементов в каталоге</summary>
        public DbSet<Category> Categories { get; set; }

        public override async Task InitializeStartData(CancellationToken cancel = default)
        {
            if(Sections.Any())return;

            var sections = new Collection<Section>
            {
                new()
                {
                    Name="Листовой материал",
                    Description = "Основные листовые материалы для производства каркасов, фасадов и т.д.",
                    SectionType = CatalogSectionTypes.SheetMaterial
                },
                new()
                {
                    Name = "Погонный материал",
                    Description = "Погонные материалы и фурнитура (профили, трубы, столешницы и т.д.)",
                    SectionType = CatalogSectionTypes.RunningMaterial
                },
                new()
                {
                    Name = "Кромкооблицовочный материал",
                    Description = "Кромка и торцевые облицовочные материалы",
                    SectionType = CatalogSectionTypes.RunningMaterial
                },
                new()
                {
                    Name = "Фурнитура",
                    Description = "Различные элементы фурнитуры для производства мебели",
                    SectionType = CatalogSectionTypes.General
                },
                new()
                {
                    Name = "Стяжки",
                    Description = "Стяжная фурнитура",
                    SectionType = CatalogSectionTypes.Consumable
                },
                new()
                {
                    Name = "Услуги, операции",
                    Description = "Работы мебельного цеха, услуги по доставке, сборке",
                    SectionType = CatalogSectionTypes.Service
                },
                new()
                {
                    Name = "Расходные материалы",
                    Description = "Картон, стрейч-плёнка и т.д.",
                    SectionType = CatalogSectionTypes.Consumable
                }
            };
            await Sections.AddRangeAsync(sections, cancel).ConfigureAwait(false);

            // ---------------- Test Data -----------------
#if DEBUG
            var vendors = new Collection<Vendor>();
            for (var i = 0; i < 10; i++)
            {
                vendors.Add(new Vendor(){ Name = $"Поставщик {i}"});
            }

            var rnd = new Random();
            var categories = new Collection<Category>();
            for (var i = 0; i < 100; i++)
            {
                categories.Add(new Category() { Name = $"Категория {i}", Vendor = vendors[rnd.Next(vendors.Count)]});
            }

            await Vendors.AddRangeAsync(vendors, cancel).ConfigureAwait(false);
            await Categories.AddRangeAsync(categories, cancel).ConfigureAwait(false);

#endif
            // ---------------- Test Data -----------------

            await SaveChangesAsync(cancel).ConfigureAwait(false);
        }
    }
}

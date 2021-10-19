using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WPRMebel.DB.Catalog;
using WPRMebel.DB.Catalog.Entities;
using WPRMebel.DB.Context.Base;

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

        protected override void OnModelCreating(ModelBuilder model)
        {
            //model.Entity<Vendor>()
            //    .HasMany<Category>()
            //    .WithOne(v => v.Vendor)
            //    .OnDelete(DeleteBehavior.Cascade);
        }

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
            
            await SaveChangesAsync(cancel).ConfigureAwait(false);
        }
    }
}

﻿using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WPRMebel.DB.Context.Base;
using WPRMebel.DB.Initialization;
using WPRMebel.Domain.Base.Catalog;
using WPRMebel.Domain.Base.Catalog.Abstract;

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

        /// <summary>Листовые материалы каталога</summary>
        public DbSet<SheetMaterial> SheetMaterials { get; set; }

        /// <summary>Погонные материалы каталога</summary>
        public DbSet<RunningMaterial> RunningMaterials { get; set; }

        /// <summary>Элементы фурнитуры каталога</summary>
        public DbSet<Fitting> Fittings { get; set; }

        /// <summary>Элементы услуг каталога</summary>
        public DbSet<Service> Services { get; set; }

        /// <summary>Свойства элементов</summary>
        public DbSet<ElementProperty> ElementProperties { get; set; }

        /// <summary>Значения свойств элементов</summary>
        public DbSet<ElementPropertyValue> ElementPropertyValues { get; set; }

        /// <summary>Дочерние элементы комплекта</summary>
        public DbSet<ChildCatalogElement> ChildCatalogElements { get; set; }


        protected override void OnModelCreating(ModelBuilder model)
        {
            //model.Entity<Vendor>()
            //    .HasMany<Category>()
            //    .WithOne(v => v.Vendor)
            //    .OnDelete(DeleteBehavior.Cascade);

            model.Entity<Category>()
                .HasOne( c => c.Vendor)
                .WithMany(v => v.Categories)
                .OnDelete(DeleteBehavior.SetNull);

            //model.Entity<ChildCatalogElement>()
            //    .HasOne(child => child.CatalogElement).WithMany()
            //    .OnDelete(DeleteBehavior.SetNull);

            //model.Entity<ChildCatalogElement>()
            //    .HasOne(child => child.OwnerCatalogElement)
            //    .WithMany(e => e.ChildCatalogElements)
            //    .OnDelete(DeleteBehavior.Cascade);

            //model.Entity<CatalogElement>()
            //    .HasMany(element => element.ChildCatalogElements)
            //    .WithOne(child => child.OwnerCatalogElement)
            //    .OnDelete(DeleteBehavior.Cascade);
        }

        public override async Task InitializeStartData(CancellationToken Cancel = default)
        {
            if(Sections.Any())return;

            await Sections.AddRangeAsync(CatalogDbInitializer.InitSections, Cancel).ConfigureAwait(false);
            
            await SaveChangesAsync(Cancel).ConfigureAwait(false);
        }
    }
}

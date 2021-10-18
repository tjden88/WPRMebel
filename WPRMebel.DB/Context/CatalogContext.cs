using Microsoft.EntityFrameworkCore;
using WPRMebel.DB.Entities.Catalog;

namespace WPRMebel.DB.Context
{
    public abstract class CatalogContext : DbContext
    {
        /// <summary> Секции каталога </summary>
        public DbSet<Section> Sections { get; set; }

        /// <summary>Поставщики</summary>
        public DbSet<Vendor> Vendors { get; set; }

        /// <summary>Категории элементов в каталоге</summary>
        public DbSet<Category> Categories { get; set; }
    }
}

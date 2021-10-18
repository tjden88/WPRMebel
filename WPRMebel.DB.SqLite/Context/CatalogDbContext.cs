using Microsoft.EntityFrameworkCore;
using WPRMebel.DB.Context;

namespace WPRMebel.DB.SqLite.Context
{
    /// <summary>
    /// Контекст БД каталога SQLite
    /// </summary>
    public class CatalogDbContext : CatalogContext
    {
        protected override void Configure(DbContextOptionsBuilder optionsBuilder) => 
            optionsBuilder.UseSqlite($"Data Source={Connection.CatalogDbPath};");


        protected override void OnModelCreating(ModelBuilder modelBuilder) // FlueNet API there
        {
            //modelBuilder.Entity<Category>().HasData(new Category { Id = 1, Name = "StartData", Description = "StartDescript" }); // Начальные данные


            //modelBuilder.Entity<CatalogTrueView>((tw =>
            //{
            //    tw.HasNoKey();
            //    tw.ToView("CatalogView");
            //}));
        }
    }
}

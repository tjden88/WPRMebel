using System.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace WPRMebel.DB.Context
{
    public class DataDB : DbContext
    {
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=wpfENtityTestDB;Trusted_Connection=True;");
        //    //optionsBuilder.UseLazyLoadingProxies();
        //    optionsBuilder.LogTo(message => Debug.WriteLine(message), Microsoft.Extensions.Logging.LogLevel.Information);
        //}

        //protected override void OnModelCreating(ModelBuilder modelBuilder) // FlueNet API there
        //{
        //    modelBuilder.Entity<Category>().HasData(new Category { Id = 1, Name = "StartData", Description = "StartDescript" }); // Начальные данные


        //    modelBuilder.Entity<CatalogTrueView>((tw =>
        //    {
        //        tw.HasNoKey();
        //        tw.ToView("CatalogView");
        //    }));
        //}
    }
}

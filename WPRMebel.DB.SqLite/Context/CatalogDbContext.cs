﻿using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using WPRMebel.DB.Context;

namespace WPRMebel.DB.SqLite.Context
{
    public class CatalogDbContext : CatalogContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={Connection.CatalogDbPath};Version=3;");
            //optionsBuilder.UseLazyLoadingProxies();
            optionsBuilder.LogTo(message => Debug.WriteLine(message), Microsoft.Extensions.Logging.LogLevel.Information);
        }

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

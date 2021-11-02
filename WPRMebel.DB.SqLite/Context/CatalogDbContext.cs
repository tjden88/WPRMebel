using Microsoft.EntityFrameworkCore;
using WPRMebel.DB.Context;

namespace WPRMebel.DB.SqLite.Context
{
    /// <summary>
    /// Контекст БД каталога SQLite
    /// </summary>
    public class CatalogDbContext : CatalogContextBase
    {
        protected override void Configure(DbContextOptionsBuilder optionsBuilder) => 
            optionsBuilder.UseSqlite($"Data Source={Connection.CatalogDbPath};");

    }
}

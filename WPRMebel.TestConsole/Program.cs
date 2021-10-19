using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WPRMebel.DB.TestSqlServer.Context;

namespace WPRMebel.TestConsole
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            // Проверка создания БД

            var db = new CatalogDbContext();
            await db.Database.MigrateAsync();

            await db.InitializeStartData();

            Console.ReadLine();
        }
    }
}

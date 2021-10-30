using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WPRMebel.DB.Repositories;
using WPRMebel.DB.TestSqlServer.Context;
using WPRMebel.Entityes.Catalog;

namespace WPRMebel.TestConsole
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var cdb = new CatalogDbContext();
            await cdb.Database.MigrateAsync();
            await cdb.InitializeStartData();

            var repo = new NamedDbRepository<Category>(new CatalogDbContext());

            var vendors = new NamedDbRepository<Vendor>(new CatalogDbContext());

            var s = Stopwatch.StartNew();
            repo.BeginTransaction();
                var v = new Vendor() {Id = 1};
            for (var i = 0; i < 500; i++)
            {
                var e= await repo.Add(new Category() { Name = "Test " + i, Vendor = v }).ConfigureAwait(false);
                await repo.Delete(e).ConfigureAwait(false);
            }
            await repo.CommitTransaction().ConfigureAwait(false);

            Console.WriteLine(s.ElapsedMilliseconds);


            Console.ReadLine();
        }
    }
}

using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WPRMebel.DB.Repositories;
using WPRMebel.DB.TestSqlServer.Context;
using WPRMebel.Domain.Base.Catalog;

namespace WPRMebel.TestConsole
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var cdb = new CatalogDbContext();
            await cdb.Database.EnsureDeletedAsync();
            await cdb.Database.MigrateAsync();
            await cdb.InitializeStartData();

            var repo = new NamedDbRepository<Category>(cdb);

            var vendors = new NamedDbRepository<Vendor>(cdb);
            var sections = new NamedDbRepository<Section>(cdb);

            //var s = Stopwatch.StartNew();
            //repo.BeginTransaction();
            //    var v = new Vendor() {Id = 1};
            //for (var i = 0; i < 1000; i++)
            //{
            //    var e= await repo.Add(new Category() { Name = "Test " + i, Vendor = v }).ConfigureAwait(false);
            //}
            //await repo.CommitTransaction().ConfigureAwait(false);

            //Console.WriteLine(s.ElapsedMilliseconds);


            var r = await repo.Delete("Категория 51");
            var r2 = await repo.Delete("Категория 52");

            var element = cdb.Fittings.First();

             var r3 = await sections.Delete(1);

            //await vendors.Delete(5);

            Console.WriteLine(r);
            Console.WriteLine(r2);
            //Console.ReadLine();
        }
    }
}

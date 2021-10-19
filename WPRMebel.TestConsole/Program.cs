using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WPRMebel.DB.Catalog.Entities;
using WPRMebel.DB.Repositories;
using WPRMebel.DB.TestSqlServer.Context;

namespace WPRMebel.TestConsole
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var repo = new NamedRepository<Category>(new CatalogDbContext());

          var item = await repo.GetByName("Категория 11");
          var res = await repo.Delete(item);
          var res2 = await repo.Delete(3);


            Console.ReadLine();
        }
    }
}

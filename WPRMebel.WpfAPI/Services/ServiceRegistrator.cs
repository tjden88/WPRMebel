using Microsoft.Extensions.DependencyInjection;
using WPRMebel.DB.Context;
using WPRMebel.DB.Repositories;
using WPRMebel.DB.TestSqlServer.Context;
using WPRMebel.Interfaces.Base.Repositories;
using WPRMebel.WpfAPI.Catalog;
using WPRMebel.WpfAPI.Catalog.Interfaces;

namespace WPRMebel.WpfAPI.Services
{
    public static class ServiceRegistrator
    {
        /// <summary> Зарегистрировать сервисы </summary>
        public static IServiceCollection AddApiServices(this IServiceCollection Services) => Services
            .AddScoped(typeof(IRepository<>), typeof(DbRepository<>))
            .AddScoped(typeof(INamedRepository<>), typeof(DbNamedRepository<>))
            .AddScoped<ICatalogElementView, CatalogElementView>()
        ;

        /// <summary> Зарегистрировать бд </summary>
        public static IServiceCollection AddDb(this IServiceCollection Services) => Services
            .AddDbContext<CatalogContextBase, CatalogDbContext>()
        ;
    }
}

using Microsoft.Extensions.DependencyInjection;
using WPRMebel.DB.Repositories;
using WPRMebel.DB.TestSqlServer.Context;
using WPRMebel.WpfAPI.Catalog;
using WPRMebel.WpfAPI.Interfaces;

namespace WPRMebel.WpfAPI.Services
{
    public static class ServiceRegistrator
    {
        /// <summary> Зарегистрировать сервисы </summary>
        public static IServiceCollection AddApiServices(this IServiceCollection Services) => Services
            .AddScoped(typeof(DbRepository<>))
            .AddScoped(typeof(ICatalogDbRepository<>), typeof(CatalogDbRepository<>))
            .AddScoped<CatalogViewer>()
        ;

        /// <summary> Зарегистрировать бд </summary>
        public static IServiceCollection AddDb(this IServiceCollection Services) => Services
            .AddDbContext<CatalogDbContext>()
        ;
    }
}

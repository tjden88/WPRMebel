using Microsoft.Extensions.DependencyInjection;
using WPRMebel.DB.Context;
using WPRMebel.DB.Repositories;
using WPRMebel.Interfaces.Base.Repositories;

namespace WPRMebel.WpfAPI.Services
{
    public static class ServiceRegistrator
    {
        /// <summary> Зарегистрировать сервисы </summary>
        public static IServiceCollection AddApiServices(this IServiceCollection Services) => Services
            .AddScoped(typeof(IRepository<>), typeof(DbRepository<>))
            .AddScoped(typeof(INamedRepository<>), typeof(DbNamedRepository<>))
        ;

        /// <summary> Зарегистрировать бд </summary>
        public static IServiceCollection AddDb(this IServiceCollection Services) => Services
            .AddDbContext<CatalogContext>()
        ;
    }
}

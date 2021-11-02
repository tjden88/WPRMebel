using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WPRMebel.DB.Context.Base
{
    /// <summary>
    /// Базовый класс контекста данных
    /// </summary>
    public abstract class BaseContext : DbContext
    {
        /// <summary> Имя БД. Используется в строке подключения </summary>
        public string DatabaseName { get; set; } = "WPRMebel";

        /// <summary> Заполнение начальными данными. </summary>
        public abstract Task InitializeStartData(CancellationToken Cancel = default);

        /// <summary> Конфигурировать контекст БД. Вызывается базовым классом в переопределённом методе OnConfiguring </summary>
        protected abstract void Configure(DbContextOptionsBuilder optionsBuilder);

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .LogTo(message => Debug.WriteLine(message), Microsoft.Extensions.Logging.LogLevel.Information)
                .UseLazyLoadingProxies()
                ;
            Configure(optionsBuilder);
        }
    }
}

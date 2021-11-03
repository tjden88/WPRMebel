using System.Collections.ObjectModel;
using System.Threading.Tasks;
using WPRMebel.Interfaces.Base.Entityes;

namespace WPRMebel.WpfAPI.Interfaces
{
    /// <summary>
    /// Просмотр содержимого БД для оконного интерфейса
    /// </summary>
    public interface ICatalogDbViewer<T> where T : IEntity
    {
        Task<ObservableCollection<T>> LoadItemsAsync();
    }
}

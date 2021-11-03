using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using DispatcherPriority = System.Windows.Threading.DispatcherPriority;

namespace System.Collections.ObjectModel
{
    internal static class ObservableCollectionExtensions
    {
        public static void Add<T>(this ObservableCollection<T> collection, IEnumerable<T> Items)
        {
            foreach (var item in Items)
                collection.Add(item);
        }

        public static void AddClear<T>(this ObservableCollection<T> collection, IEnumerable<T> Items)
        {
            collection.Clear();
            collection.Add(Items);
        }

        public static async Task AddAsync<T>(this ObservableCollection<T> collection, IEnumerable<T> Items)
        {
            await Task.Run(() =>
            {
                foreach (var item in Items)
                    Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Loaded,
                        (ThreadStart) delegate
                        {
                            collection.Add(item);
                        });
            });
        }
    }
}

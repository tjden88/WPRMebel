using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace WPRMebel.WPF.Extensions
{
    /// <summary>
    /// Коллекция с поддержкой пакетного добавления
    /// </summary>
    /// <typeparam name="T">Тип</typeparam>
    public class RangeObservableCollection<T> : ObservableCollection<T>
    {

        /// <summary>
        /// Очистить коллекцию и вставить новые элементы
        /// </summary>
        /// <param name="items">Коллекция новых элементов</param>
        public void AddRangeClear(IEnumerable<T> items)
        {
            ClearItems();
            AddRange(items);
        }

        /// <summary>
        /// Добавить новые элементы
        /// </summary>
        /// <param name="items">Коллекция новых элементов</param>
        public void AddRange(IEnumerable<T> items)
        {
            if (items == null) throw new ArgumentNullException(nameof(items));

            foreach (var item in items) Items.Add(item);

            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }
    }
}

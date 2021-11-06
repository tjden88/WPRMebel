using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Threading;

namespace WPRMebel.WPF.Extensions
{
    /// <summary>
    /// Коллекция с поддержкой пакетного добавления
    /// </summary>
    /// <typeparam name="T">Тип</typeparam>
    public class RangeObservableCollection<T> : ObservableCollection<T>
    {
        private readonly SynchronizationContext _SynchronizationContext = SynchronizationContext.Current;

        private bool _SuppressNotification;

        /// <summary>
        /// Очистить коллекцию и вставить новые элементы
        /// </summary>
        /// <param name="items">Коллекция новых элементов</param>
        public void AddRangeClear(IEnumerable<T> items)
        {
            _SuppressNotification = true;
            ClearItems();
            AddRange(items);
        }

        /// <summary>
        /// Добавить новые элементы
        /// </summary>
        /// <param name="items">Коллекция новых элементов</param>
        public void AddRange(IEnumerable<T> items)
        {
            if (items != null)
            {
                _SuppressNotification = true;
                foreach (var item in items) Add(item);
                _SuppressNotification = false;

                OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            }
        }

        public void RemoveRange(IEnumerable<T> collection)
        {
            if (collection != null)
            {
                _SuppressNotification = true;
                foreach (var item in collection) Remove(item);
                _SuppressNotification = false;

                OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            }
        }
        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            if (SynchronizationContext.Current == _SynchronizationContext)
            {
                // Execute the CollectionChanged event on the current thread
                RaiseCollectionChanged(e);
            }
            else
            {
                // Raises the CollectionChanged event on the creator thread
                _SynchronizationContext.Send(RaiseCollectionChanged, e);
            }
        }
        protected override void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (SynchronizationContext.Current == _SynchronizationContext)
            {
                // Execute the PropertyChanged event on the current thread
                RaisePropertyChanged(e);
            }
            else
            {
                // Raises the PropertyChanged event on the creator thread
                _SynchronizationContext.Send(RaisePropertyChanged, e);
            }
        }

        private void RaiseCollectionChanged(object param)
        {
            // We are in the creator thread, call the base implementation directly
            if (!_SuppressNotification)
                base.OnCollectionChanged((NotifyCollectionChangedEventArgs)param);
        }
        private void RaisePropertyChanged(object param)
        {
            // We are in the creator thread, call the base implementation directly
            base.OnPropertyChanged((PropertyChangedEventArgs)param);
        }
    }
}

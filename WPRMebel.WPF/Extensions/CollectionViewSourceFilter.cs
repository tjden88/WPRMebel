using System;
using System.Windows;
using System.Windows.Data;

namespace WPRMebel.WPF.Extensions
{
    /// <summary>
    /// Класс - посредник для обработки фильтрации CollectionViewSource внутри ViewModel
    /// </summary>
    public class CollectionViewSourceFilter
    {
        private readonly Action<FilterEventArgs> _OnFilterAction;

        private CollectionViewSource _CollectionViewSource;

        /// <summary>
        /// Класс - посредник для обработки фильтрации CollectionViewSource внутри ViewModel
        /// </summary>
        /// <param name="OnFilterAction">Действие фильтрации CollectionViewSource</param>
        public CollectionViewSourceFilter(Action<FilterEventArgs> OnFilterAction) => _OnFilterAction = OnFilterAction;

        /// <summary> Обновить представление коллекции </summary>
        public void RefreshSource() => _CollectionViewSource?.View.Refresh();


        private void DoFilter(object S, FilterEventArgs e) => _OnFilterAction?.Invoke(e);

        private static void FilterObjectOnChanged(DependencyObject D, DependencyPropertyChangedEventArgs E)
        {
            if (D is not CollectionViewSource cvs)
                throw new NotSupportedException("Свойство предназначено для CollectionViewSource");

            if (E.OldValue is CollectionViewSourceFilter oldValue)
            {
                oldValue._CollectionViewSource = null;
                cvs.Filter -= oldValue.DoFilter;
            }

            if (E.NewValue is CollectionViewSourceFilter newValue)
            {
                newValue._CollectionViewSource = cvs;
                cvs.Filter += newValue.DoFilter;
            }
        }



        #region AttachedProp : FilterObject

        /// <summary>
        /// Установить объект CollectionViewSourceFilter для фильтрации данных.
        /// Присоединённой свойство для CollectionViewSource
        /// </summary>
        public static readonly DependencyProperty FilterObjectProperty = DependencyProperty.RegisterAttached(
            "FilterObject",
            typeof(CollectionViewSourceFilter),
            typeof(CollectionViewSourceFilter),
            new PropertyMetadata(null, FilterObjectOnChanged));

        public static CollectionViewSourceFilter GetFilterObject(CollectionViewSource obj) => (CollectionViewSourceFilter)obj.GetValue(FilterObjectProperty);
        public static void SetFilterObject(CollectionViewSource obj, CollectionViewSourceFilter value) => obj.SetValue(FilterObjectProperty, value);

        #endregion
    }

}

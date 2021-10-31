using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace WPRMebel.WPF.Controls
{
    /// <summary>
    /// Контрол для отображения контента с анимацией при изменении контента
    /// </summary>
    public class MainPageViewer : Control
    {
        private readonly DoubleAnimation _ContentTranslateAnimation;
        private readonly DoubleAnimation _ContentTranslateAnimationBack;

        private readonly TranslateTransform _Transform = new();

        static MainPageViewer()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MainPageViewer), new FrameworkPropertyMetadata(typeof(MainPageViewer)));
        }

        public MainPageViewer()
        {
            RenderTransform = _Transform;
            _ContentTranslateAnimation = new()
            {
                Duration = TimeSpan.FromSeconds(0.3),
                EasingFunction = new CubicEase() {EasingMode = EasingMode.EaseIn}
            };
            _ContentTranslateAnimation.Completed += ContentTranslateAnimationOnCompleted;

            _ContentTranslateAnimationBack = new()
            {
                Duration = TimeSpan.FromSeconds(0.5),
                EasingFunction = new CubicEase() { EasingMode = EasingMode.EaseOut }
            };
            _ContentTranslateAnimationBack.Completed += ContentTranslateAnimationBackOnCompleted;
        }

        #region Content : object - Отображаемое содержимое

        /// <summary>Отображаемое содержимое</summary>
        public static readonly DependencyProperty ContentProperty =
            DependencyProperty.Register(
                nameof(Content),
                typeof(object),
                typeof(MainPageViewer),
                new PropertyMetadata(default, ContentChanged));

        private static void ContentChanged(DependencyObject D, DependencyPropertyChangedEventArgs E)
        {
            ((MainPageViewer)D).StartAnimation();
        }

        /// <summary>Отображаемое содержимое</summary>
        [Category("MainPageViewer")]
        [Description("Отображаемое содержимое")]
        public object Content
        {
            get => GetValue(ContentProperty);
            set => SetValue(ContentProperty, value);
        }

        #endregion

        #region ActualContent : object - Видимый контент

        /// <summary>Видимый контент</summary>
        public static readonly DependencyProperty ActualContentProperty =
            DependencyProperty.Register(
                nameof(ActualContent),
                typeof(object),
                typeof(MainPageViewer),
                new PropertyMetadata(default(object)));

        /// <summary>Видимый контент</summary>
        [Category("MainPageViewer")]
        [Description("Видимый контент")]
        public object ActualContent
        {
            get => GetValue(ActualContentProperty);
            set => SetValue(ActualContentProperty, value);
        }

        #endregion

        private void StartAnimation()
        {
            CacheMode = new BitmapCache();
            _ContentTranslateAnimation.To = -ActualWidth;
            _Transform.BeginAnimation(TranslateTransform.XProperty, _ContentTranslateAnimation);
        }

        private void ContentTranslateAnimationOnCompleted(object Sender, EventArgs E)
        {
            ActualContent = Content;
            _ContentTranslateAnimationBack.From = ActualWidth;
            _Transform.BeginAnimation(TranslateTransform.XProperty, _ContentTranslateAnimationBack);
        }

        private void ContentTranslateAnimationBackOnCompleted(object Sender, EventArgs E) => CacheMode = null;
    }
}

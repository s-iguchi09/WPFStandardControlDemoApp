using System.Windows;
using System.Windows.Controls.Primitives;

namespace WPFStandardControlDemoApp.Common.Behaviors
{
    /// <summary>
    /// Provides an attached property to set the initial selected index for a <see cref="Selector"/> once when it is loaded.
    /// <para><see cref="Selector"/> の読み込み時に一度だけ初期選択インデックスを設定するための添付プロパティを提供します。</para>
    /// </summary>
    public static class SelectorInitialSelectionBehavior
    {
        /// <summary>
        /// Gets or sets the initial selected index. This is applied only once when the control is loaded.
        /// <para>初期選択インデックスを取得または設定します。この値はコントロールの読み込み時に一度だけ適用されます。</para>
        /// </summary>
        public static readonly DependencyProperty InitialSelectedIndexProperty =
            DependencyProperty.RegisterAttached(
                "InitialSelectedIndex",
                typeof(int?),
                typeof(SelectorInitialSelectionBehavior),
                new PropertyMetadata(null, OnInitialSelectedIndexChanged));

        public static void SetInitialSelectedIndex(DependencyObject element, int? value)
            => element.SetValue(InitialSelectedIndexProperty, value);

        public static int? GetInitialSelectedIndex(DependencyObject element)
            => (int?)element.GetValue(InitialSelectedIndexProperty);

        private static void OnInitialSelectedIndexChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is not Selector selector)
            {
                throw new InvalidOperationException("This behavior can only be attached to a Selector control.");
            }

            WeakEventManager<Selector, RoutedEventArgs>.RemoveHandler(selector, nameof(Selector.Loaded), OnSelectorLoaded);

            if (selector.IsLoaded)
            {
                ApplyInitialIndex(selector);
            }
            else
            {
                WeakEventManager<Selector, RoutedEventArgs>.AddHandler(selector, nameof(Selector.Loaded), OnSelectorLoaded);
            }
        }

        private static void OnSelectorLoaded(object? sender, RoutedEventArgs e)
        {
            if (sender is not Selector selector) return;

            WeakEventManager<Selector, RoutedEventArgs>.RemoveHandler(selector, nameof(Selector.Loaded), OnSelectorLoaded);
            ApplyInitialIndex(selector);
        }

        private static void ApplyInitialIndex(Selector selector)
        {
            if (selector is null || selector.Items.Count == 0) return;

            var index = GetInitialSelectedIndex(selector);

            if (index is null || index.Value < -1) return;

            selector.SelectedIndex = index.Value;
        }
    }
}

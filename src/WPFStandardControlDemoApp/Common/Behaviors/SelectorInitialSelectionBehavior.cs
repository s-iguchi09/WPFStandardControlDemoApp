using System.Windows;
using System.Windows.Controls.Primitives;

namespace WPFStandardControlDemoApp.Common.Behaviors
{
    /// <summary>
    /// Selector の ItemsControl の初期化タイミングのズレにより、
    /// 設定済みの SelectedIndex に対応する項目が初期表示時に正しく選択されない問題を補正するためのビヘイビア。
    /// Loaded イベントのタイミングで SelectedIndex を再適用し、
    /// 初期選択状態を確実に反映させる。
    /// </summary>
    public static class SelectorInitialSelectionBehavior
    {
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
                throw new InvalidOperationException("SelectorInitialSelectionBehavior is for Selector only.");
            }

            selector.Loaded += (s, args) =>
            {
                var index = GetInitialSelectedIndex(selector);
                if (index.HasValue && selector.ItemsSource != null)
                {
                    selector.SelectedIndex = index.Value;
                }
            };
        }
    }
}

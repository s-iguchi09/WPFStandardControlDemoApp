using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Threading;

namespace WPFStandardControlDemoApp.Common.Behaviors
{
    /// <summary>
    /// ListBoxやDataGridにおいて、選択されたアイテムを自動的に表示領域までスクロールさせます。
    /// 型判定のみを用いた高速版です。
    /// </summary>
    public static class ScrollIntoViewBehavior
    {
        public static readonly DependencyProperty IsEnabledProperty =
            DependencyProperty.RegisterAttached(
                "IsEnabled",
                typeof(bool),
                typeof(ScrollIntoViewBehavior),
                new PropertyMetadata(false, OnIsEnabledChanged));

        public static void SetIsEnabled(DependencyObject d, bool value) => d.SetValue(IsEnabledProperty, value);
        public static bool GetIsEnabled(DependencyObject d) => (bool)d.GetValue(IsEnabledProperty);

        private static void OnIsEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            // ListBox, DataGrid, ListView などの共通基底クラス
            if (!(d is Selector selector)) return;

            // 弱参照イベントマネージャーを使用してリークを防止しつつ購読管理
            WeakEventManager<Selector, SelectionChangedEventArgs>.RemoveHandler(selector, nameof(Selector.SelectionChanged), OnSelectionChanged);

            if ((bool)e.NewValue)
            {
                WeakEventManager<Selector, SelectionChangedEventArgs>.AddHandler(selector, nameof(Selector.SelectionChanged), OnSelectionChanged);
            }
        }

        private static void OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
        {
            // 複数選択（範囲選択）時は、最後に「追加」されたアイテムにフォーカスを合わせる
            if (e.AddedItems.Count <= 0) return;
            var targetItem = e.AddedItems[e.AddedItems.Count - 1];

            if (!(sender is FrameworkElement element)) return;

            // DispatcherPriority.Background を使用することで、
            // 仮想化されたアイテムの生成やレイアウト更新が完了した後にスクロールを実行する
            element.Dispatcher.BeginInvoke(new Action(() =>
            {
                // 型判定による高速実行
                if (sender is ListBox listBox)
                {
                    // 実行の瞬間にまだ選択されているかチェック（チャタリング・連打対策）
                    if (listBox.SelectedItems.Contains(targetItem))
                    {
                        listBox.ScrollIntoView(targetItem);
                    }
                }
                else if (sender is DataGrid dataGrid)
                {
                    if (dataGrid.SelectedItems.Contains(targetItem))
                    {
                        dataGrid.ScrollIntoView(targetItem);
                    }
                }
            }), DispatcherPriority.Background);
        }
    }
}

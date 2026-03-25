using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace WPFStandardControlDemoApp.Common.Behaviors
{
    public static class DataGridRowMoveBehavior
    {
        public static readonly DependencyProperty IsEnabledProperty =
            DependencyProperty.RegisterAttached("IsEnabled", typeof(bool), typeof(DataGridRowMoveBehavior), new PropertyMetadata(false, OnIsEnabledChanged));

        public static void SetIsEnabled(DependencyObject d, bool value) => d.SetValue(IsEnabledProperty, value);
        public static bool GetIsEnabled(DependencyObject d) => (bool)d.GetValue(IsEnabledProperty);

        private static void OnIsEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(d is DataGrid dg)) return;

            // クリーンアップ
            WeakEventManager<DataGrid, MouseButtonEventArgs>.RemoveHandler(dg, nameof(DataGrid.PreviewMouseLeftButtonDown), OnMouseDown);
            WeakEventManager<DataGrid, MouseEventArgs>.RemoveHandler(dg, nameof(DataGrid.PreviewMouseMove), OnMouseMove);
            WeakEventManager<DataGrid, DragEventArgs>.RemoveHandler(dg, nameof(DataGrid.Drop), OnDrop);

            if ((bool)e.NewValue)
            {
                dg.AllowDrop = true;
                WeakEventManager<DataGrid, MouseButtonEventArgs>.AddHandler(dg, nameof(DataGrid.PreviewMouseLeftButtonDown), OnMouseDown);
                WeakEventManager<DataGrid, MouseEventArgs>.AddHandler(dg, nameof(DataGrid.PreviewMouseMove), OnMouseMove);
                WeakEventManager<DataGrid, DragEventArgs>.AddHandler(dg, nameof(DataGrid.Drop), OnDrop);
            }
        }

        private static Point _startPoint;

        private static void OnMouseDown(object? sender, MouseButtonEventArgs e)
        {
            _startPoint = e.GetPosition(null);
        }

        private static void OnMouseMove(object? sender, MouseEventArgs e)
        {
            if (e.LeftButton != MouseButtonState.Pressed) return;

            Point mousePos = e.GetPosition(null);
            Vector diff = _startPoint - mousePos;

            if (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance ||
                Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance)
            {
                var row = FindVisualParent<DataGridRow>((DependencyObject)e.OriginalSource);
                if (row == null || row.IsEditing) return;

                // ドラッグ開始（データをRow.Itemとして保持）
                DragDrop.DoDragDrop(row, row.Item, DragDropEffects.Move);
            }
        }

        private static void OnDrop(object? sender, DragEventArgs e)
        {
            if (!(sender is DataGrid dg)) return;

            // ドラッグされたアイテムを取得
            var droppedData = e.Data.GetData(e.Data.GetFormats()[0]);
            if (droppedData == null) return;

            // ドロップ先の行を取得
            var row = FindVisualParent<DataGridRow>((DependencyObject)e.OriginalSource);
            int targetIndex = (row != null) ? row.GetIndex() : dg.Items.Count - 1;

            // データソース（IList）を操作
            if (dg.ItemsSource is IList list)
            {
                int oldIndex = list.IndexOf(droppedData);
                if (oldIndex >= 0 && oldIndex != targetIndex)
                {
                    list.RemoveAt(oldIndex);
                    // 境界値チェック
                    if (targetIndex > list.Count) targetIndex = list.Count;
                    list.Insert(targetIndex, droppedData);

                    // 選択状態を維持
                    dg.SelectedIndex = targetIndex;
                }
            }
        }

        private static T FindVisualParent<T>(DependencyObject child) where T : DependencyObject
        {
            DependencyObject parentObject = VisualTreeHelper.GetParent(child);
            if (parentObject == null) return null!;
            return parentObject is T parent ? parent : FindVisualParent<T>(parentObject);
        }
    }
}

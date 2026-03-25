using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace WPFStandardControlDemoApp.Common.Behaviors
{
    /// <summary>
    /// DataGridのソート状態をリセットするためのトリガーを指定します。
    /// </summary>
    [Flags]
    public enum SortResetMode
    {
        None = 0,
        RightClick = 0b_0001, // 列ヘッダーの右クリックでリセット
        ShiftClick = 0b_0010, // 指定した修飾キー + 左クリックでリセット
        Cycle = 0b_0100, // 昇順 -> 降順 -> なし のループ挙動
        All = RightClick | ShiftClick | Cycle
    }

    /// <summary>
    /// DataGridの列ソートを初期状態（ソートなし）に戻す機能を提供します。
    /// </summary>
    public static class DataGridSortResetBehavior
    {
        #region Dependency Properties

        /// <summary>
        /// 有効にするリセットモードを指定します（ビットフラグ形式）。
        /// </summary>
        public static readonly DependencyProperty ResetModeProperty =
            DependencyProperty.RegisterAttached(
                "ResetMode",
                typeof(SortResetMode),
                typeof(DataGridSortResetBehavior),
                new PropertyMetadata(SortResetMode.None, OnResetModeChanged));

        public static void SetResetMode(DependencyObject d, SortResetMode value) => d.SetValue(ResetModeProperty, value);
        public static SortResetMode GetResetMode(DependencyObject d) => (SortResetMode)d.GetValue(ResetModeProperty);

        /// <summary>
        /// ShiftClickモード時に使用する修飾キーを指定します（デフォルトはShift）。
        /// </summary>
        public static readonly DependencyProperty ResetModifiersProperty =
            DependencyProperty.RegisterAttached(
                "ResetModifiers",
                typeof(ModifierKeys),
                typeof(DataGridSortResetBehavior),
                new PropertyMetadata(ModifierKeys.Shift));

        public static void SetResetModifiers(DependencyObject d, ModifierKeys value) => d.SetValue(ResetModifiersProperty, value);
        public static ModifierKeys GetResetModifiers(DependencyObject d) => (ModifierKeys)d.GetValue(ResetModifiersProperty);

        #endregion

        private static void OnResetModeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(d is DataGrid dataGrid)) return;

            // 既存の購読を解除してクリーンアップ
            WeakEventManager<DataGrid, MouseButtonEventArgs>.RemoveHandler(dataGrid, nameof(DataGrid.PreviewMouseRightButtonDown), OnPreviewMouseRightButtonDown);
            WeakEventManager<DataGrid, DataGridSortingEventArgs>.RemoveHandler(dataGrid, nameof(DataGrid.Sorting), OnSorting);

            var mode = (SortResetMode)e.NewValue;
            if (mode == SortResetMode.None) return;

            // モードに応じたイベントの購読
            if (mode.HasFlag(SortResetMode.RightClick))
            {
                WeakEventManager<DataGrid, MouseButtonEventArgs>.AddHandler(dataGrid, nameof(DataGrid.PreviewMouseRightButtonDown), OnPreviewMouseRightButtonDown);
            }

            if (mode.HasFlag(SortResetMode.ShiftClick) || mode.HasFlag(SortResetMode.Cycle))
            {
                WeakEventManager<DataGrid, DataGridSortingEventArgs>.AddHandler(dataGrid, nameof(DataGrid.Sorting), OnSorting);
            }
        }

        private static void OnPreviewMouseRightButtonDown(object? sender, MouseButtonEventArgs e)
        {
            if (!(sender is DataGrid dataGrid)) return;

            // 右クリックされた場所が列ヘッダーであればリセット実行
            if (IsHeaderClick(e))
            {
                ResetSort(dataGrid);
                e.Handled = true;
            }
        }

        private static void OnSorting(object? sender, DataGridSortingEventArgs e)
        {
            if (!(sender is DataGrid dataGrid)) return;
            var mode = GetResetMode(dataGrid);
            var modifiers = GetResetModifiers(dataGrid);

            // 1. 修飾キー付きクリック判定
            if (mode.HasFlag(SortResetMode.ShiftClick) && Keyboard.Modifiers == modifiers)
            {
                ResetSort(dataGrid);
                e.Handled = true;
                return;
            }

            // 2. Cycle（ループ）判定: 降順の次はソートなしに戻す
            if (mode.HasFlag(SortResetMode.Cycle) && e.Column.SortDirection == ListSortDirection.Descending)
            {
                ResetSort(dataGrid);
                e.Handled = true;
            }
        }

        private static bool IsHeaderClick(MouseButtonEventArgs e)
        {
            DependencyObject dep = (DependencyObject)e.OriginalSource;
            while (dep != null && !(dep is DataGridColumnHeader))
            {
                dep = VisualTreeHelper.GetParent(dep);
            }
            return dep is DataGridColumnHeader;
        }

        private static void ResetSort(DataGrid dataGrid)
        {
            ICollectionView view = CollectionViewSource.GetDefaultView(dataGrid.ItemsSource);
            if (view == null) return;

            // ビューのソート記述をクリア
            view.SortDescriptions.Clear();

            // 全列のソートアイコン（▲▼）を非表示にする
            foreach (var column in dataGrid.Columns)
            {
                column.SortDirection = null;
            }

            view.Refresh();
        }
    }
}

using System.Windows;
using System.Windows.Controls;
using WPFStandardControlDemoApp.Common.Helpers;

namespace WPFStandardControlDemoApp.Common.Behaviors
{
    /// <summary>
    /// Provides attached properties to control the appearance of the internal <see cref="Button"/> (PART_Button) in a <see cref="DatePicker"/>.
    /// <para>
    /// <see cref="DatePicker"/> 内部のカレンダーボタン (PART_Button) の外観を制御するための添付プロパティを提供します。
    /// </para>
    /// </summary>
    /// <seealso cref="DatePicker"/>
    public static class DatePickerIconBehavior
    {
        #region Attached Properties

        /// <summary>
        /// Gets or sets the <see cref="Thickness"/> for the internal PART_Button.
        /// <para>内部ボタンの余白 (<see cref="FrameworkElement.Margin"/>) を取得または設定します。</para>
        /// </summary>
        public static readonly DependencyProperty MarginProperty =
            DependencyProperty.RegisterAttached("Margin", typeof(Thickness), typeof(DatePickerIconBehavior), new PropertyMetadata(default(Thickness), OnPropertyChanged));

        /// <summary>
        /// Gets or sets the width for the internal PART_Button.
        /// <para>内部ボタンの幅 (<see cref="FrameworkElement.Width"/>) を取得または設定します。</para>
        /// </summary>
        public static readonly DependencyProperty WidthProperty =
            DependencyProperty.RegisterAttached("Width", typeof(double), typeof(DatePickerIconBehavior), new PropertyMetadata(double.NaN, OnPropertyChanged));

        /// <summary>
        /// Gets or sets the height for the internal PART_Button.
        /// <para>内部ボタンの高さ (<see cref="FrameworkElement.Height"/>) を取得または設定します。</para>
        /// </summary>
        public static readonly DependencyProperty HeightProperty =
            DependencyProperty.RegisterAttached("Height", typeof(double), typeof(DatePickerIconBehavior), new PropertyMetadata(double.NaN, OnPropertyChanged));

        /// <summary>
        /// Gets or sets the <see cref="HorizontalAlignment"/> for the internal PART_Button.
        /// <para>内部ボタンの水平方向の配置 (<see cref="FrameworkElement.HorizontalAlignment"/>) を取得または設定します。</para>
        /// </summary>
        public static readonly DependencyProperty HorizontalAlignmentProperty =
            DependencyProperty.RegisterAttached("HorizontalAlignment", typeof(HorizontalAlignment), typeof(DatePickerIconBehavior), new PropertyMetadata(HorizontalAlignment.Stretch, OnPropertyChanged));

        /// <summary>
        /// Gets or sets the <see cref="VerticalAlignment"/> for the internal PART_Button.
        /// <para>内部ボタンの垂直方向의 配置 (<see cref="FrameworkElement.VerticalAlignment"/>) を取得または設定します。</para>
        /// </summary>
        public static readonly DependencyProperty VerticalAlignmentProperty =
            DependencyProperty.RegisterAttached("VerticalAlignment", typeof(VerticalAlignment), typeof(DatePickerIconBehavior), new PropertyMetadata(VerticalAlignment.Stretch, OnPropertyChanged));

        /// <summary>
        /// Gets or sets the opacity for the internal PART_Button.
        /// <para>内部ボタンの不透明度 (<see cref="UIElement.Opacity"/>) を取得または設定します。</para>
        /// </summary>
        public static readonly DependencyProperty OpacityProperty =
            DependencyProperty.RegisterAttached("Opacity", typeof(double), typeof(DatePickerIconBehavior), new PropertyMetadata(1.0, OnPropertyChanged));

        /// <summary>
        /// Gets or sets the <see cref="Visibility"/> for the internal PART_Button.
        /// <para>内部ボタンの表示状態 (<see cref="UIElement.Visibility"/>) を取得または設定します。</para>
        /// </summary>
        public static readonly DependencyProperty VisibilityProperty =
            DependencyProperty.RegisterAttached("Visibility", typeof(Visibility), typeof(DatePickerIconBehavior), new PropertyMetadata(Visibility.Visible, OnPropertyChanged));

        public static void SetMargin(DependencyObject e, Thickness v) => e.SetValue(MarginProperty, v);
        public static Thickness GetMargin(DependencyObject e) => (Thickness)e.GetValue(MarginProperty);
        public static void SetWidth(DependencyObject e, double v) => e.SetValue(WidthProperty, v);
        public static double GetWidth(DependencyObject e) => (double)e.GetValue(WidthProperty);
        public static void SetHeight(DependencyObject e, double v) => e.SetValue(HeightProperty, v);
        public static double GetHeight(DependencyObject e) => (double)e.GetValue(HeightProperty);
        public static void SetHorizontalAlignment(DependencyObject e, HorizontalAlignment v) => e.SetValue(HorizontalAlignmentProperty, v);
        public static HorizontalAlignment GetHorizontalAlignment(DependencyObject e) => (HorizontalAlignment)e.GetValue(HorizontalAlignmentProperty);
        public static void SetVerticalAlignment(DependencyObject e, VerticalAlignment v) => e.SetValue(VerticalAlignmentProperty, v);
        public static VerticalAlignment GetVerticalAlignment(DependencyObject e) => (VerticalAlignment)e.GetValue(VerticalAlignmentProperty);
        public static void SetOpacity(DependencyObject e, double v) => e.SetValue(OpacityProperty, v);
        public static double GetOpacity(DependencyObject e) => (double)e.GetValue(OpacityProperty);
        public static void SetVisibility(DependencyObject e, Visibility v) => e.SetValue(VisibilityProperty, v);
        public static Visibility GetVisibility(DependencyObject e) => (Visibility)e.GetValue(VisibilityProperty);

        #endregion

        #region Internal Management

        /// <summary>
        /// Tracks whether a <see cref="FrameworkElement.Loaded"/> event handler is already registered.
        /// <para>Loaded イベントの待機状態を管理するための内部用プロパティです。</para>
        /// </summary>
        private static readonly DependencyProperty IsWaitingForLoadedProperty =
            DependencyProperty.RegisterAttached("IsWaitingForLoaded", typeof(bool), typeof(DatePickerIconBehavior), new PropertyMetadata(false));

        private static void OnPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is not DatePicker datePicker) return;

            if (datePicker.IsLoaded)
            {
                ApplyAll(datePicker);
                return;
            }

            if ((bool)datePicker.GetValue(IsWaitingForLoadedProperty)) return;
            datePicker.SetValue(IsWaitingForLoadedProperty, true);

            WeakEventManager<FrameworkElement, RoutedEventArgs>.AddHandler(datePicker, nameof(FrameworkElement.Loaded), OnDatePickerLoaded);
        }

        private static void OnDatePickerLoaded(object? sender, RoutedEventArgs e)
        {
            if (sender is not DatePicker datePicker) return;
            datePicker.ClearValue(IsWaitingForLoadedProperty);
            WeakEventManager<FrameworkElement, RoutedEventArgs>.RemoveHandler(datePicker, nameof(FrameworkElement.Loaded), OnDatePickerLoaded);

            ApplyAll(datePicker);
        }

        #endregion

        /// <summary>
        /// Finds the internal PART_Button and ensures all relevant properties are bound using <see cref="BindingHelper.EnsureBinding"/>.
        /// <para>内部パーツ (PART_Button) を探し、<see cref="BindingHelper.EnsureBinding"/> を使用してすべてのバインディングを確立します。</para>
        /// </summary>
        private static void ApplyAll(DatePicker dp)
        {
            if (dp.Template?.FindName("PART_Button", dp) is not Button button) return;

            BindingHelper.EnsureBinding(dp, button, MarginProperty, FrameworkElement.MarginProperty);
            BindingHelper.EnsureBinding(dp, button, WidthProperty, FrameworkElement.WidthProperty);
            BindingHelper.EnsureBinding(dp, button, HeightProperty, FrameworkElement.HeightProperty);
            BindingHelper.EnsureBinding(dp, button, HorizontalAlignmentProperty, FrameworkElement.HorizontalAlignmentProperty);
            BindingHelper.EnsureBinding(dp, button, VerticalAlignmentProperty, FrameworkElement.VerticalAlignmentProperty);
            BindingHelper.EnsureBinding(dp, button, OpacityProperty, UIElement.OpacityProperty);
            BindingHelper.EnsureBinding(dp, button, VisibilityProperty, UIElement.VisibilityProperty);
        }
    }
}

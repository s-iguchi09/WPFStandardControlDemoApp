using System.Windows;
using WPFStandardControlDemoApp.Common.Behaviors.Bases;

namespace WPFStandardControlDemoApp.Common.Behaviors
{
    /// <summary>
    /// Attached properties to restrict TextBox input to <see cref="decimal"/> values.
    /// <para>TextBox入力を <see cref="decimal"/> 型に制限する添付プロパティを提供します。</para>
    /// </summary>
    public class TextBoxDecimalInputBehavior : TextBoxNumberInputBehaviorBase<decimal, TextBoxDecimalInputBehavior>
    {
        /// <summary>
        /// Gets or sets the value bound to the TextBox.
        /// <para>TextBoxにバインドされる値を取得または設定します。</para>
        /// </summary>
        public static readonly DependencyProperty ValueProperty = DependencyProperty.RegisterAttached("Value", typeof(decimal), typeof(TextBoxDecimalInputBehavior), new FrameworkPropertyMetadata(0m, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
        /// <summary>
        /// Gets or sets the minimum allowed value.
        /// <para>許可される最小値を取得または設定します。</para>
        /// </summary>
        public static readonly DependencyProperty MinValueProperty = DependencyProperty.RegisterAttached("MinValue", typeof(decimal), typeof(TextBoxDecimalInputBehavior), new PropertyMetadata(decimal.MinValue));
        /// <summary>
        /// Gets or sets the maximum allowed value.
        /// <para>許可される最大値を取得または設定します。</para>
        /// </summary>
        public static readonly DependencyProperty MaxValueProperty = DependencyProperty.RegisterAttached("MaxValue", typeof(decimal), typeof(TextBoxDecimalInputBehavior), new PropertyMetadata(decimal.MaxValue));
        /// <summary>
        /// Gets or sets whether negative numbers are allowed.
        /// <para>負の数を許可するかどうかを取得または設定します。</para>
        /// </summary>
        public static readonly DependencyProperty AllowNegativeProperty = DependencyProperty.RegisterAttached("AllowNegative", typeof(bool), typeof(TextBoxDecimalInputBehavior), new PropertyMetadata(false));
        /// <summary>
        /// Gets or sets whether to block invalid input immediately.
        /// <para>不正な入力を即座にブロックするかどうかを取得または設定します。</para>
        /// </summary>
        public static readonly DependencyProperty PreventInvalidInputProperty = DependencyProperty.RegisterAttached("PreventInvalidInput", typeof(bool), typeof(TextBoxDecimalInputBehavior), new PropertyMetadata(false));
        /// <summary>
        /// Gets or sets the message for range errors.
        /// <para>範囲外エラー時のメッセージを取得または設定します。</para>
        /// </summary>
        public static readonly DependencyProperty RangeErrorMessageProperty = DependencyProperty.RegisterAttached("RangeErrorMessage", typeof(string), typeof(TextBoxDecimalInputBehavior), new PropertyMetadata(null));
        /// <summary>
        /// Gets or sets the message for format errors.
        /// <para>形式不正エラー時のメッセージを取得または設定します。</para>
        /// </summary>
        public static readonly DependencyProperty FormatErrorMessageProperty = DependencyProperty.RegisterAttached("FormatErrorMessage", typeof(string), typeof(TextBoxDecimalInputBehavior), new PropertyMetadata(null));

        protected override bool IsDecimalAllowed => true;
        protected override DependencyProperty ValuePropertyDescriptor => ValueProperty;
        protected override DependencyProperty MinValuePropertyDescriptor => MinValueProperty;
        protected override DependencyProperty MaxValuePropertyDescriptor => MaxValueProperty;
        protected override DependencyProperty AllowNegativePropertyDescriptor => AllowNegativeProperty;
        protected override DependencyProperty PreventInvalidInputPropertyDescriptor => PreventInvalidInputProperty;
        protected override DependencyProperty RangeErrorMessagePropertyDescriptor => RangeErrorMessageProperty;
        protected override DependencyProperty FormatErrorMessagePropertyDescriptor => FormatErrorMessageProperty;
        protected override bool TryParse(string text, out decimal result) => decimal.TryParse(text, out result);

        public static new void SetIsEnabled(DependencyObject obj, bool value) => obj.SetValue(IsEnabledProperty, value);
        public static new bool GetIsEnabled(DependencyObject obj) => (bool)obj.GetValue(IsEnabledProperty);

        public static new void SetValue(DependencyObject obj, decimal value) => obj.SetValue(ValueProperty, value);
        public static new decimal GetValue(DependencyObject obj) => (decimal)obj.GetValue(ValueProperty);

        public static new void SetMinValue(DependencyObject obj, decimal value) => obj.SetValue(MinValueProperty, value);
        public static new decimal GetMinValue(DependencyObject obj) => (decimal)obj.GetValue(MinValueProperty);

        public static new void SetMaxValue(DependencyObject obj, decimal value) => obj.SetValue(MaxValueProperty, value);
        public static new decimal GetMaxValue(DependencyObject obj) => (decimal)obj.GetValue(MaxValueProperty);

        public static new void SetAllowNegative(DependencyObject obj, bool value) => obj.SetValue(AllowNegativeProperty, value);
        public static new bool GetAllowNegative(DependencyObject obj) => (bool)obj.GetValue(AllowNegativeProperty);

        public static new void SetPreventInvalidInput(DependencyObject obj, bool value) => obj.SetValue(PreventInvalidInputProperty, value);
        public static new bool GetPreventInvalidInput(DependencyObject obj) => (bool)obj.GetValue(PreventInvalidInputProperty);

        public static new void SetRangeErrorMessage(DependencyObject obj, string value) => obj.SetValue(RangeErrorMessageProperty, value);
        public static new string GetRangeErrorMessage(DependencyObject obj) => (string)obj.GetValue(RangeErrorMessageProperty);

        public static new void SetFormatErrorMessage(DependencyObject obj, string value) => obj.SetValue(FormatErrorMessageProperty, value);
        public static new string GetFormatErrorMessage(DependencyObject obj) => (string)obj.GetValue(FormatErrorMessageProperty);
    }
}

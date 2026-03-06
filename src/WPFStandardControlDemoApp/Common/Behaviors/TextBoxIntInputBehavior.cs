using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using WPFStandardControlDemoApp.Common.Behaviors.Bases;

namespace WPFStandardControlDemoApp.Common.Behaviors
{
    /// <summary>
    /// Attached properties to restrict TextBox input to <see cref="int"/> values.
    /// <para>TextBox入力を <see cref="int"/> 型に制限する添付プロパティを提供します。</para>
    /// </summary>
    public class TextBoxIntInputBehavior : TextBoxNumberInputBehaviorBase<int, TextBoxIntInputBehavior>
    {
        /// <summary>
        /// Gets or sets the integer value bound to the TextBox.
        /// <para>TextBoxにバインドされる整数値を取得または設定します。</para>
        /// </summary>
        public static readonly DependencyProperty ValueProperty = DependencyProperty.RegisterAttached("Value", typeof(int), typeof(TextBoxIntInputBehavior), new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        /// <summary>
        /// Gets or sets the minimum allowed value.
        /// <para>許可される最小値を取得または設定します。</para>
        /// </summary>
        public static readonly DependencyProperty MinValueProperty = DependencyProperty.RegisterAttached("MinValue", typeof(int), typeof(TextBoxIntInputBehavior), new PropertyMetadata(int.MinValue));

        /// <summary>
        /// Gets or sets the maximum allowed value.
        /// <para>許可される最大値を取得または設定します。</para>
        /// </summary>
        public static readonly DependencyProperty MaxValueProperty = DependencyProperty.RegisterAttached("MaxValue", typeof(int), typeof(TextBoxIntInputBehavior), new PropertyMetadata(int.MaxValue));

        /// <summary>
        /// Gets or sets whether negative numbers are allowed.
        /// <para>負の数を許可するかどうかを取得または設定します。</para>
        /// </summary>
        public static readonly DependencyProperty AllowNegativeProperty = DependencyProperty.RegisterAttached("AllowNegative", typeof(bool), typeof(TextBoxIntInputBehavior), new PropertyMetadata(false));

        /// <summary>
        /// Gets or sets whether to block invalid input immediately.
        /// <para>不正な入力を即座にブロックするかどうかを取得または設定します。</para>
        /// </summary>
        public static readonly DependencyProperty PreventInvalidInputProperty = DependencyProperty.RegisterAttached("PreventInvalidInput", typeof(bool), typeof(TextBoxIntInputBehavior), new PropertyMetadata(false));

        /// <summary>
        /// Gets or sets the message for range errors.
        /// <para>範囲外エラー時のメッセージを取得または設定します。</para>
        /// </summary>
        public static readonly DependencyProperty RangeErrorMessageProperty = DependencyProperty.RegisterAttached("RangeErrorMessage", typeof(string), typeof(TextBoxIntInputBehavior), new PropertyMetadata(null));

        /// <summary>
        /// Gets or sets the message for format errors.
        /// <para>形式不正エラー時のメッセージを取得または設定します。</para>
        /// </summary>
        public static readonly DependencyProperty FormatErrorMessageProperty = DependencyProperty.RegisterAttached("FormatErrorMessage", typeof(string), typeof(TextBoxIntInputBehavior), new PropertyMetadata(null));

        protected override bool IsDecimalAllowed => false;
        protected override DependencyProperty ValuePropertyDescriptor => ValueProperty;
        protected override DependencyProperty MinValuePropertyDescriptor => MinValueProperty;
        protected override DependencyProperty MaxValuePropertyDescriptor => MaxValueProperty;
        protected override DependencyProperty AllowNegativePropertyDescriptor => AllowNegativeProperty;
        protected override DependencyProperty PreventInvalidInputPropertyDescriptor => PreventInvalidInputProperty;
        protected override DependencyProperty RangeErrorMessagePropertyDescriptor => RangeErrorMessageProperty;
        protected override DependencyProperty FormatErrorMessagePropertyDescriptor => FormatErrorMessageProperty;
        protected override bool TryParse(string text, out int result) => int.TryParse(text, out result);

        public static new void SetIsEnabled(DependencyObject obj, bool value) => obj.SetValue(IsEnabledProperty, value);
        public static new bool GetIsEnabled(DependencyObject obj) => (bool)obj.GetValue(IsEnabledProperty);

        public static new void SetValue(DependencyObject obj, int value) => obj.SetValue(ValueProperty, value);
        public static new int GetValue(DependencyObject obj) => (int)obj.GetValue(ValueProperty);

        public static new void SetMinValue(DependencyObject obj, int value) => obj.SetValue(MinValueProperty, value);
        public static new int GetMinValue(DependencyObject obj) => (int)obj.GetValue(MinValueProperty);

        public static new void SetMaxValue(DependencyObject obj, int value) => obj.SetValue(MaxValueProperty, value);
        public static new int GetMaxValue(DependencyObject obj) => (int)obj.GetValue(MaxValueProperty);

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

using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WPFStandardControlDemoApp.Common.Behaviors.WeakEventManagers;

namespace WPFStandardControlDemoApp.Common.Behaviors.Bases
{
    /// <summary>
    /// Provides a base class for TextBox behaviors that restrict input to numeric values and synchronize with a typed property.
    /// <para>TextBox入力を数値に制限し、型指定されたプロパティと同期するBehaviorの基底クラスを提供します。</para>
    /// </summary>
    /// <typeparam name="T">The numeric type (e.g., int, double). <para>数値型（int, doubleなど）</para></typeparam>
    /// <typeparam name="TSelf">The type of the derived class. <para>派生クラスの型</para></typeparam>
    public abstract class TextBoxNumberInputBehaviorBase<T, TSelf> where T : struct, IComparable<T> where TSelf : TextBoxNumberInputBehaviorBase<T, TSelf>
    {
        private static TSelf? _instance;
        private static TSelf Instance => _instance ??= (TSelf)System.Runtime.Serialization.FormatterServices.GetUninitializedObject(typeof(TSelf));

        protected abstract DependencyProperty ValuePropertyDescriptor { get; }
        protected abstract DependencyProperty MinValuePropertyDescriptor { get; }
        protected abstract DependencyProperty MaxValuePropertyDescriptor { get; }
        protected abstract DependencyProperty AllowNegativePropertyDescriptor { get; }
        protected abstract DependencyProperty PreventInvalidInputPropertyDescriptor { get; }
        protected abstract DependencyProperty RangeErrorMessagePropertyDescriptor { get; }
        protected abstract DependencyProperty FormatErrorMessagePropertyDescriptor { get; }

        protected abstract bool IsDecimalAllowed { get; }
        protected abstract bool TryParse(string text, out T result);

        public static T GetValue(DependencyObject obj) => (T)obj.GetValue(Instance.ValuePropertyDescriptor);
        public static void SetValue(DependencyObject obj, T value) => obj.SetValue(Instance.ValuePropertyDescriptor, value);

        public static T GetMinValue(DependencyObject obj) => (T)obj.GetValue(Instance.MinValuePropertyDescriptor);
        public static void SetMinValue(DependencyObject obj, T value) => obj.SetValue(Instance.MinValuePropertyDescriptor, value);

        public static T GetMaxValue(DependencyObject obj) => (T)obj.GetValue(Instance.MaxValuePropertyDescriptor);
        public static void SetMaxValue(DependencyObject obj, T value) => obj.SetValue(Instance.MaxValuePropertyDescriptor, value);

        public static bool GetAllowNegative(DependencyObject obj) => (bool)obj.GetValue(Instance.AllowNegativePropertyDescriptor);
        public static void SetAllowNegative(DependencyObject obj, bool value) => obj.SetValue(Instance.AllowNegativePropertyDescriptor, value);

        public static bool GetPreventInvalidInput(DependencyObject obj) => (bool)obj.GetValue(Instance.PreventInvalidInputPropertyDescriptor);
        public static void SetPreventInvalidInput(DependencyObject obj, bool value) => obj.SetValue(Instance.PreventInvalidInputPropertyDescriptor, value);

        public static string GetRangeErrorMessage(DependencyObject obj) => (string)obj.GetValue(Instance.RangeErrorMessagePropertyDescriptor);
        public static void SetRangeErrorMessage(DependencyObject obj, string value) => obj.SetValue(Instance.RangeErrorMessagePropertyDescriptor, value);

        public static string GetFormatErrorMessage(DependencyObject obj) => (string)obj.GetValue(Instance.FormatErrorMessagePropertyDescriptor);
        public static void SetFormatErrorMessage(DependencyObject obj, string value) => obj.SetValue(Instance.FormatErrorMessagePropertyDescriptor, value);

        /// <summary>
        /// Gets or sets whether this behavior is enabled.
        /// <para>このBehaviorが有効かどうかを取得または設定します。</para>
        /// </summary>
        public static readonly DependencyProperty IsEnabledProperty = DependencyProperty.RegisterAttached("IsEnabled", typeof(bool), typeof(TSelf), new PropertyMetadata(false, OnIsEnabledChanged));
        public static void SetIsEnabled(DependencyObject obj, bool value) => obj.SetValue(IsEnabledProperty, value);
        public static bool GetIsEnabled(DependencyObject obj) => (bool)obj.GetValue(IsEnabledProperty);

        protected static void OnIsEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is not TextBox textBox)
            {
                throw new InvalidOperationException("This behavior can only be attached to a TextBox control.");
            }

            var instance = (TextBoxNumberInputBehaviorBase<T, TSelf>?)Activator.CreateInstance(typeof(TSelf));

            if (instance is null) return;

            Detach(textBox);

            if (e.NewValue is not bool isEnabled || isEnabled is false)
            {
                InputMethod.SetIsInputMethodEnabled(textBox, true);
                return;
            }

            InputMethod.SetIsInputMethodEnabled(textBox, false);
            Attach(textBox);

            textBox.Unloaded += TextBox_Unloaded;
        }

        private static void TextBox_Unloaded(object? sender, RoutedEventArgs e)
        {
            if (sender is not TextBox textBox)
            {
                return;
            }

            Detach(textBox);
        }

        private static void Attach(TextBox textBox)
        {
            WeakEventManager<TextBox, TextCompositionEventArgs>.AddHandler(textBox, nameof(TextBox.PreviewTextInput), TextBox_PreviewTextInput);
            WeakEventManager<TextBox, TextChangedEventArgs>.AddHandler(textBox, nameof(TextBox.TextChanged), TextBox_TextChanged);
            WeakEventManager<TextBox, RoutedEventArgs>.AddHandler(textBox, nameof(TextBox.LostFocus), TextBox_LostFocus);
            WeakEventManager<FrameworkElement, RoutedEventArgs>.AddHandler(textBox, nameof(TextBox.Unloaded), TextBox_Unloaded);
            DataObjectPastingWeakEventManager.AddHandler(textBox, OnPasting);
        }

        private static void Detach(TextBox textBox)
        {
            WeakEventManager<TextBox, TextCompositionEventArgs>.RemoveHandler(textBox, nameof(TextBox.PreviewTextInput), TextBox_PreviewTextInput);
            WeakEventManager<TextBox, TextChangedEventArgs>.RemoveHandler(textBox, nameof(TextBox.TextChanged), TextBox_TextChanged);
            WeakEventManager<TextBox, RoutedEventArgs>.RemoveHandler(textBox, nameof(TextBox.LostFocus), TextBox_LostFocus);
            WeakEventManager<FrameworkElement, RoutedEventArgs>.RemoveHandler(textBox, nameof(TextBox.Unloaded), TextBox_Unloaded);
            DataObjectPastingWeakEventManager.RemoveHandler(textBox, OnPasting);
        }

        private static void OnPastingInternal(object? sender, DataObjectPastingEventArgs e)
        {
            if (sender is not TextBox textBox) return;
            if (e.DataObject.GetDataPresent(typeof(string)) is false) { e.CancelCommand(); return; }

            string pasteText = (string)e.DataObject.GetData(typeof(string));
            string futureText = GetFutureText(textBox, pasteText);

            if (IsParsableAsPartial(textBox, futureText) is false)
            {
                e.CancelCommand();
                return;
            }

            if (GetPreventInvalidInput(textBox) && IsInRange(textBox, futureText) is false)
            {
                e.CancelCommand();
            }
        }

        private static void OnPasting(object? sender, EventArgs e)
        {
            if (e is not DataObjectPastingEventArgs pastingArgs)
            {
                return;
            }

            OnPastingInternal(sender, pastingArgs);
        }

        private static void TextBox_PreviewTextInput(object? sender, TextCompositionEventArgs e)
        {
            if (sender is not TextBox textBox) return;
            string futureText = GetFutureText(textBox, e.Text);

            if (IsParsableAsPartial(textBox, futureText) is false)
            {
                e.Handled = true;
                return;
            }

            if (GetPreventInvalidInput(textBox) && IsInRange(textBox, futureText) is false)
            {
                e.Handled = true;
            }
        }

        private static void TextBox_TextChanged(object? sender, TextChangedEventArgs e)
        {
            if (sender is not TextBox textBox) return;
            if (IsIntermediateText(textBox.Text)) return;
            if (textBox.GetBindingExpression(Instance.ValuePropertyDescriptor) is not System.Windows.Data.BindingExpression binding) return;

            if (Instance.TryParse(textBox.Text, out T result) is false)
            {
                string customFormatMsg = GetFormatErrorMessage(textBox);
                MarkError(binding, string.IsNullOrWhiteSpace(customFormatMsg) ? "Invalid format." : customFormatMsg);
                return;
            }

            T min = GetMinValue(textBox);
            T max = GetMaxValue(textBox);

            if (result.CompareTo(min) < 0 || result.CompareTo(max) > 0)
            {
                string customRangeMsg = GetRangeErrorMessage(textBox) as string ?? string.Empty;
                MarkError(binding, string.IsNullOrWhiteSpace(customRangeMsg) ? $"Value must be between {min} and {max}." : customRangeMsg);
                return;
            }

            Validation.ClearInvalid(binding);
            SetValue(textBox, result);
        }

        private static void TextBox_LostFocus(object? sender, RoutedEventArgs e)
        {
            if (sender is not TextBox textBox) return;
            if (Instance.TryParse(textBox.Text, out T val) is false) return;

            T min = GetMinValue(textBox);
            T max = GetMaxValue(textBox);
            T clamped = val.CompareTo(min) < 0 ? min : (val.CompareTo(max) > 0 ? max : val);

            textBox.Text = clamped.ToString();
            SetValue(textBox, clamped);

            if (textBox.GetBindingExpression(Instance.ValuePropertyDescriptor) is not System.Windows.Data.BindingExpression binding)
            {
                return;
            }

            Validation.ClearInvalid(binding);
        }

        private static string GetFutureText(TextBox tb, string input) => tb.Text.Remove(tb.SelectionStart, tb.SelectionLength).Insert(tb.SelectionStart, input);

        private static bool IsIntermediateText(string text) => string.IsNullOrEmpty(text) || text is "-" or "." or "-.";

        private static bool IsParsableAsPartial(TextBox tb, string text)
        {
            if (!IsNegativeInputAllowed(tb, text)) return false;
            if (IsIntermediateText(text))
            {
                if (text.Contains(".") && Instance.IsDecimalAllowed is false) return false;
                return true;
            }
            return Instance.TryParse(text, out _);
        }


        private static bool IsInRange(TextBox tb, string text)
        {
            if (IsIntermediateText(text)) return true;
            if (Instance.TryParse(text, out T result) is false) return false;
            if (!IsNegativeInputAllowed(tb, text)) return false;

            return result.CompareTo(GetMinValue(tb)) >= 0 &&
                   result.CompareTo(GetMaxValue(tb)) <= 0;
        }

        private static bool IsNegativeInputAllowed(TextBox tb, string text)
        {
            if (!text.Contains("-")) return true;

            return GetAllowNegative(tb);
        }

        private static void MarkError(System.Windows.Data.BindingExpression binding, string message)
        {
            var ve = new ValidationError(new ExceptionValidationRule(), binding) { ErrorContent = message };
            Validation.MarkInvalid(binding, ve);
        }
    }
}

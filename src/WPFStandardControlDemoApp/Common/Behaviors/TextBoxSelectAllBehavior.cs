using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace WPFStandardControlDemoApp.Common.Behaviors
{
    /// <summary>
    /// Provides the behavior to select all text when the TextBox gets focus.
    /// <br/>TextBoxがフォーカスを得た際にテキストを全選択する振る舞いを提供します。
    /// </summary>
    public static partial class TextBoxSelectAllBehavior
    {
        /// <summary>
        /// Attached property to enable or disable the "Select All on Focus" behavior.
        /// <br/>フォーカス取得時の「全選択」機能の有効・無効を設定するための添付プロパティです。
        /// </summary>
        public static readonly DependencyProperty EnabledProperty =
            DependencyProperty.RegisterAttached(
                "IsEnabled",
                typeof(bool),
                typeof(TextBoxSelectAllBehavior),
                new PropertyMetadata(false, OnIsEnabledChanged));

        public static void SetIsEnabled(DependencyObject obj, bool value) => obj.SetValue(EnabledProperty, value);
        public static bool GetIsEnabled(DependencyObject obj) => (bool)obj.GetValue(EnabledProperty);

        private static void OnIsEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is not TextBox textBox)
            {
                throw new InvalidOperationException("This behavior can only be attached to a TextBox control.");
            }

            WeakEventManager<TextBox, KeyboardFocusChangedEventArgs>.RemoveHandler(textBox, nameof(textBox.GotKeyboardFocus), OnGotKeyboardFocus);
            WeakEventManager<TextBox, MouseButtonEventArgs>.RemoveHandler(textBox, nameof(textBox.PreviewMouseLeftButtonDown), OnPreviewMouseLeftButtonDown);

            if (e.NewValue is not bool isEnabled || isEnabled is false)
            {
                return;
            }

            WeakEventManager<TextBox, KeyboardFocusChangedEventArgs>.AddHandler(textBox, nameof(textBox.GotKeyboardFocus), OnGotKeyboardFocus);
            WeakEventManager<TextBox, MouseButtonEventArgs>.AddHandler(textBox, nameof(textBox.PreviewMouseLeftButtonDown), OnPreviewMouseLeftButtonDown);
        }

        private static void OnGotKeyboardFocus(object? sender, KeyboardFocusChangedEventArgs e)
        {
            SelectAll(sender as TextBox);
        }

        private static void OnPreviewMouseLeftButtonDown(object? sender, MouseButtonEventArgs e)
        {
            if (sender is not TextBox textBox || textBox.IsKeyboardFocusWithin) return;

            textBox.Focus();
            SelectAll(textBox);

            e.Handled = true;
        }

        private static void SelectAll(TextBox? textBox)
        {
            if (textBox == null) return;

            textBox.Dispatcher.BeginInvoke(new Action(() =>
            {
                if (textBox.IsKeyboardFocusWithin)
                {
                    textBox.SelectAll();
                }
            }), DispatcherPriority.Background);
        }
    }
}

using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WPFStandardControlDemoApp.Common.Behaviors
{
    /// <summary>
    /// TextBox に対して数値（int 型）以外の入力を禁止するビヘイビアです。
    /// 不正な文字入力を自動的に抑制し、数値入力専用の TextBox を実現します。
    /// </summary>
    public static class TextBoxIntOnlyBehavior
    {
        public static readonly DependencyProperty EnableProperty =
            DependencyProperty.RegisterAttached(
                "Enable",
                typeof(bool),
                typeof(TextBoxIntOnlyBehavior),
                new PropertyMetadata(false, OnEnableChanged));

        public static void SetEnable(DependencyObject obj, bool value)
            => obj.SetValue(EnableProperty, value);

        public static bool GetEnable(DependencyObject obj)
            => (bool)obj.GetValue(EnableProperty);

        public static readonly DependencyProperty AllowNegativeProperty =
            DependencyProperty.RegisterAttached(
                "AllowNegative",
                typeof(bool),
                typeof(TextBoxIntOnlyBehavior),
                new PropertyMetadata(false));

        public static void SetAllowNegative(DependencyObject obj, bool value)
            => obj.SetValue(AllowNegativeProperty, value);

        public static bool GetAllowNegative(DependencyObject obj)
            => (bool)obj.GetValue(AllowNegativeProperty);

        private static void OnEnableChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is not TextBox tb)
            {
                throw new InvalidOperationException("TextBoxIntOnlyBehavior is for TextBox only.");
            }

            if ((bool)e.NewValue)
            {
                InputMethod.SetIsInputMethodEnabled(tb, false);

                tb.PreviewTextInput += OnPreviewTextInput;
                DataObject.AddPastingHandler(tb, OnPaste);
            }
            else
            {
                tb.PreviewTextInput -= OnPreviewTextInput;
                DataObject.RemovePastingHandler(tb, OnPaste);
            }
        }

        private static void OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var tb = (TextBox)sender;
            bool allowNegative = GetAllowNegative(tb);

            foreach (char c in e.Text)
            {
                if (char.IsDigit(c)) continue;

                if (c == '-' && allowNegative)
                {
                    if (tb.SelectionStart == 0 && !tb.Text.Contains("-"))
                        continue;
                }

                e.Handled = true;
                return;
            }
        }

        private static void OnPaste(object sender, DataObjectPastingEventArgs e)
        {
            var tb = (TextBox)sender;
            bool allowNegative = GetAllowNegative(tb);

            if (e.DataObject.GetDataPresent(DataFormats.Text))
            {
                string paste = (string)e.DataObject.GetData(DataFormats.Text);

                if (!IsValidInt(paste, allowNegative))
                {
                    e.CancelCommand();
                }
            }
            else
            {
                e.CancelCommand();
            }
        }

        private static bool IsValidInt(string text, bool allowNegative)
        {
            if (string.IsNullOrEmpty(text)) return false;

            int start = 0;

            if (text[0] == '-')
            {
                if (!allowNegative) return false;
                start = 1;
            }

            for (int i = start; i < text.Length; i++)
            {
                if (!char.IsDigit(text[i])) return false;
            }

            return true;
        }
    }
}

using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WPFStandardControlDemoApp.Common.Behaviors.WeakEventManagers;

namespace WPFStandardControlDemoApp.Common.Behaviors
{
    public static class TextBoxInputMaskBehavior
    {
        // 許可する入力を定義する正規表現 (例: ^[0-9]*$)
        public static readonly DependencyProperty InputMaskProperty =
            DependencyProperty.RegisterAttached("InputMask", typeof(string), typeof(TextBoxInputMaskBehavior), new PropertyMetadata(null, OnInputMaskChanged));

        // 貼り付けを許可するかどうか
        public static readonly DependencyProperty AllowClipboardProperty =
            DependencyProperty.RegisterAttached("AllowClipboard", typeof(bool), typeof(TextBoxInputMaskBehavior), new PropertyMetadata(true));

        public static void SetInputMask(DependencyObject d, string value) => d.SetValue(InputMaskProperty, value);
        public static string GetInputMask(DependencyObject d) => (string)d.GetValue(InputMaskProperty);

        public static void SetAllowClipboard(DependencyObject d, bool value) => d.SetValue(AllowClipboardProperty, value);
        public static bool GetAllowClipboard(DependencyObject d) => (bool)d.GetValue(AllowClipboardProperty);

        private static void OnInputMaskChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is not TextBox textBox) return;

            // 弱参照イベントマネージャーでハンドラを整理
            WeakEventManager<TextBox, TextCompositionEventArgs>.RemoveHandler(textBox, nameof(TextBox.PreviewTextInput), OnPreviewTextInput);
            DataObjectPastingWeakEventManager.RemoveHandler(textBox, OnPasting);

            if (e.NewValue is string mask && !string.IsNullOrEmpty(mask))
            {
                WeakEventManager<TextBox, TextCompositionEventArgs>.AddHandler(textBox, nameof(TextBox.PreviewTextInput), OnPreviewTextInput);
                DataObjectPastingWeakEventManager.AddHandler(textBox, OnPasting);
            }
        }

        // 1. キーボード入力のバリデーション
        private static void OnPreviewTextInput(object? sender, TextCompositionEventArgs e)
        {
            if (sender is not TextBox textBox) return;

            var mask = GetInputMask(textBox);
            if (string.IsNullOrEmpty(mask)) return;

            // 入力後の文字列をシミュレート
            string fullText = GetFullTextAfterInput(textBox, e.Text);

            // 正規表現にマッチしない場合は入力をキャンセル
            if (!Regex.IsMatch(fullText, mask))
            {
                e.Handled = true;
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

        // 2. 貼り付け（クリップボード）のバリデーション
        private static void OnPastingInternal(object sender, DataObjectPastingEventArgs e)
        {
            if (sender is not TextBox textBox) return;

            // 貼り付け自体が禁止されている場合
            if (!GetAllowClipboard(textBox))
            {
                e.CancelCommand();
                return;
            }

            var mask = GetInputMask(textBox);
            if (string.IsNullOrEmpty(mask)) return;

            // 貼り付けようとしているテキストを取得
            if (e.DataObject.GetDataPresent(DataFormats.Text))
            {
                var pasteText = e.DataObject.GetData(DataFormats.Text) as string;
                if (pasteText != null)
                {
                    string fullText = GetFullTextAfterInput(textBox, pasteText);

                    // 貼り付け後の結果が正規表現に合わないならキャンセル
                    if (!Regex.IsMatch(fullText, mask))
                    {
                        e.CancelCommand();
                    }
                }
            }
        }

        // 入力後の「完成予定の文字列」を計算するヘルパー
        private static string GetFullTextAfterInput(TextBox textBox, string input)
        {
            string currentText = textBox.Text;
            int selectionStart = textBox.SelectionStart;
            int selectionLength = textBox.SelectionLength;

            // 現在のテキストから選択範囲を削除し、新しい入力を挿入
            return currentText.Remove(selectionStart, selectionLength).Insert(selectionStart, input);
        }
    }
}

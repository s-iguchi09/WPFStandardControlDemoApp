using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace WPFStandardControlDemoApp.Common.Converters
{
    public class StringIsNullOrEmptyToVisibilityConverter : MarkupConverterBase
    {
        /// <summary>
        /// true の場合、文字列が「空でない」ときに非表示にします。
        /// </summary>
        public bool IsInverse { get; set; } = false;

        /// <summary>
        /// 非表示にする際の振る舞い（Collapsed または Hidden）を指定します。
        /// デフォルトは Collapsed（領域を詰め込む）です。
        /// </summary>
        public Visibility HiddenState { get; set; } = Visibility.Collapsed;

        public override object Convert(object v, Type t, object p, CultureInfo c)
        {
            bool isEmpty = string.IsNullOrEmpty(v as string);

            // IsInverse が true なら判定を反転
            bool shouldHide = IsInverse ? !isEmpty : isEmpty;

            return shouldHide ? HiddenState : Visibility.Visible;
        }

        public override object ConvertBack(object v, Type t, object p, CultureInfo c)
        {
            // ViewからViewModelへの書き戻しは行わないため Binding.DoNothing を返す
            return Binding.DoNothing;
        }
    }
}

using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace WPFStandardControlDemoApp.Common.Converters
{
    public class BoolToVisibilityConverter : MarkupConverterBase
    {
        /// <summary>
        /// Trueの時に非表示にする場合は True を設定します。
        /// </summary>
        public bool IsInverse { get; set; }

        /// <summary>
        /// 非表示の状態を Collapsed (サイズ0) か Hidden (空間維持) かで指定します。
        /// デフォルトは Collapsed です。
        /// </summary>
        public Visibility HiddenState { get; set; } = Visibility.Collapsed;

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool boolValue = value is bool b && b;

            // 反転処理
            if (IsInverse)
            {
                boolValue = !boolValue;
            }

            return boolValue ? Visibility.Visible : HiddenState;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isVisible = value is Visibility v && v == Visibility.Visible;
            return IsInverse ? !isVisible : isVisible;
        }
    }
}

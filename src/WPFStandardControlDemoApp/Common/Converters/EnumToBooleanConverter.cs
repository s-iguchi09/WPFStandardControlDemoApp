using System.Globalization;
using System.Windows.Data;

namespace WPFStandardControlDemoApp.Common.Converters
{
    /// <summary>
    /// Enum値とConverterParameterを比較し、一致すれば true を返します。
    /// RadioButtonのバインディングなどに最適化されています。
    /// </summary>
    public class EnumToBooleanConverter : MarkupConverterBase
    {
        /// <summary>
        /// 判定を反転させます（一致しない場合に true）。
        /// </summary>
        public bool IsInverse { get; set; }

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || parameter == null) return IsInverse;

            string checkValue = parameter.ToString();
            string targetValue = value.ToString();

            // 大文字小文字を区別せずに比較
            bool isMatch = string.Equals(targetValue, checkValue, StringComparison.InvariantCultureIgnoreCase);

            return IsInverse ? !isMatch : isMatch;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // RadioButtonなどが「Checked (true)」になった時だけ、Enum値を戻す
            if (value is bool isChecked && isChecked && parameter != null)
            {
                if (targetType.IsEnum)
                {
                    return Enum.Parse(targetType, parameter.ToString(), true);
                }
            }

            // チェックが外れた時や、型が合わない時はバインディングを更新しない
            return Binding.DoNothing;
        }
    }
}

using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace WPFStandardControlDemoApp.Common.Converters
{
    /// <summary>
    /// Converts between a delimited string and a <see cref="DoubleCollection"/>.
    /// 区切り記号を含む文字列と <see cref="DoubleCollection"/> の間の変換を行います。
    /// </summary>
    /// <remarks>
    /// This converter is primarily used for the <see cref="System.Windows.Controls.Slider.Ticks"/> property.
    /// It supports various delimiters (comma, semicolon, space) and safely handles invalid input during typing.
    /// このコンバーターは主に <see cref="System.Windows.Controls.Slider.Ticks"/> プロパティに使用されます。
    /// カンマ、セミコロン、スペースなどの複数の区切り文字をサポートしており、入力途中の不完全な文字列に対しても安全に処理を行います。
    /// </remarks>
    [ValueConversion(typeof(string), typeof(DoubleCollection))]
    public class StringToDoubleCollectionConverter : MarkupConverterBase
    {
        /// <summary>
        /// Converts a string of numbers into a <see cref="DoubleCollection"/>.
        /// 数値の文字列を <see cref="DoubleCollection"/> に変換します。
        /// </summary>
        /// <param name="value">
        /// The string to convert.
        /// 変換する文字列。
        /// </param>
        /// <param name="targetType">
        /// The type of the binding target property.
        /// バインディングターゲットプロパティの型。
        /// </param>
        /// <param name="parameter">
        /// The converter parameter to use.
        /// 使用するコンバーターパラメーター。
        /// </param>
        /// <param name="culture">
        /// The culture to use in the converter.
        /// コンバーターで使用するカルチャ。
        /// </param>
        /// <returns>
        /// A <see cref="DoubleCollection"/> containing the parsed numbers.
        /// 解析された数値を含む <see cref="DoubleCollection"/>。
        /// </returns>
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string text)
            {
                if (string.IsNullOrWhiteSpace(text))
                {
                    return new DoubleCollection();
                }

                var parts = text.Split(new[] { ',', ';', ' ', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

                var collection = new DoubleCollection();
                foreach (var part in parts)
                {
                    if (double.TryParse(part, NumberStyles.Any, culture, out double result))
                    {
                        collection.Add(result);
                    }
                }
                return collection;
            }
            return new DoubleCollection();
        }

        /// <summary>
        /// Converts a <see cref="DoubleCollection"/> back into a comma-separated string.
        /// <see cref="DoubleCollection"/> をカンマ区切りの文字列に戻します。
        /// </summary>
        /// <param name="value">
        /// The <see cref="DoubleCollection"/> to convert.
        /// 変換する <see cref="DoubleCollection"/>。
        /// </param>
        /// <param name="targetType">
        /// The type to convert to.
        /// 変換先の型。
        /// </param>
        /// <param name="parameter">
        /// The converter parameter to use.
        /// 使用するコンバーターパラメーター。
        /// </param>
        /// <param name="culture">
        /// The culture to use in the converter.
        /// コンバーターで使用するカルチャ。
        /// </param>
        /// <returns>
        /// A comma-separated string of numbers.
        /// カンマ区切りの数値文字列。
        /// </returns>
        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DoubleCollection collection)
            {
                return string.Join(", ", collection.Select(d => d.ToString(culture)));
            }
            return string.Empty;
        }
    }
}

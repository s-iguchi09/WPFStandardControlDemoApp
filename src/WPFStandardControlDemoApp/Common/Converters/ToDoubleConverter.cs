using System.Globalization;
using System.Windows;

namespace WPFStandardControlDemoApp.Common.Converters
{
    public class ToDoubleConverter : MarkupConverterBase
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var text = value?.ToString();

            if (string.IsNullOrWhiteSpace(text))
                return DependencyProperty.UnsetValue;

            if (double.TryParse(text, NumberStyles.Float, culture, out double result))
                return result;

            return DependencyProperty.UnsetValue;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double d)
                return d.ToString(culture);

            return string.Empty;
        }

    }
}

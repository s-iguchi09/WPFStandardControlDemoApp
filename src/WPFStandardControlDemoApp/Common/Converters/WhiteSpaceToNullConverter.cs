using System.Globalization;

namespace WPFStandardControlDemoApp.Common.Converters
{
    public class WhiteSpaceToNullConverter : MarkupConverterBase
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is string str && string.IsNullOrWhiteSpace(str) ? null : value;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}

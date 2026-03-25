using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace WPFStandardControlDemoApp.Common.Converters
{
    public class RectConverter : MarkupMultiConverterBase
    {
        public override object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            double x = values[0] is string sx && double.TryParse(sx, out var dx) ? dx : 0;
            double y = values[1] is string sy && double.TryParse(sy, out var dy) ? dy : 0;
            double w = values[2] is string sw && double.TryParse(sw, out var dw) ? dw : 0;
            double h = values[3] is string sh && double.TryParse(sh, out var dh) ? dh : 0;

            return new Rect(x, y, Math.Max(0, w), Math.Max(0, h));
        }

        public override object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
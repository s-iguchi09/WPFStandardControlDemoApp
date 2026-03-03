using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace WPFStandardControlDemoApp.Common.Converters
{
    /// <summary>
    /// string を Thickness に変換するコンバーター。
    /// 例:
    /// "10"            -> Thickness(10)
    /// "10,20"         -> Thickness(10,20,10,20)
    /// "1,2,3,4"       -> Thickness(1,2,3,4)
    /// "  5  "         -> Thickness(5)
    /// パースできない場合は 0 の Thickness を返す。
    /// </summary>
    public class StringToThicknessConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not string s || string.IsNullOrWhiteSpace(s))
                return new Thickness(0);

            var parts = s
                .Replace(" ", string.Empty)
                .Split(new[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries);

            try
            {
                switch (parts.Length)
                {
                    case 1:
                        {
                            if (double.TryParse(parts[0], NumberStyles.Any, culture, out var uniform))
                                return new Thickness(uniform);
                            break;
                        }
                    case 2:
                        {
                            if (double.TryParse(parts[0], NumberStyles.Any, culture, out var horizontal) &&
                                double.TryParse(parts[1], NumberStyles.Any, culture, out var vertical))
                            {
                                return new Thickness(horizontal, vertical, horizontal, vertical);
                            }
                            break;
                        }
                    case 4:
                        {
                            if (double.TryParse(parts[0], NumberStyles.Any, culture, out var left) &&
                                double.TryParse(parts[1], NumberStyles.Any, culture, out var top) &&
                                double.TryParse(parts[2], NumberStyles.Any, culture, out var right) &&
                                double.TryParse(parts[3], NumberStyles.Any, culture, out var bottom))
                            {
                                return new Thickness(left, top, right, bottom);
                            }
                            break;
                        }
                }
            }
            catch
            {
                return new Thickness(0);
            }

            return new Thickness(0);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not Thickness thickness)
                return string.Empty;

            double left = thickness.Left;
            double top = thickness.Top;
            double right = thickness.Right;
            double bottom = thickness.Bottom;

            if (left == top && top == right && right == bottom)
                return left.ToString(culture);

            if (left == right && top == bottom)
                return $"{left.ToString(culture)},{top.ToString(culture)}";

            return $"{left.ToString(culture)},{top.ToString(culture)},{right.ToString(culture)},{bottom.ToString(culture)}";
        }
    }
}

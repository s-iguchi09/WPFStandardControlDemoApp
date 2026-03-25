using System.Diagnostics;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace WPFStandardControlDemoApp.Common.Converters
{
    public class ValueConverterGroup : List<IValueConverter>, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return this.Aggregate(value, (current, converter) =>
            {
                var result = converter.Convert(current, targetType, parameter, culture);

                // デバッグ用：変換に失敗（DependencyProperty.UnsetValueが返却）されたら通知
                if (result == DependencyProperty.UnsetValue)
                {
                    Debug.WriteLine($"[ConverterGroup] Warning: {converter.GetType().Name} failed to convert {current}");
                }
                return result;
            });
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotSupportedException();
    }
}

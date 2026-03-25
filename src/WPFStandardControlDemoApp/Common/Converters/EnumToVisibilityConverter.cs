using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace WPFStandardControlDemoApp.Common.Converters
{
    public class EnumToVisibilityConverter : MarkupConverterBase
    {
        public bool IsInverse { get; set; }
        public Visibility HiddenState { get; set; } = Visibility.Collapsed;

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || parameter == null) return GetResult(false);

            string targetValue = value.ToString();
            string checkValue = parameter.ToString();
            bool isMatch = string.Equals(targetValue, checkValue, StringComparison.InvariantCultureIgnoreCase);

            return GetResult(isMatch);
        }

        private object GetResult(bool isVisible)
        {
            if (IsInverse) isVisible = !isVisible;
            return isVisible ? Visibility.Visible : HiddenState;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => Binding.DoNothing;
    }
}

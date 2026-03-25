using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace WPFStandardControlDemoApp.Common.Converters
{
    public class NumericToVisibilityConverter : MarkupConverterBase
    {
        public bool IsInverse { get; set; }
        public Visibility HiddenState { get; set; } = Visibility.Collapsed;
        public double ComparisonValue { get; set; } = 0;
        public double Tolerance { get; set; } = 1e-9;

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return GetResult(false);
            try
            {
                double currentVal = System.Convert.ToDouble(value);
                // 0以外なら真（表示）とするC言語風ロジック
                bool isVisible = Math.Abs(currentVal - ComparisonValue) > Tolerance;
                return GetResult(isVisible);
            }
            catch { return GetResult(false); }
        }

        private object GetResult(bool isVisible)
        {
            if (IsInverse) isVisible = !isVisible;
            return isVisible ? Visibility.Visible : HiddenState;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => Binding.DoNothing;
    }
}

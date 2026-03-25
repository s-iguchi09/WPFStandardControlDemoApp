using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace WPFStandardControlDemoApp.Common.Converters
{
    public class NumericRangeToVisibilityConverter : MarkupConverterBase
    {
        public bool IsInverse { get; set; }
        public Visibility HiddenState { get; set; } = Visibility.Collapsed;
        public double Min { get; set; } = double.NegativeInfinity;
        public double Max { get; set; } = double.PositiveInfinity;
        public double Tolerance { get; set; } = 1e-9;

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return GetResult(false);
            try
            {
                double val = System.Convert.ToDouble(value);
                bool isInRange = (val >= Min - Tolerance) && (val <= Max + Tolerance);
                return GetResult(isInRange);
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

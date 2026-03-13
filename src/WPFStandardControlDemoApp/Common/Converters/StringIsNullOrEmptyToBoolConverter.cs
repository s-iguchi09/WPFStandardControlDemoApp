using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace WPFStandardControlDemoApp.Common.Converters
{
    public class StringIsNullOrEmptyToBoolConverter : MarkupConverterBase
    {
        public bool TreatWhiteSpaceAsEmpty { get; set; }
        public bool Inverse { get; set; }
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || value == DependencyProperty.UnsetValue)
            {
                return Inverse ? false : true;
            }

            string? str = value?.ToString();

            bool isEmpty = TreatWhiteSpaceAsEmpty
                ? string.IsNullOrWhiteSpace(str)
                : string.IsNullOrEmpty(str);

            return Inverse ? !isEmpty : isEmpty;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

using System.Globalization;
using System.Windows.Data;

namespace WPFStandardControlDemoApp.Common.Converters
{
    // bool値を反転させる (True -> False)
    public class InverseBooleanConverter : MarkupConverterBase
    {
        public override object Convert(object v, Type t, object p, CultureInfo c)
        {
            return !(v is bool b && b);
        }

        public override object ConvertBack(object v, Type t, object p, CultureInfo c)
        {
            return !(v is bool b && b);
        }
    }
}

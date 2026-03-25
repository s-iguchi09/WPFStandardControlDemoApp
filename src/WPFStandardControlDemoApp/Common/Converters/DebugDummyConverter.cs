using System.Diagnostics;
using System.Globalization;
using System.Windows.Data;

namespace WPFStandardControlDemoApp.Common.Converters
{
    // バインディングの値をデバッグ出力する (ブレークポイントを貼るのに最適)
    public class DebugDummyConverter : MarkupConverterBase
    {
        public override object Convert(object v, Type t, object p, CultureInfo c)
        {
            Debug.WriteLine($"Binding Debug [Convert]: Value={v}, Type={t}, Param={p}");
            return v;
        }
        public override object ConvertBack(object v, Type t, object p, CultureInfo c)
        {
            Debug.WriteLine($"Binding Debug [ConvertBack]: Value={v}, Type={t}");
            return v;
        }
    }
}

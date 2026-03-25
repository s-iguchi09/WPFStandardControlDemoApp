using System.Globalization;
using System.Windows;

namespace WPFStandardControlDemoApp.Common.Converters
{
    public class ValuesToRectConverter : MarkupMultiConverterBase
    {
        public override object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            // 1. 要素数と UnsetValue のチェック
            if (values == null || values.Length < 4 || values.Any(v => v == DependencyProperty.UnsetValue || v == null))
            {
                return Rect.Empty;
            }

            try
            {
                // 2. どんな数値型（int, double, long等）でも安全に double へ変換
                double x = System.Convert.ToDouble(values[0]);
                double y = System.Convert.ToDouble(values[1]);
                double w = System.Convert.ToDouble(values[2]);
                double h = System.Convert.ToDouble(values[3]);

                // 3. Width/Height が負にならないようガード（Rectの制約対策）
                return new Rect(x, y, Math.Max(0, w), Math.Max(0, h));
            }
            catch (Exception)
            {
                // 変換に失敗した場合は空のRectを返す
                return Rect.Empty;
            }
        }
    }
}

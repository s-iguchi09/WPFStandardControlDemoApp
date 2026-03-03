using System.Windows.Media;
using WPFStandardControlDemoApp.Common.Extensions;

namespace WPFStandardControlDemoApp.Common.Helpers
{
    public static class BrushesHelper
    {
        public static IEnumerable<KeyValuePair<string, Brush>> AllBrushes => Brushes.GetAllBrushs().ToList();
    }
}

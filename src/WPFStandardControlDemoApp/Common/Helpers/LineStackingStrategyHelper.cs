using System.Windows;

namespace WPFStandardControlDemoApp.Common.Helpers
{
    public class LineStackingStrategyHelper
    {
        public static IEnumerable<LineStackingStrategy> AllLineStackingStrategies => Enum.GetValues<LineStackingStrategy>();
    }
}

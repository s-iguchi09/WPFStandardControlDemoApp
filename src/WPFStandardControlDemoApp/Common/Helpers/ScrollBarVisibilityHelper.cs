using System.Windows.Controls;

namespace WPFStandardControlDemoApp.Common.Helpers
{
    public class ScrollBarVisibilityHelper
    {
        public static IEnumerable<ScrollBarVisibility> AllScrollBarVisibilities => Enum.GetValues<ScrollBarVisibility>();
    }
}

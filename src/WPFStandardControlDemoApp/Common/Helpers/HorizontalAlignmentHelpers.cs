using System.Windows;

namespace WPFStandardControlDemoApp.Common.Helpers
{
    public class HorizontalAlignmentHelpers
    {
        public static IEnumerable<HorizontalAlignment> AllHorizontalAlignments => Enum.GetValues<HorizontalAlignment>();
    }
}

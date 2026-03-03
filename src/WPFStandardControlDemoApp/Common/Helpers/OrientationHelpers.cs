using System.Windows.Controls;

namespace WPFStandardControlDemoApp.Common.Helpers
{
    public static class OrientationHelpers
    {
        public static IEnumerable<Orientation> AllOrientations => Enum.GetValues<Orientation>().Cast<Orientation>();
    }
}

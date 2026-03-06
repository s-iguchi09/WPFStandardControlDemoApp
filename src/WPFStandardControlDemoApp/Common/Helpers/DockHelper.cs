using System.Windows.Controls;

namespace WPFStandardControlDemoApp.Common.Helpers
{
    public static class DockHelper
    {
        public static IEnumerable<Dock> AllDocks => Enum.GetValues(typeof(Dock)).Cast<Dock>();
    }
}

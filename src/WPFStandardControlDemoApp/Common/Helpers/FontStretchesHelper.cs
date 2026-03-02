using System.Windows;
using WPFStandardControlDemoApp.Common.Extensions;

namespace WPFStandardControlDemoApp.Common.Helpers
{
    public static class FontStretchesHelper
    {
            public static IEnumerable<FontStretch> AllFontStretchs => FontStretches.GetAllFontStretchs();
    }
}

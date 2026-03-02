using System.Windows;
using WPFStandardControlDemoApp.Common.Extensions;

namespace WPFStandardControlDemoApp.Common.Helpers
{
    public static class FontStylesHelpers
    {
        public static IEnumerable<FontStyle> AllFontStyles => FontStyles.GetAllFontStyles();
    }
}

using System.Windows;
using WPFStandardControlDemoApp.Common.Extensions;

namespace WPFStandardControlDemoApp.Common.Helpers
{
    public static class FontWeightsHelper
    {
        public static IEnumerable<FontWeight> AllFontWeigthts => FontWeights.GetAllFontWeights();
    }
}

using System.Windows;

namespace WPFStandardControlDemoApp.Common.Extensions
{
    public static class FontStretchesExtension
    {
        extension(FontStretches)
        {
            public static IEnumerable<FontStretch> GetAllFontStretchs()
            {
                yield return FontStretches.UltraCondensed;
                yield return FontStretches.ExtraCondensed;
                yield return FontStretches.Condensed;
                yield return FontStretches.SemiCondensed;
                yield return FontStretches.Normal;
                yield return FontStretches.Medium;
                yield return FontStretches.SemiExpanded;
                yield return FontStretches.Expanded;
                yield return FontStretches.ExtraExpanded;
                yield return FontStretches.UltraExpanded;
            }
        }
    }
}

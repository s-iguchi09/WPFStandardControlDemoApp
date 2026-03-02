using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace WPFStandardControlDemoApp.Common.Extensions
{
    public static class FontStylesExtensions
    {
        extension(FontStyles)
        {
            public static IEnumerable<FontStyle> GetAllFontStyles()
            {
                yield return FontStyles.Normal;
                yield return FontStyles.Italic;
                yield return FontStyles.Oblique;
            }
        }
    }
}

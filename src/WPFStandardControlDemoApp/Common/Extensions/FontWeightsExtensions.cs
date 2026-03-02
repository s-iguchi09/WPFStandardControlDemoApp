using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media.Media3D;

namespace WPFStandardControlDemoApp.Common.Extensions
{
    public static class FontWeightsExtensions
    {
        extension(FontWeights)
        {
            public static IEnumerable<FontWeight> GetAllFontWeights()
            {
                yield return FontWeights.Thin;
                yield return FontWeights.ExtraLight;
                yield return FontWeights.UltraLight;
                yield return FontWeights.Light;
                yield return FontWeights.Normal;
                yield return FontWeights.Regular;
                yield return FontWeights.Medium;
                yield return FontWeights.DemiBold;
                yield return FontWeights.SemiBold;
                yield return FontWeights.Bold;
                yield return FontWeights.ExtraBold;
                yield return FontWeights.UltraBold;
                yield return FontWeights.Black;
                yield return FontWeights.Heavy;
                yield return FontWeights.ExtraBlack;
                yield return FontWeights.UltraBlack;
            }
        }
    }
}

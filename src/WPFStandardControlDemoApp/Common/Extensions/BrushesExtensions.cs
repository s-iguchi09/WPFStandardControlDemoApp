using System.Reflection;
using System.Windows.Media;

namespace WPFStandardControlDemoApp.Common.Extensions
{
    public static class BrushesExtensions
    {
        extension(Brushes)
        {
            public static Dictionary<string, Brush> GetAllBrushs()
            {
                return typeof(Brushes).GetProperties(BindingFlags.Public | BindingFlags.Static)
                                      .Select(p => new KeyValuePair<string, Brush>(p.Name, (Brush)p.GetValue(null)))
                                      .ToDictionary();

            }
        }
    }
}

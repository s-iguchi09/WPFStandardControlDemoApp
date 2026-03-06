using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace WPFStandardControlDemoApp.Common.Helpers
{
    public class GridResizeDirectionHelper
    {
        public static IEnumerable<GridResizeDirection> AllGridResizeDirection => Enum.GetValues<GridResizeDirection>();
    }
}

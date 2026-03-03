using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace WPFStandardControlDemoApp.Common.Helpers
{
    public class VerticalAlignmentHelpers
    {
        public static IEnumerable<VerticalAlignment> AllVerticalAlignments => Enum.GetValues<VerticalAlignment>();
    }
}

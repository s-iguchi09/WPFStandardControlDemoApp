using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFStandardControlDemoApp.Features.ViewboxUsage
{
    public class ViewboxUsageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged = delegate { };

        public override string ToString()
        {
            return "Viewbox";
        }
    }
}

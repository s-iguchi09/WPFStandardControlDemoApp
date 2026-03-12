using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFStandardControlDemoApp.Features.TabItemUsage
{
    public class TabItemUsageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged = delegate { };

        public override string ToString()
        {
            return "TabItem";
        }
    }
}

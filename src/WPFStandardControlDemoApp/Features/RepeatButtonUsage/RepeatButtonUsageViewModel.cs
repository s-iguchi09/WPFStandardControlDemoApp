using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace WPFStandardControlDemoApp.Features.RepeatButtonUsage
{
    public class RepeatButtonUsageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public override string ToString()
        {
            return "RepeatButton";
        }
    }
}

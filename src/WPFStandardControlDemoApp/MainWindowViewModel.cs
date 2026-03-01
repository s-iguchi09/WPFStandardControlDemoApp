using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace WPFStandardControlDemoApp
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged = delegate { };
    }
}

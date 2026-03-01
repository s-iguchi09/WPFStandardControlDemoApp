using System.ComponentModel;

namespace WPFStandardControlDemoApp.Features.ToggleButtonUsage
{
    public class ToggleButtonUsageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged = delegate { };

        public override string ToString()
        {
            return "ToggleButton";
        }
    }
}

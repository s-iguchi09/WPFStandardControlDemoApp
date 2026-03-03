using System.ComponentModel;

namespace WPFStandardControlDemoApp.Features.StackPanelUsage
{
    public class StackPanelUsageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged = delegate { };

        public override string ToString()
        {
            return "StackPanel";
        }
    }
}

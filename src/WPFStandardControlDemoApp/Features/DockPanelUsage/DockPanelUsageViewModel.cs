using System.ComponentModel;

namespace WPFStandardControlDemoApp.Features.DockPanelUsage
{
    public class DockPanelUsageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged = delegate { };

        public override string ToString()
        {
            return "DockPanel";
        }
    }
}

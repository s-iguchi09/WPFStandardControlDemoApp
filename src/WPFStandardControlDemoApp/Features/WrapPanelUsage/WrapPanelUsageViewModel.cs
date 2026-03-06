using System.ComponentModel;

namespace WPFStandardControlDemoApp.Features.WrapPanelUsage
{
    public class WrapPanelUsageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged = delegate { };

        public override string ToString()
        {
            return "WrapPanel";
        }
    }
}

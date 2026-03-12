using System.ComponentModel;

namespace WPFStandardControlDemoApp.Features.ExpanderUsage
{
    public class ExpanderUsageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged=delegate { };

        public override string ToString()
        {
            return "Expander";
        }
    }
}

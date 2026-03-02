using System.ComponentModel;

namespace WPFStandardControlDemoApp.Features.LabelUsage
{
    public class LabelUsageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged = delegate { };

        public override string ToString()
        {
            return "Label";
        }
    }
}

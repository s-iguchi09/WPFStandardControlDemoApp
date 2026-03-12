using System.ComponentModel;

namespace WPFStandardControlDemoApp.Features.GroupBoxUsage
{
    public class GroupBoxUsageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged = delegate { };

        public override string ToString()
        {
            return "GroupBox";
        }
    }
}

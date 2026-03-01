using System.ComponentModel;

namespace WPFStandardControlDemoApp.Features.CheckBoxUsage
{
    public class CheckBoxUsageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public override string ToString()
        {
            return "CheckBox";
        }
    }
}

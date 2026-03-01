using System.ComponentModel;

namespace WPFStandardControlDemoApp.Features.RadioButtonUsage
{
    public class RadioButtonUsageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public override string ToString()
        {
            return "RadioButton";
        }
    }
}

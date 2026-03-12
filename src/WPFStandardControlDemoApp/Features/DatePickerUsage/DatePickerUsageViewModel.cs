using System.ComponentModel;

namespace WPFStandardControlDemoApp.Features.DatePickerUsage
{
    public class DatePickerUsageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged = delegate { };

        public override string ToString()
        {
            return "DatePicker";
        }
    }
}

using System.ComponentModel;

namespace WPFStandardControlDemoApp.Features.PasswordBoxUsage
{
    public class PasswordBoxUsageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged = delegate { };

        public override string ToString()
        {
            return "PasswordBox";
        }
    }
}

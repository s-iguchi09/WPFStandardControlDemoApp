using System.ComponentModel;

namespace WPFStandardControlDemoApp.Features.TextBoxUsage
{
    public class TextBoxUsageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged = delegate { };

        public override string ToString()
        {
            return "TextBox";
        }
    }
}

using System.ComponentModel;

namespace WPFStandardControlDemoApp.Features.TextBlockUsage
{
    public class TextBlockUsageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public override string ToString()
        {
            return "TextBlock";
        }
    }
}

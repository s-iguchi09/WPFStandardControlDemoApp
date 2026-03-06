using System.ComponentModel;

namespace WPFStandardControlDemoApp.Features.ScrollViewerUsage
{
    public class ScrollViewerUsageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged = delegate { };

        public override string ToString()
        {
            return "ScrollViewer";
        }
    }
}

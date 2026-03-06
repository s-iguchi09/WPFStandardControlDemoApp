using System.ComponentModel;

namespace WPFStandardControlDemoApp.Features.GridSplitterUsage
{
    public class GridSplitterUsageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged = delegate { };

        public override string ToString()
        {
            return "GridSplitter";
        }
    }
}

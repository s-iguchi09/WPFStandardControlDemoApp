using System.ComponentModel;

namespace WPFStandardControlDemoApp.Features.GridUsage
{
    public class GridUsageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public override string ToString()
        {
            return "Grid";
        }
    }
}

using System.ComponentModel;

namespace WPFStandardControlDemoApp.Features.UniformGridUsage
{
    public class UniformGridUsageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged = delegate { };

        public override string ToString()
        {
            return "UniformGrid";
        }
    }
}

using System.ComponentModel;

namespace WPFStandardControlDemoApp.Features.SliderUsage
{
    public class SliderUsageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged = delegate { };

        public override string ToString()
        {
            return "Slider";
        }
    }
}

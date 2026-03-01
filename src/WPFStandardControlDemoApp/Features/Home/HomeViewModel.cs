using System.ComponentModel;

namespace WPFStandardControlDemoApp.Features.Home
{
    public class HomeViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged = delegate { };

        public override string ToString()
        {
            return "Home";
        }
    }
}

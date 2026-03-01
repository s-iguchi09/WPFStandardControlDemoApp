using System.ComponentModel;

namespace WPFStandardControlDemoApp.Features.Home
{
    public class HomeViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public override string ToString()
        {
            return "Home";
        }
    }
}

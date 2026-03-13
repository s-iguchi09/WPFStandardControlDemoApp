using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using WPFStandardControlDemoApp.Core.Commands;

namespace WPFStandardControlDemoApp.Features.ButtonUsage
{
    public class ButtonUsageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged = delegate { };

        public ICommand ClickCommand { get; } = new RelayCommand(_ => MessageBox.Show("Click!"), null);
        public ICommand ClickWithParameterCommand { get; } = new RelayCommand(param => MessageBox.Show(param?.ToString()), null);
        public ButtonUsageViewModel()
        {

        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using WPFStandardControlDemoApp.Core.Commands;

namespace WPFStandardControlDemoApp.Features.RepeatButtonUsage
{
    public class RepeatButtonUsageViewModel : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged Implementation
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        public ICommand ClickCommand { get; }
        private string _clickDateTimeText = string.Empty;

        public string ClickDateTimeText
        {
            get => _clickDateTimeText;
            set
            {
                if (_clickDateTimeText != value)
                {
                    _clickDateTimeText = value;
                    OnPropertyChanged();
                }
            }
        }
        public RepeatButtonUsageViewModel()
        {
            ClickCommand = new RelayCommand(_ => ClickDateTimeText = DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss.fff"));
        }

    }
}

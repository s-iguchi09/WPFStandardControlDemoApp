using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WPFStandardControlDemoApp.Features.DataGridUsage
{
    public class DataGridUsageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged = delegate { };

        protected void OnPropertyChanged([CallerMemberName] string name = null!) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        public ObservableCollection<SampleItem> Items { get; } = new ObservableCollection<SampleItem>();

        public DataGridUsageViewModel()
        {
            Items.Add(new SampleItem { Name = "Item 1", Value = "Value A" });
            Items.Add(new SampleItem { Name = "Item 2", Value = "Value B" });
            // Error.
            Items.Add(new SampleItem { Name = "", Value = "Error Row" });
        }
    }

    public class SampleItem : IDataErrorInfo
    {
        public string Name { get; set; }
        public string Value { get; set; }

        // IDataErrorInfo
        public string Error => (string.IsNullOrEmpty(Name)) ? "Name cannot be empty." : null!;
        public string this[string columnName] => null!;
    }
}

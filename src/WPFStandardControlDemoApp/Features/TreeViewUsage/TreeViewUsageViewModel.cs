using System.Collections.ObjectModel;
using System.ComponentModel;

namespace WPFStandardControlDemoApp.Features.TreeViewUsage
{
    public class TreeViewUsageViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<DeviceItem> DeviceTree { get; set; }

        public TreeViewUsageViewModel()
        {
            DeviceTree = new ObservableCollection<DeviceItem>();

            // --- Desktop 階層 ---
            var desktop = new DeviceItem("Desktop");
            desktop.Children.Add(new DeviceItem("Workstation PC"));
            desktop.Children.Add(new DeviceItem("Gaming PC"));

            // --- Mobile 階層 ---
            var mobile = new DeviceItem("Mobile");
            mobile.Children.Add(new DeviceItem("Laptop"));
            mobile.Children.Add(new DeviceItem("Tablet"));

            // ルートに追加
            DeviceTree.Add(desktop);
            DeviceTree.Add(mobile);
        }

        public event PropertyChangedEventHandler? PropertyChanged = delegate { };

        public class DeviceItem
        {
            // 表示するテキスト (Desktop, Laptop など)
            public string Name { get; set; }

            // 子要素のリスト
            public ObservableCollection<DeviceItem> Children { get; set; } = new ObservableCollection<DeviceItem>();

            // 便宜上のコンストラクタ
            public DeviceItem(string name) => Name = name;
        }
    }
}

using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Data;
using WPFStandardControlDemoApp.Features.About;
using WPFStandardControlDemoApp.Features.ButtonUsage;
using WPFStandardControlDemoApp.Features.CanvasUsage;
using WPFStandardControlDemoApp.Features.CheckBoxUsage;
using WPFStandardControlDemoApp.Features.ComboBoxUsage;
using WPFStandardControlDemoApp.Features.DataGridUsage;
using WPFStandardControlDemoApp.Features.DatePickerUsage;
using WPFStandardControlDemoApp.Features.DockPanelUsage;
using WPFStandardControlDemoApp.Features.ExpanderUsage;
using WPFStandardControlDemoApp.Features.GridSplitterUsage;
using WPFStandardControlDemoApp.Features.GridUsage;
using WPFStandardControlDemoApp.Features.GroupBoxUsage;
using WPFStandardControlDemoApp.Features.ImageUsage;
using WPFStandardControlDemoApp.Features.InkCanvasUsage;
using WPFStandardControlDemoApp.Features.LabelUsage;
using WPFStandardControlDemoApp.Features.ListBoxUsage;
using WPFStandardControlDemoApp.Features.ListViewUsage;
using WPFStandardControlDemoApp.Features.MenuUsage;
using WPFStandardControlDemoApp.Features.PasswordBoxUsage;
using WPFStandardControlDemoApp.Features.PopupUsage;
using WPFStandardControlDemoApp.Features.ProgressBarUsage;
using WPFStandardControlDemoApp.Features.RadioButtonUsage;
using WPFStandardControlDemoApp.Features.RepeatButtonUsage;
using WPFStandardControlDemoApp.Features.ScrollViewerUsage;
using WPFStandardControlDemoApp.Features.SliderUsage;
using WPFStandardControlDemoApp.Features.StackPanelUsage;
using WPFStandardControlDemoApp.Features.TabControlUsage;
using WPFStandardControlDemoApp.Features.TabItemUsage;
using WPFStandardControlDemoApp.Features.TextBlockUsage;
using WPFStandardControlDemoApp.Features.TextBoxUsage;
using WPFStandardControlDemoApp.Features.ToggleButtonUsage;
using WPFStandardControlDemoApp.Features.ToolBarUsage;
using WPFStandardControlDemoApp.Features.ToolTipUsage;
using WPFStandardControlDemoApp.Features.TreeViewUsage;
using WPFStandardControlDemoApp.Features.UniformGridUsage;
using WPFStandardControlDemoApp.Features.ViewboxUsage;
using WPFStandardControlDemoApp.Features.WrapPanelUsage;

namespace WPFStandardControlDemoApp
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {


        public ObservableCollection<ContentItem> Contents { get; }
        public ICollectionView ContentsView { get; }

        private ContentItem _selectedContent;
        public ContentItem SelectedContent
        {
            get => _selectedContent;
            set
            {
                if (_selectedContent != value)
                {
                    _selectedContent = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set
            {
                if (_searchText != value)
                {
                    _searchText = value;
                    OnPropertyChanged();
                    // 入力があるたびにフィルターをリフレッシュ
                    ContentsView.Refresh();
                }
            }
        }

        public MainWindowViewModel()
        {
            Contents = new ObservableCollection<ContentItem>
            {
                // --- General ---
                //new ContentItem("Home", "General", ()=> new HomeViewModel() ),

                // --- Inputs ---
                new ContentItem("TextBox", "Inputs", ()=> new TextBoxUsageViewModel() ),
                new ContentItem("PasswordBox", "Inputs", ()=> new PasswordBoxUsageViewModel() ),
                new ContentItem("Button", "Inputs", ()=> new ButtonUsageViewModel() ),
                new ContentItem("RepeatButton", "Inputs", ()=> new RepeatButtonUsageViewModel() ),
                new ContentItem("ToggleButton", "Inputs", ()=> new ToggleButtonUsageViewModel() ),
                new ContentItem("CheckBox", "Inputs", ()=> new CheckBoxUsageViewModel() ),
                new ContentItem("RadioButton", "Inputs", ()=> new RadioButtonUsageViewModel() ),
                new ContentItem("ComboBox", "Inputs", ()=> new ComboBoxUsageViewModel() ),
                new ContentItem("DatePicker", "Inputs", ()=> new DatePickerUsageViewModel() ),
                new ContentItem("Slider", "Inputs", ()=> new SliderUsageViewModel() ),

                // --- Layout ---
                new ContentItem("Grid", "Layout", ()=>new GridUsageViewModel() ),
                new ContentItem("StackPanel", "Layout", ()=>new StackPanelUsageViewModel() ),
                new ContentItem("WrapPanel", "Layout", ()=>new WrapPanelUsageViewModel() ),
                new ContentItem("DockPanel", "Layout", ()=>new DockPanelUsageViewModel() ),
                new ContentItem("UniformGrid", "Layout", ()=>new UniformGridUsageViewModel() ),
                new ContentItem("Viewbox", "Layout", ()=>new ViewboxUsageViewModel() ),
                new ContentItem("ScrollViewer", "Layout", ()=>new ScrollViewerUsageViewModel() ),

                // --- Display ---
                new ContentItem("TextBlock", "Display", ()=>new TextBlockUsageViewModel() ),
                new ContentItem("Label", "Display", ()=>new LabelUsageViewModel() ),
                new ContentItem("ProgressBar", "Display", ()=>new ProgressBarUsageViewModel() ),
                new ContentItem("GroupBox", "Display", ()=>new GroupBoxUsageViewModel() ),
                new ContentItem("Expander", "Display", ()=>new ExpanderUsageViewModel() ),

                // --- List ---
                new ContentItem("ListBox", "List", ()=> new ListBoxUsageViewModel() ),
                new ContentItem("ListView", "List", ()=> new ListViewUsageViewModel() ),
                new ContentItem("TreeView", "List", ()=> new TreeViewUsageViewModel() ),
                new ContentItem("DataGrid", "List", ()=> new DataGridUsageViewModel() ),

                // --- Graphics ---
                new ContentItem("Image", "Graphics", ()=> new ImageUsageViewModel() ),
                new ContentItem("Canvas", "Graphics", ()=> new CanvasUsageViewModel() ),
                new ContentItem("InkCanvas", "Graphics", ()=> new InkCanvasUsageViewModel() ),

                // --- Selectors ---
                new ContentItem("TabControl", "Selectors", ()=>new TabControlUsageViewModel() ),
                new ContentItem("TabItem", "Selectors", ()=>new TabItemUsageViewModel() ),

                // --- Menu ---
                new ContentItem("Menu", "Menu",()=> new MenuUsageViewModel() ),
                new ContentItem("ToolBar", "Menu",()=> new ToolBarUsageViewModel() ),

                // --- Overlays ---
                new ContentItem("ToolTip", "OverLays", ()=> new ToolTipUsageViewModel() ),
                new ContentItem("Popup", "OverLays", ()=> new PopupUsageViewModel() ),

                // --- Resizer ---
                new ContentItem("GridSplitter", "Resizer", ()=>new GridSplitterUsageViewModel() ),

                new ContentItem("About", "General", ()=> new AboutViewModel() ),
            };

            // Setup CollectionView with Grouping
            ContentsView = CollectionViewSource.GetDefaultView(Contents);
            ContentsView.GroupDescriptions.Add(new PropertyGroupDescription(nameof(ContentItem.Category)));

            // フィルターロジックの定義
            ContentsView.Filter = (item) =>
            {
                if (string.IsNullOrWhiteSpace(SearchText))
                    return true; // 検索文字がなければ全て表示

                var content = item as ContentItem;
                if (content == null) return false;

                // 名前（Name）またはカテゴリ（Category）に部分一致するかチェック
                // 大文字小文字を区別しないように StringComparison.OrdinalIgnoreCase を使用
                return content.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase);
                //|| content.Category.Contains(SearchText, StringComparison.OrdinalIgnoreCase);
            };

            // Set Initial Selection
            SelectedContent = Contents[0];
        }

        #region INotifyPropertyChanged Implementation
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}

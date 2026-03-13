namespace WPFStandardControlDemoApp
{
    public class ContentItem
    {
        private object _viewModel;
        private readonly Func<object> _viewModelFactory;

        public string Name { get; set; }
        public string Category { get; set; }

        /// <summary>
        /// ViewModelが必要になったタイミングで生成・取得するプロパティ
        /// </summary>
        public object ViewModel
        {
            get
            {
                // 初回アクセス時のみ、ファクトリを実行してインスタンスを作成
                if (_viewModel == null && _viewModelFactory != null)
                {
                    _viewModel = _viewModelFactory.Invoke();
                }
                return _viewModel;
            }
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="name">表示名</param>
        /// <param name="category">カテゴリ</param>
        /// <param name="viewModelFactory">ViewModelを生成する関数 (例: () => new HomeViewModel())</param>
        public ContentItem(string name, string category, Func<object> viewModelFactory)
        {
            Name = name;
            Category = category;
            _viewModelFactory = viewModelFactory;
        }
    }
}

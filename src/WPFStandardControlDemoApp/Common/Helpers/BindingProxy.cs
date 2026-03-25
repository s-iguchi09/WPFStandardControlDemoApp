using System.Windows;

namespace WPFStandardControlDemoApp.Common.Helpers
{
    /// <summary>
    /// XAMLの名前スコープやビジュアルツリーの壁を越えてバインディングを仲介するプロキシクラス。
    /// </summary>
    public class BindingProxy : Freezable
    {
        #region Overrides of Freezable

        protected override Freezable CreateInstanceCore()
        {
            // WPFの内部メカニズム（アニメーションやリソース評価）で
            // 新しいインスタンスが必要になった際に呼ばれます。
            return new BindingProxy();
        }

        #endregion

        /// <summary>
        /// 仲介するデータ。
        /// </summary>
        public object Data
        {
            get => GetValue(DataProperty);
            set => SetValue(DataProperty, value);
        }

        public static readonly DependencyProperty DataProperty =
            DependencyProperty.Register(
                nameof(Data),
                typeof(object),
                typeof(BindingProxy),
                new FrameworkPropertyMetadata(
                    null,
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, // 双方向バインディングをデフォルトに
                    OnDataChanged));

        private static void OnDataChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            // 必要に応じて、データが切り替わった際のロギングや
            // 特殊な破棄処理をここに記述できます。
        }
    }
}

using System.ComponentModel;
using System.Windows;
using System.Windows.Threading;

namespace WPFStandardControlDemoApp.Common.Behaviors
{
    /// <summary>
    /// 通知を受け取ってWindowを閉じます（キャンセル時のリセット・メモリリーク対策済）。
    /// </summary>
    public static class WindowCloseBehavior
    {
        public static readonly DependencyProperty CloseTriggerProperty =
            DependencyProperty.RegisterAttached(
                "CloseTrigger",
                typeof(object),
                typeof(WindowCloseBehavior),
                new PropertyMetadata(null, OnCloseTriggerChanged));

        public static void SetCloseTrigger(DependencyObject d, object value) => d.SetValue(CloseTriggerProperty, value);
        public static object GetCloseTrigger(DependencyObject d) => d.GetValue(CloseTriggerProperty);

        private static void OnCloseTriggerChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue == null) return;

            var window = d as Window ?? Window.GetWindow(d);
            if (window == null) return;

            window.Dispatcher.BeginInvoke(new Action(() =>
            {
                if (!window.IsLoaded) return;

                CancelEventHandler closingHandler = null!;
                RoutedEventHandler unloadedHandler = null!;

                // イベント解除の共通化
                Action cleanup = () =>
                {
                    window.Closing -= closingHandler;
                    window.Unloaded -= unloadedHandler;
                };

                closingHandler = (s, ev) =>
                {
                    cleanup();
                    if (ev.Cancel)
                    {
                        // キャンセルされたら、再度通知を受け取れるよう値をnullリセット
                        d.SetCurrentValue(CloseTriggerProperty, null);
                    }
                };

                unloadedHandler = (s, ev) => cleanup();

                window.Closing += closingHandler;
                window.Unloaded += unloadedHandler;

                try
                {
                    window.Close();
                }
                catch
                {
                    cleanup(); d.SetCurrentValue(CloseTriggerProperty, null);
                }

            }), DispatcherPriority.ContextIdle);
        }
    }
}

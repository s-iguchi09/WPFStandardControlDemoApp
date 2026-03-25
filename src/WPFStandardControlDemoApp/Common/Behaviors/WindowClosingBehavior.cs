using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace WPFStandardControlDemoApp.Common.Behaviors
{
    public static class WindowClosingBehavior
    {
        public static readonly DependencyProperty ClosingCommandProperty =
            DependencyProperty.RegisterAttached(
                "ClosingCommand",
                typeof(ICommand),
                typeof(WindowClosingBehavior),
                new PropertyMetadata(null, OnClosingCommandChanged));

        public static void SetClosingCommand(DependencyObject d, ICommand value) => d.SetValue(ClosingCommandProperty, value);
        public static ICommand GetClosingCommand(DependencyObject d) => (ICommand)d.GetValue(ClosingCommandProperty);

        private static void OnClosingCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(d is Window window)) return;

            WeakEventManager<Window, CancelEventArgs>.RemoveHandler(window, nameof(Window.Closing), Window_Closing);

            if (e.NewValue is ICommand)
            {
                WeakEventManager<Window, CancelEventArgs>.AddHandler(window, nameof(Window.Closing), Window_Closing);
            }
        }

        private static void Window_Closing(object? sender, CancelEventArgs e)
        {
            if (!(sender is Window window)) return;

            var command = GetClosingCommand(window);
            if (command != null && command.CanExecute(e))
            {
                command.Execute(e);
            }
        }
    }
}

using System.Windows;
using System.Windows.Controls;

namespace WPFStandardControlDemoApp.Common.Behaviors
{
    public static class PasswordBindingBehavior
    {
        public static readonly DependencyProperty PasswordProperty =
            DependencyProperty.RegisterAttached(
                "Password",
                typeof(string),
                typeof(PasswordBindingBehavior),
                new FrameworkPropertyMetadata(
                    string.Empty,
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                    OnPasswordPropertyChanged));

        public static void SetPassword(DependencyObject d, string value) => d.SetValue(PasswordProperty, value);
        public static string GetPassword(DependencyObject d) => (string)d.GetValue(PasswordProperty);

        private static readonly DependencyProperty IsUpdatingProperty =
            DependencyProperty.RegisterAttached("IsUpdating", typeof(bool), typeof(PasswordBindingBehavior), new PropertyMetadata(false));

        private static void OnPasswordPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is not PasswordBox pb) return;

            WeakEventManager<PasswordBox, RoutedEventArgs>.RemoveHandler(pb, nameof(PasswordBox.PasswordChanged), OnPasswordChanged);
            WeakEventManager<PasswordBox, RoutedEventArgs>.AddHandler(pb, nameof(PasswordBox.PasswordChanged), OnPasswordChanged);

            if (!(bool)pb.GetValue(IsUpdatingProperty))
            {
                pb.Password = (string)e.NewValue ?? string.Empty;
            }
        }

        private static void OnPasswordChanged(object? sender, RoutedEventArgs e)
        {
            if (sender is not PasswordBox pb) return;

            pb.SetValue(IsUpdatingProperty, true);
            try
            {
                SetPassword(pb, pb.Password);
            }
            finally
            {
                pb.SetValue(IsUpdatingProperty, false);
            }
        }
    }
}

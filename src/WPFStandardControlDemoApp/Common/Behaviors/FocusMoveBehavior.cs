using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace WPFStandardControlDemoApp.Common.Behaviors
{
    public static class FocusMoveBehavior
    {
        public static readonly DependencyProperty IsEnabledProperty =
            DependencyProperty.RegisterAttached("IsEnabled", typeof(bool), typeof(FocusMoveBehavior), new PropertyMetadata(false, OnIsEnabledChanged));

        public static readonly DependencyProperty AcceptsReturnModifierProperty =
            DependencyProperty.RegisterAttached("AcceptsReturnModifier", typeof(ModifierKeys), typeof(FocusMoveBehavior), new PropertyMetadata(ModifierKeys.Control));

        public static void SetIsEnabled(DependencyObject d, bool value) => d.SetValue(IsEnabledProperty, value);
        public static bool GetIsEnabled(DependencyObject d) => (bool)d.GetValue(IsEnabledProperty);

        public static void SetAcceptsReturnModifier(DependencyObject d, ModifierKeys value) => d.SetValue(AcceptsReturnModifierProperty, value);
        public static ModifierKeys GetAcceptsReturnModifier(DependencyObject d) => (ModifierKeys)d.GetValue(AcceptsReturnModifierProperty);

        private static void OnIsEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is not UIElement element) return;

            WeakEventManager<UIElement, KeyEventArgs>.RemoveHandler(element, nameof(UIElement.PreviewKeyDown), OnPreviewKeyDown);

            if ((bool)e.NewValue)
            {
                WeakEventManager<UIElement, KeyEventArgs>.AddHandler(element, nameof(UIElement.PreviewKeyDown), OnPreviewKeyDown);
            }
        }

        private static void OnPreviewKeyDown(object? sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter) return;
            if (e.OriginalSource is not UIElement element) return;

            if (element is ButtonBase) return;

            if (element is TextBox { AcceptsReturn: true })
            {
                var modifier = GetAcceptsReturnModifier(element);
                if (Keyboard.Modifiers != modifier) return;
            }
            else
            {
                if (Keyboard.Modifiers != ModifierKeys.None) return;
            }

            var request = new TraversalRequest(FocusNavigationDirection.Next);
            if (element.MoveFocus(request))
            {
                e.Handled = true;
            }
        }
    }
}

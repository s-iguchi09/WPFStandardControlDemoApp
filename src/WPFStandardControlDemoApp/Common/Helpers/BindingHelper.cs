using System.Windows;
using System.Windows.Data;

namespace WPFStandardControlDemoApp.Common.Helpers
{
    /// <summary>
    /// Provides utility methods to ensure proper binding states between dependency objects.
    /// 依存関係オブジェクト間の適切なバインディング状態を保証するためのユーティリティメソッドを提供します。
    /// </summary>
    internal static class BindingHelper
    {
        /// <summary>
        /// Ensures that a OneWay binding is established from the source property to the target property, 
        /// but only if the source has a local value set.
        /// ソースプロパティに値が設定されている場合、ターゲットプロパティへの OneWay バインディングが確立されている状態を保証します。
        /// </summary>
        /// <param name="source">The source <see cref="DependencyObject"/> (e.g., a DatePicker).</param>
        /// <param name="target">The target <see cref="FrameworkElement"/> (e.g., an internal PART_Button).</param>
        /// <param name="sourceProp">The source <see cref="DependencyProperty"/> (usually an attached property).</param>
        /// <param name="targetProp">The target <see cref="DependencyProperty"/> to be bound.</param>
        public static void EnsureBinding(DependencyObject source, FrameworkElement target, DependencyProperty sourceProp, DependencyProperty targetProp)
        {
            var value = source.GetValue(sourceProp);

            if (value == DependencyProperty.UnsetValue) return;

            var existingBinding = BindingOperations.GetBindingExpression(target, targetProp);
            if (existingBinding?.ParentBinding.Source == source &&
                existingBinding.ParentBinding.Path.Path.Contains(sourceProp.Name)) return;

            BindingOperations.SetBinding(target, targetProp, new Binding
            {
                Source = source,
                Path = new PropertyPath(sourceProp),
                Mode = BindingMode.OneWay
            });
        }
    }
}

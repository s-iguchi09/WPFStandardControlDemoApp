using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace WPFStandardControlDemoApp.Common
{
    /// <summary>
    /// Base class for converters that support both <see cref="MarkupExtension"/> and <see cref="IValueConverter"/>.
    /// <see cref="MarkupExtension"/> と <see cref="IValueConverter"/> の両方をサポートするコンバーターの基底クラスです。
    /// </summary>
    public abstract class MarkupConverterBase : MarkupExtension, IValueConverter
    {
        /// <summary>
        /// Provides the value for the target property. 
        /// Returns this instance for <see cref="ResourceDictionary"/>, or a clone for inline usage.
        /// ターゲットプロパティの値を返します。
        /// <see cref="ResourceDictionary"/> 定義時は自身を、インライン使用時はクローンを返します。
        /// </summary>
        /// <param name="serviceProvider">
        /// A service provider helper that can provide services for the markup extension.
        /// マークアップ拡張機能のサービスを提供できるサービスプロバイダー。
        /// </param>
        /// <returns>
        /// The object value to set on the property where the extension is applied.
        /// 拡張機能が適用されたプロパティに設定するオブジェクト値。
        /// </returns>
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            var target = serviceProvider.GetService(typeof(IProvideValueTarget)) as IProvideValueTarget;

            if (target?.TargetObject is ResourceDictionary || target?.TargetObject == null)
            {
                return this;
            }

            return this.MemberwiseClone();
        }

        /// <summary>
        /// Converts the source value to the target type.
        /// ソース値をターゲット型に変換します。
        /// </summary>
        /// <param name="value">
        /// The value produced by the binding source.
        /// バインディングソースによって生成された値。
        /// </param>
        /// <param name="targetType">
        /// The type of the binding target property.
        /// バインディングターゲットプロパティの型。
        /// </param>
        /// <param name="parameter">
        /// The converter parameter to use.
        /// 使用するコンバーターパラメーター。
        /// </param>
        /// <param name="culture">
        /// The culture to use in the converter.
        /// コンバーターで使用するカルチャ。
        /// </param>
        /// <returns>
        /// A converted value.
        /// 変換された値。
        /// </returns>
        public abstract object Convert(object value, Type targetType, object parameter, CultureInfo culture);

        /// <summary>
        /// Converts the target value back to the source type.
        /// This method returns <see cref="DependencyProperty.UnsetValue"/> by default.
        /// ターゲット値をソース型に逆変換します。
        /// このメソッドはデフォルトで <see cref="DependencyProperty.UnsetValue"/> を返します。
        /// </summary>
        /// <param name="value">
        /// The value that is produced by the binding target.
        /// バインディングターゲットによって生成された値。
        /// </param>
        /// <param name="targetType">
        /// The type to convert to.
        /// 変換先の型。
        /// </param>
        /// <param name="parameter">
        /// The converter parameter to use.
        /// 使用するコンバーターパラメーター。
        /// </param>
        /// <param name="culture">
        /// The culture to use in the converter.
        /// コンバーターで使用するカルチャ。
        /// </param>
        /// <returns>
        /// Always returns <see cref="DependencyProperty.UnsetValue"/>.
        /// 常に <see cref="DependencyProperty.UnsetValue"/> を返します。
        /// </returns>
        public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => DependencyProperty.UnsetValue;
    }
}

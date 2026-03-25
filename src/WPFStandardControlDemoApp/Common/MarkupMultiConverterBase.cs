using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace WPFStandardControlDemoApp.Common
{
    /// <summary>
    /// Base class for multi-converters that support both <see cref="MarkupExtension"/> and <see cref="IMultiValueConverter"/>.
    /// <see cref="MarkupExtension"/> と <see cref="IMultiValueConverter"/> の両方をサポートするコンバーターの基底クラスです。
    /// </summary>
    public abstract class MarkupMultiConverterBase : MarkupExtension, IMultiValueConverter
    {
        /// <summary>
        /// Provides the value for the target property. 
        /// Returns this instance for <see cref="ResourceDictionary"/>, or a clone for inline usage.
        /// </summary>
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            var target = serviceProvider.GetService(typeof(IProvideValueTarget)) as IProvideValueTarget;

            if (target?.TargetObject is ResourceDictionary || target?.TargetObject == null)
            {
                return this;
            }

            return MemberwiseClone();
        }

        /// <summary>
        /// Converts source values to a target value.
        /// ソース値の配列をターゲット値に変換します。
        /// </summary>
        /// <param name="values">バインディングソースからの値の配列。</param>
        /// <param name="targetType">ターゲットプロパティの型。</param>
        /// <param name="parameter">使用するパラメーター。</param>
        /// <param name="culture">カルチャ情報。</param>
        /// <returns>変換された値。</returns>
        public abstract object Convert(object[] values, Type targetType, object parameter, CultureInfo culture);

        /// <summary>
        /// Converts the target value back to the source values.
        /// ターゲット値をソース値の配列に逆変換します。
        /// </summary>
        /// <param name="value">ターゲットからの値。</param>
        /// <param name="targetTypes">変換先の型の配列。</param>
        /// <param name="parameter">使用するパラメーター。</param>
        /// <param name="culture">カルチャ情報。</param>
        /// <returns>
        /// デフォルトでは、全てのソースに対して変更なし（DoNothing）を示す配列を返します。
        /// </returns>
        public virtual object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}

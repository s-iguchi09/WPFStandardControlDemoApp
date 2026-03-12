using System.ComponentModel;
using System.Globalization;
using System.Windows.Data;

namespace WPFStandardControlDemoApp.Common.Converters
{
    /// <summary>
    /// Converts an Enum value to its [Description] attribute text.
    /// <para>Enum の値を [Description] 属性に設定された文字列に変換します。</para>
    /// </summary>
    public class EnumDescriptionConverter : IValueConverter
    {
        /// <summary>
        /// Gets the description of the Enum. If [Description] is missing, returns the Enum name.
        /// <para>Enum の説明文を取得します。[Description] がない場合は Enum の名前を返します。</para>
        /// </summary>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Enum enumValue)
            {
                // Get the field information for the Enum value
                var field = enumValue.GetType().GetField(enumValue.ToString());

                if (field != null)
                {
                    // Try to get the [Description] attribute
                    var attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;

                    // Return the description if it exists, otherwise return the enum name (ToString)
                    return attribute?.Description ?? enumValue.ToString();
                }
            }

            return value?.ToString() ?? string.Empty;
        }

        /// <summary>
        /// Not supported. (Two-way binding is not possible for Description).
        /// <para>サポートされていません（説明文から Enum への逆変換はできません）。</para>
        /// </summary>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}

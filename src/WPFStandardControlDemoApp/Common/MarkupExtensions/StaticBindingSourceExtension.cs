using System.Reflection;
using System.Windows.Markup;

namespace WPFStandardControlDemoApp.Common.MarkupExtensions
{
    /// <summary>
    /// Provides a collection of public static properties from a specified type as a list.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This extension allows you to populate UI controls like ComboBox directly from 
    /// static classes such as <see cref="System.Windows.Media.Brushes"/> or custom constant classes.
    /// Each item in the list is a <see cref="StaticItem"/> object with 'Name' and 'Value' properties.
    /// </para>
    /// <code>
    /// <![CDATA[
    /// /// <ComboBox ItemsSource="{local:StaticBindingSource {x:Type Brushes}}"
    ///           DisplayMemberPath="Name" SelectedValuePath="Value" />
    ///
    /// /// <ComboBox ItemsSource="{local:StaticBindingSource {x:Type Brushes}, Exclude='Transparent, White'}" />
    ///
    /// /// <ComboBox ItemsSource="{local:StaticBindingSource {x:Type Brushes}, IncludeNull=True, NullLabel='None'}" />
    ///
    /// /// <ComboBox ItemsSource="{local:StaticBindingSource {x:Type Brushes}, IsSorted=True}" />
    /// ]]>
    /// </code>
    /// </remarks>
    public class StaticBindingSourceExtension : MarkupExtension
    {
        /// <summary>
        /// Represents a single static property item for UI binding, 
        /// pairing the property name with its actual value.
        /// </summary>
        /// <remarks>
        /// In XAML, bind <c>DisplayMemberPath</c> to "Name" and <c>SelectedValuePath</c> to "Value".
        /// </remarks>
        public class StaticItem(string name, object? value)
        {
            /// <summary>
            /// Gets the name of the static property.
            /// </summary>
            public string Name { get; } = name;

            /// <summary>
            /// Gets the value of the static property.
            /// </summary>
            public object? Value { get; } = value;
        }

        /// <summary>
        /// Gets or sets the target class type to extract static properties from.
        /// </summary>
        [ConstructorArgument("targetType")]
        public Type TargetType { get; set; } = null!;

        /// <summary>
        /// Gets or sets whether to include a null/empty item 
        /// at the beginning of the list.
        /// </summary>
        public bool IncludeNull { get; set; }

        /// <summary>
        /// Gets or sets the label for the null item. 
        /// Default is an empty string.
        /// </summary>
        public string NullLabel { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets a comma-separated list of property names 
        /// to exclude from the list (case-insensitive).
        /// </summary>
        public string? Exclude { get; set; }

        /// <summary>
        /// Gets or sets whether to sort the items by Name.
        /// </summary>
        public bool IsSorted { get; set; }

        /// <summary>
        /// Gets or sets whether the sort order is ascending. 
        /// Default is true.
        /// </summary>
        public bool IsAscending { get; set; } = true;

        public StaticBindingSourceExtension() { }
        public StaticBindingSourceExtension(Type targetType) => TargetType = targetType;

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (TargetType == null) return null!;

            var excludeList = Exclude?.Split(',')
                .Select(x => x.Trim())
                .Where(x => !string.IsNullOrEmpty(x))
                ?? Enumerable.Empty<string>();

            var query = TargetType.GetProperties(BindingFlags.Public | BindingFlags.Static)
                .Where(p => !excludeList.Any(ex => string.Equals(ex, p.Name, StringComparison.OrdinalIgnoreCase)))
                .Select(p => new StaticItem(p.Name, p.GetValue(null)));

            if (IsSorted)
            {
                query = IsAscending ? query.OrderBy(i => i.Name) : query.OrderByDescending(i => i.Name);
            }

            var items = query.ToList();

            if (IncludeNull)
            {
                items.Insert(0, new StaticItem(NullLabel, null));
            }

            return items;
        }
    }
}

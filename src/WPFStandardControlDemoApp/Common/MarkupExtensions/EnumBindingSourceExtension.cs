using System.ComponentModel;
using System.Reflection;
using System.Windows.Markup;

namespace WPFStandardControlDemoApp.Common.MarkupExtensions
{
    /// <summary>
    /// Provides a list of Enum members as name-value pairs, 
    /// automatically resolving <see cref="DescriptionAttribute"/> for human-readable display names.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This extension populates ItemsControl sources (e.g., ComboBox). 
    /// If a <see cref="DescriptionAttribute"/> is present on an Enum member, its value is used for the 'Name' property. 
    /// Otherwise, it falls back to the standard Enum field name.
    /// </para>
    /// <code>
    /// <![CDATA[
    /// /// <ComboBox ItemsSource="{local:EnumBindingSource {x:Type sys:DayOfWeek}}"
    ///           DisplayMemberPath="Name" SelectedValuePath="Value" />
    ///
    /// /// <ComboBox ItemsSource="{local:EnumBindingSource {x:Type sys:DayOfWeek}, Exclude='Saturday, Sunday'}" />
    ///
    /// /// <ComboBox ItemsSource="{local:EnumBindingSource {x:Type sys:DayOfWeek}, IncludeNull=True, NullLabel='(Select)'}" />
    ///
    /// /// <ComboBox ItemsSource="{local:EnumBindingSource {x:Type sys:DayOfWeek}, IsSorted=True, IsAscending=True}" />
    /// ]]>
    /// </code>
    /// </remarks>
    public class EnumBindingSourceExtension : MarkupExtension
    {
        /// <summary>
        /// Represents a single selectable item for UI binding, 
        /// pairing an Enum's display name with its actual field value.
        /// </summary>
        /// <remarks>
        /// In XAML, bind <c>DisplayMemberPath</c> to "Name" and <c>SelectedValuePath</c> to "Value".
        /// </remarks>
        public class EnumItem(string name, object? value)
        {
            /// <summary>
            /// Gets the resolved display name 
            /// (derived from DescriptionAttribute or the field name).
            /// </summary>
            public string Name { get; } = name;

            /// <summary>
            /// Gets the actual Enum field value.
            /// </summary>
            public object? Value { get; } = value;
        }

        /// <summary>
        /// Gets or sets the Enum type to be enumerated.
        /// </summary>
        [ConstructorArgument("enumType")]
        public Type EnumType { get; set; } = null!;

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
        /// Gets or sets a comma-separated list of Enum field names 
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

        public EnumBindingSourceExtension() { }
        public EnumBindingSourceExtension(Type enumType) => EnumType = enumType;

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (EnumType == null || !EnumType.IsEnum) return null!;

            var excludeList = Exclude?.Split(',')
                .Select(x => x.Trim())
                .Where(x => !string.IsNullOrEmpty(x))
                ?? Enumerable.Empty<string>();

            var query = Enum.GetValues(EnumType)
                .Cast<Enum>()
                .Where(e => !excludeList.Any(ex => string.Equals(ex, e.ToString(), StringComparison.OrdinalIgnoreCase)))
                .Select(e => new EnumItem(GetDescription(e), e));

            if (IsSorted)
            {
                query = IsAscending ? query.OrderBy(i => i.Name) : query.OrderByDescending(i => i.Name);
            }

            var items = query.ToList();

            if (IncludeNull)
            {
                items.Insert(0, new EnumItem(NullLabel, null));
            }

            return items;
        }

        private static string GetDescription(Enum enumValue)
        {
            return enumValue.GetType()
                .GetField(enumValue.ToString())
                ?.GetCustomAttribute<DescriptionAttribute>()
                ?.Description ?? enumValue.ToString();
        }
    }
}

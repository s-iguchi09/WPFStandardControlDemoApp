using System.Globalization;
using System.Windows;
using System.Windows.Data;
using WPFStandardControlDemoApp.Common.Extensions;

namespace WPFStandardControlDemoApp.Common.Converters
{
    /// <summary>
    /// Specifies the operation type for <see cref="DateTimeAdvancedConverter"/>.
    /// <see cref="DateTimeAdvancedConverter"/> の操作タイプを指定します。
    /// </summary>
    public enum DateTimeOperation
    {
        /// <summary>
        /// No operation. Performs no calculation.
        /// 操作なし。計算を行わず元の値を返します。
        /// </summary>
        None,

        /// <summary>
        /// Addition operation based on the specified <see cref="DateTimeUnit"/>.
        /// 指定された <see cref="DateTimeUnit"/> に基づいて加算操作を行います。
        /// </summary>
        Add,

        /// <summary>
        /// Find the next occurrence of the specified <see cref="DayOfWeek"/>.
        /// 次の指定された <see cref="DayOfWeek"/> を検索します。
        /// </summary>
        Next,

        /// <summary>
        /// Find the previous occurrence of the specified <see cref="DayOfWeek"/>.
        /// 前の指定された <see cref="DayOfWeek"/> を検索します。
        /// </summary>
        Previous
    }

    /// <summary>
    /// Specifies the time unit for addition operations in <see cref="DateTimeAdvancedConverter"/>.
    /// <see cref="DateTimeAdvancedConverter"/> における加算操作の時間の単位を指定します。
    /// </summary>
    public enum DateTimeUnit
    {
        /// <summary>
        /// Normal calendar days.
        /// 通常のカレンダー日。
        /// </summary>
        Day,

        /// <summary>
        /// Workdays skipping weekends (Saturday and Sunday).
        /// 週末（土日）を除いた営業日。
        /// </summary>
        Workday,

        /// <summary>
        /// Full weeks (increments of 7 days).
        /// 週単位（7日ごとの増分）。
        /// </summary>
        Week,

        /// <summary>
        /// Calendar months.
        /// カレンダー月。
        /// </summary>
        Month,

        /// <summary>
        /// Calendar years.
        /// カレンダー年。
        /// </summary>
        Year
    }

    /// <summary>
    /// Multi-functional Date Converter: Supports adding intervals, business days, and finding specific weekdays.
    /// 多機能日付コンバーター：日付の加算、営業日計算、特定の曜日の検索をサポートします。
    /// </summary>
    [ValueConversion(typeof(DateTime), typeof(DateTime))]
    public class DateTimeAdvancedConverter : MarkupConverterBase
    {
        /// <summary>
        /// Gets or sets the <see cref="DateTimeOperation"/>.
        /// <see cref="DateTimeOperation"/> を取得または設定します。
        /// </summary>
        public DateTimeOperation Operation { get; set; } = DateTimeOperation.None;

        /// <summary>
        /// Gets or sets the <see cref="DateTimeUnit"/> to add.
        /// 加算する <see cref="DateTimeUnit"/> を取得または設定します。
        /// </summary>
        public DateTimeUnit Unit { get; set; } = DateTimeUnit.Day;

        /// <summary>
        /// Gets or sets the numeric offset to apply (positive or negative).
        /// 適用するオフセット値を取得または設定します（正または負の値）。
        /// </summary>
        public int Offset { get; set; } = 0;

        /// <summary>
        /// Gets or sets the target <see cref="DayOfWeek"/> for Next/Previous operations.
        /// <see cref="DateTimeOperation.Next"/> または <see cref="DateTimeOperation.Previous"/> 操作の対象となる <see cref="DayOfWeek"/> を取得または設定します。
        /// </summary>
        public DayOfWeek? TargetDay { get; set; }

        /// <summary>
        /// Performs the date conversion based on the set properties.
        /// 設定されたプロパティに基づいて日付変換を実行します。
        /// </summary>
        /// <param name="value">The <see cref="DateTime"/> value to convert.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>A converted <see cref="DateTime"/> value.</returns>
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not DateTime baseDate) return DependencyProperty.UnsetValue;

            try
            {
                return Operation switch
                {
                    DateTimeOperation.Add => HandleAddition(baseDate),
                    DateTimeOperation.Next => baseDate.GetClosestWeekday(TargetDay ?? DayOfWeek.Monday, true),
                    DateTimeOperation.Previous => baseDate.GetClosestWeekday(TargetDay ?? DayOfWeek.Monday, false),
                    _ => baseDate
                };
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"DateTimeAdvancedConverter Error: {ex.Message}");
                return baseDate;
            }
        }

        private DateTime HandleAddition(DateTime date)
        {
            return Unit switch
            {
                DateTimeUnit.Year => date.AddYears(Offset),
                DateTimeUnit.Month => date.AddMonths(Offset),
                DateTimeUnit.Week => date.AddDays(Offset * 7),
                DateTimeUnit.Workday => date.AddWorkdays(Offset),
                _ => date.AddDays(Offset)
            };
        }
    }
}

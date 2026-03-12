namespace WPFStandardControlDemoApp.Common.Extensions
{
    /// <summary>
    /// Provides advanced date calculation methods for <see cref="DateTime"/>.
    /// <see cref="DateTime"/> に対する高度な日付計算メソッドを提供します。
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Adds a specified number of workdays (skipping Saturday and Sunday) to the <see cref="DateTime"/>.
        /// 指定した営業日数（土日を除く）を <see cref="DateTime"/> に加算または減算します。
        /// </summary>
        /// <param name="date">
        /// The starting date.
        /// 開始日。
        /// </param>
        /// <param name="days">
        /// Number of workdays to add.
        /// 加算する営業日数。
        /// </param>
        /// <returns>
        /// The calculated <see cref="DateTime"/>.
        /// 計算後の <see cref="DateTime"/>。
        /// </returns>
        public static DateTime AddWorkdays(this DateTime date, int days)
        {
            if (days == 0) return date;

            if (date.DayOfWeek == DayOfWeek.Saturday) date = date.AddDays(days > 0 ? 2 : -1);
            else if (date.DayOfWeek == DayOfWeek.Sunday) date = date.AddDays(days > 0 ? 1 : -2);

            int fullWeeks = days / 5;
            date = date.AddDays(fullWeeks * 7);
            int remainingDays = days % 5;

            while (remainingDays != 0)
            {
                int step = Math.Sign(remainingDays);
                date = date.AddDays(step);
                if (date.DayOfWeek is not (DayOfWeek.Saturday or DayOfWeek.Sunday))
                {
                    remainingDays -= step;
                }
            }
            return date;
        }

        /// <summary>
        /// Returns the next or previous occurrence of a specific <see cref="DayOfWeek"/>.
        /// 次または前の指定した <see cref="DayOfWeek"/> の日付を返します。
        /// </summary>
        /// <param name="date">
        /// Base date.
        /// 基準日。
        /// </param>
        /// <param name="targetDay">
        /// The <see cref="DayOfWeek"/> to find.
        /// 検索する <see cref="DayOfWeek"/>。
        /// </param>
        /// <param name="lookForward">
        /// True to find future date, False for past.
        /// 未来なら True、過去なら False。
        /// </param>
        /// <returns>
        /// The calculated <see cref="DateTime"/>.
        /// 計算後の <see cref="DateTime"/>。
        /// </returns>
        public static DateTime GetClosestWeekday(this DateTime date, DayOfWeek targetDay, bool lookForward)
        {
            int diff = (int)targetDay - (int)date.DayOfWeek;

            if (lookForward) { if (diff <= 0) diff += 7; }
            else { if (diff >= 0) diff -= 7; }

            return date.AddDays(diff);
        }
    }
}

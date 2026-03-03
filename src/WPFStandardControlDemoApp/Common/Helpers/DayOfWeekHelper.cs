namespace WPFStandardControlDemoApp.Common.Helpers
{
    public class DayOfWeekHelper
    {
        public static IEnumerable<DayOfWeek> AllDaysOfWeeks => Enum.GetValues<DayOfWeek>();
    }
}

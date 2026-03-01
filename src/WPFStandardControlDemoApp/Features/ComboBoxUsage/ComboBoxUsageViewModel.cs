using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace WPFStandardControlDemoApp.Features.ComboBoxUsage
{
    public class ComboBoxUsageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public Dictionary<DayOfWeek, string> ShortingDayOfWeeks { get; set; } = new Dictionary<DayOfWeek, string>()
        {
            {DayOfWeek.Sunday ,"Sun"},
            {DayOfWeek.Monday ,"Mon"},
            {DayOfWeek.Tuesday ,"Tue"},
            {DayOfWeek.Wednesday ,"Wed"},
            {DayOfWeek.Thursday ,"Thu"},
            {DayOfWeek.Friday ,"Fri"},
            {DayOfWeek.Saturday ,"Sat"},
        };
        public override string ToString()
        {
            return "ComboBox";
        }
    }
}

using System;

namespace BusinessDays.Rules
{
    public class WeekendRule : IRule
    {
        public bool IsBusinessDay(DateTime date)
        {
            return !IsWeekend();

            bool IsWeekend() => date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday;
        }
    }
}

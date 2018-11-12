using System;

namespace BusinessDays.Rules
{
    public class WeekendRule : IRule
    {
        public bool IsBusinessDay(DateTime date)
        {
            return !(date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday);
        }
    }
}

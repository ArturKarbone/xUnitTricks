using System;

namespace BusinessDays.Rules
{
    public interface IRule
    {
        bool IsBusinessDay(DateTime date);
    }
}

using BusinessDays.Rules;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessDays
{
    public class Calculator
    {
        public List<IRule> Rules { get; } = new List<IRule>();

        public void AddRule(IRule rule)
        {
            Rules.Add(rule);
        }

        public bool IsBusinessDay(DateTime date)
        {
            if (!Rules.Any())
            {
                throw new NoRulesException();
            }

            return Rules.All(r => r.IsBusinessDay(date));

            //foreach (var rule in Rules)
            //{
            //    if (!rule.IsBusinessDay(date))
            //    {
            //        return false;
            //    }
            //}
            //return true;
        }

        public class NoRulesException : Exception
        {

        }
    }
}

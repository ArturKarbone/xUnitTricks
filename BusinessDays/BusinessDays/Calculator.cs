﻿using BusinessDays.Rules;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessDays
{
    public class Calculator
    {
        protected List<IRule> Rules { get; } = new List<IRule>();

        public Calculator AddRule(IRule rule)
        {
            Rules.Add(rule);
            return this;
        }

        public bool IsBusinessDay(DateTime date)
        {
            EnsureRulesPresence();

            return AllRulesSatisfied();

            bool AllRulesSatisfied() => Rules.All(r => r.IsBusinessDay(date));

            void EnsureRulesPresence()
            {
                if (!Rules.Any())
                {
                    throw new NoRulesException();
                }
            }
        }

        public class NoRulesException : Exception
        {

        }
    }
}

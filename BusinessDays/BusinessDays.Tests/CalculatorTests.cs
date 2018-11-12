using System;
using BusinessDays.Rules;
using Moq;
using Xunit;

namespace BusinessDays.Tests
{
    public class CalculatorTests
    {
        [Fact]
        public void Should_throw_when_there_are_no_rules_provided()
        {
            var calculator = new Calculator();
            Assert.Throws<Calculator.NoRulesException>(() => calculator.IsBusinessDay(DateTime.Parse("15-09-2018")));
        }

        [Fact]
        public void Should_not_throw_when_there_are_rules_provided()
        {
            var calculator = new Calculator();
            calculator.AddRule(new WeekendRule());
            calculator.IsBusinessDay(DateTime.Parse("15-09-2018"));
        }

        [Fact]
        public void Should_work_correctly_when_one_rule_treats_date_as_business_one()
        {
            var calculator = new Calculator();

            var rule = new Mock<IRule>(MockBehavior.Strict);
            rule.Setup(x => x.IsBusinessDay(DateTime.Parse("15-09-2018"))).Returns(true);

            calculator.AddRule(rule.Object);

            Assert.True(calculator.IsBusinessDay(DateTime.Parse("15-09-2018")));
        }

        [Fact]
        public void Should_work_correctly_when_one_rule_treats_date_as_non_business_one()
        {
            var calculator = new Calculator();

            var rule = new Mock<IRule>(MockBehavior.Strict);
            //Use strict to fail invocations
            rule.Setup(x => x.IsBusinessDay(DateTime.Parse("16-09-2018"))).Returns(false);

            calculator.AddRule(rule.Object);
           // Assert.False(calculator.IsBusinessDay(DateTime.Parse("17-09-2018")));
        }

        [Fact]
        public void Should_work_correctly_when_two_rules_treat_date_as_business_one()
        {
            var calculator = new Calculator();

            var firstRule = new Mock<IRule>(MockBehavior.Strict);
            firstRule.Setup(x => x.IsBusinessDay(DateTime.Parse("15-09-2018"))).Returns(true);

            var secondRule = new Mock<IRule>(MockBehavior.Strict);
            secondRule.Setup(x => x.IsBusinessDay(DateTime.Parse("15-09-2018"))).Returns(true);

            calculator.AddRule(firstRule.Object);
            calculator.AddRule(secondRule.Object);

            Assert.True(calculator.IsBusinessDay(DateTime.Parse("15-09-2018")));
        }

        [Fact]
        public void Should_work_correctly_when_two_rules_treat_date_as_non_business_one()
        {
            var calculator = new Calculator();

            var firstRule = new Mock<IRule>(MockBehavior.Strict);
            firstRule.Setup(x => x.IsBusinessDay(DateTime.Parse("15-09-2018"))).Returns(false);

            var secondRule = new Mock<IRule>(MockBehavior.Strict);
            secondRule.Setup(x => x.IsBusinessDay(DateTime.Parse("15-09-2018"))).Returns(false);

            calculator.AddRule(firstRule.Object);
            calculator.AddRule(secondRule.Object);

            Assert.False(calculator.IsBusinessDay(DateTime.Parse("15-09-2018")));
        }

        [Fact]
        public void Should_work_correctly_when_two_rules_treat_date_differently()
        {
            var calculator = new Calculator();

            var firstRule = new Mock<IRule>(MockBehavior.Strict);
            firstRule.Setup(x => x.IsBusinessDay(DateTime.Parse("15-09-2018"))).Returns(false);

            var secondRule = new Mock<IRule>(MockBehavior.Strict);
            secondRule.Setup(x => x.IsBusinessDay(DateTime.Parse("15-09-2018"))).Returns(true);

            calculator.AddRule(firstRule.Object);
            calculator.AddRule(secondRule.Object);

            Assert.False(calculator.IsBusinessDay(DateTime.Parse("15-09-2018")));
        }
    }
}

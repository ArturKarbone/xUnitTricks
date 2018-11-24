using System;
using System.Collections.Generic;
using BusinessDays.Rules;
using Xunit;
using Xunit.Abstractions;

namespace BusinessDays.Tests.Rules
{
    public class WeekendRuleTests
    {
        private readonly WeekendRule _rule;
        private readonly ITestOutputHelper _output;
        public WeekendRuleTests(ITestOutputHelper output)
        {
            _rule = new WeekendRule();
            _output = output;
            _output.WriteLine("Constructor");
        }


        public static IEnumerable<object[]> SampleDaysAndExpectedResults()
        {
            yield return new object[] { true, DateTime.Parse("14-09-2018") };
            yield return new object[] { true, DateTime.Parse("13-09-2018") };
            yield return new object[] { !true, DateTime.Parse("15-09-2018") };
        }

        public class SampleDate
        {

        }
        public static IEnumerable<dynamic> SampleDaysAndExpectedResults2()
        {
            yield return new { expectedBusinessDay = true, date = DateTime.Parse("14-09-2018") };
            //yield return new object { true, DateTime.Parse("13-09-2018") };
            //yield return new object[] { !true, DateTime.Parse("15-09-2018") };
        }

        public static IEnumerable<SampleDate> SampleDaysAndExpectedResults3()
        {
            yield return new SampleDate();
            //yield return new object { true, DateTime.Parse("13-09-2018") };
            //yield return new object[] { !true, DateTime.Parse("15-09-2018") };
        }

        [Fact]
        public void Should_calculate_correctly_for_weekend_rules()
        {
            Assert.True(_rule.IsBusinessDay(DateTime.Parse("14-09-2018")));
            Assert.True(_rule.IsBusinessDay(DateTime.Parse("13-09-2018")));
            Assert.False(_rule.IsBusinessDay(DateTime.Parse("15-09-2018")));
        }

        [Theory]
        [InlineData("13-09-2018")]
        [InlineData("14-09-2018")]
        public void Should_treat_as_business_days(string date)
        {
            Assert.True(_rule.IsBusinessDay(DateTime.Parse(date)));
        }

        [Theory]
        [InlineData("15-09-2018")]
        [InlineData("16-09-2018")]
        public void Should_treat_as_non_business_days(string date)
        {
            Assert.False(_rule.IsBusinessDay(DateTime.Parse(date)));
        }

        [Theory]
        [InlineData(false, "15-09-2018")]
        [InlineData(false, "16-09-2018")]
        [InlineData(true, "14-09-2018")]
        public void Should_calculate_business_days(bool expectedBusinessDay, string date)
        {
            Assert.Equal(expectedBusinessDay, _rule.IsBusinessDay(DateTime.Parse(date)));
        }

        [Theory]
        //MemberData must reference a data type assignable to 'System.Collections.Generic.IEnumerable<object[]>'.
        [MemberData(nameof(SampleDaysAndExpectedResults))]
        //[MemberData(nameof(SampleDaysAndExpectedResults3))]
        //[MemberData(nameof(SampleDaysAndExpectedResults3))]
        public void Should_calculate_business_days_2(bool expectedBusinessDay, DateTime date)
        {
            _output.WriteLine($"Should_calculate_business_days_2 {date}");

            Assert.Equal(expectedBusinessDay, _rule.IsBusinessDay(date));
        }

        //#pragma warning disable xUnit1004

        [Theory(Skip = "Wil fail during reflection")]
        [MemberData(nameof(SampleDaysAndExpectedResults))]
        public void Should_calculate_business_days_3(DateTime date, bool expectedBusinessDay)
        {
            Assert.Equal(expectedBusinessDay, _rule.IsBusinessDay(date));
        }
    }
}

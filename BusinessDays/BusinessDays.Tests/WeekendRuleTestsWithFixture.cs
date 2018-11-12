using BusinessDays.Rules;
using System;
using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;

namespace BusinessDays.Tests
{
    [Collection("WeekendRuleFixture")]
    public class WeekendFixture
    {
        public WeekendRule Rule { get; }
     
        public WeekendFixture()
        {
            Rule = new WeekendRule();
            //output.WriteLine("WeekendFixture ctor");
        }
    }

    [CollectionDefinition("WeekendRuleFixture")]
    public class WeekendFixtureCollection : ICollectionFixture<WeekendFixture>
    {

    }


    
    //public class WeekendRuleTestsWithFixture : IClassFixture<WeekendFixture>
    public class WeekendRuleTestsWithFixture : IClassFixture<WeekendFixture>
    {
        private readonly WeekendFixture Fixture;
        private readonly ITestOutputHelper _output;
        public WeekendRuleTestsWithFixture(ITestOutputHelper output, WeekendFixture fixture)
        {
            Fixture = fixture;
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
        
        [Fact]
        public void Should_calculate_correctly_for_week_rule()
        {
            Assert.True(Fixture.Rule.IsBusinessDay(DateTime.Parse("14-09-2018")));
            Assert.True(Fixture.Rule.IsBusinessDay(DateTime.Parse("13-09-2018")));
            Assert.False(Fixture.Rule.IsBusinessDay(DateTime.Parse("15-09-2018")));
        }

        [Theory]
        [InlineData("13-09-2018")]
        [InlineData("14-09-2018")]
        public void Should_treat_as_business_days(string date)
        {
            Assert.True(Fixture.Rule.IsBusinessDay(DateTime.Parse(date)));
        }

        [Theory]
        [InlineData("15-09-2018")]
        [InlineData("16-09-2018")]
        public void Should_treat_as_non_business_days(string date)
        {
            Assert.False(Fixture.Rule.IsBusinessDay(DateTime.Parse(date)));
        }

        [Theory]
        [InlineData(false, "15-09-2018")]
        [InlineData(false, "16-09-2018")]
        [InlineData(true, "14-09-2018")]
        public void Should_calculate_business_days(bool expectedBusinessDay, string date)
        {
            Assert.Equal(expectedBusinessDay, Fixture.Rule.IsBusinessDay(DateTime.Parse(date)));
        }

        [Theory]
        //MemberData must reference a data type assignable to 'System.Collections.Generic.IEnumerable<object[]>'.
        [MemberData(nameof(SampleDaysAndExpectedResults))]
        //[MemberData(nameof(SampleDaysAndExpectedResults3))]
        //[MemberData(nameof(SampleDaysAndExpectedResults3))]
        public void Should_calculate_business_days_2(bool expectedBusinessDay, DateTime date)
        {
            _output.WriteLine($"Should_calculate_business_days_2 {date}");

            Assert.Equal(expectedBusinessDay, Fixture.Rule.IsBusinessDay(date));
        }

        //#pragma warning disable xUnit1004

        [Theory(Skip = "Wil fail during reflection")]
        [MemberData(nameof(SampleDaysAndExpectedResults))]
        public void Should_calculate_business_days_3(DateTime date, bool expectedBusinessDay)
        {
            Assert.Equal(expectedBusinessDay, Fixture.Rule.IsBusinessDay(date));
        }
    }
}

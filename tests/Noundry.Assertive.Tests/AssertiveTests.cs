using System;
using System.Collections.Generic;
using Xunit;
using Noundry.Assertive;

namespace Noundry.Assertive.Tests
{
    public class AssertiveTests
    {
        [Fact]
        public void Assert_ReturnsAssertiveInstance()
        {
            var result = "test".Assert();
            Assert.NotNull(result);
            Assert.IsType<Assertive<string>>(result);
        }

        [Fact]
        public void IsNotNull_PassesForNonNullValue()
        {
            var result = "test".Assert().IsNotNull();
            Assert.NotNull(result);
        }

        [Fact]
        public void IsNotNull_ThrowsForNullValue()
        {
            string nullValue = null;
            var exception = Assert.Throws<AssertionException>(() => nullValue.Assert().IsNotNull());
            Assert.Contains("Expected value to not be null", exception.Message);
        }

        [Fact]
        public void IsNull_PassesForNullValue()
        {
            string nullValue = null;
            var result = nullValue.Assert().IsNull();
            Assert.NotNull(result);
        }

        [Fact]
        public void IsNull_ThrowsForNonNullValue()
        {
            var exception = Assert.Throws<AssertionException>(() => "test".Assert().IsNull());
            Assert.Contains("Expected value to be null", exception.Message);
        }

        [Fact]
        public void IsEqualTo_PassesForEqualValues()
        {
            var result = 42.Assert().IsEqualTo(42);
            Assert.NotNull(result);
        }

        [Fact]
        public void IsEqualTo_ThrowsForDifferentValues()
        {
            var exception = Assert.Throws<AssertionException>(() => 42.Assert().IsEqualTo(43));
            Assert.Contains("Expected value to be '43', but was '42'", exception.Message);
            Assert.Equal(43, exception.Expected);
            Assert.Equal(42, exception.Actual);
        }

        [Fact]
        public void IsNotEqualTo_PassesForDifferentValues()
        {
            var result = 42.Assert().IsNotEqualTo(43);
            Assert.NotNull(result);
        }

        [Fact]
        public void IsNotEqualTo_ThrowsForEqualValues()
        {
            var exception = Assert.Throws<AssertionException>(() => 42.Assert().IsNotEqualTo(42));
            Assert.Contains("Expected value to not be '42'", exception.Message);
        }

        [Fact]
        public void IsOfType_PassesForCorrectType()
        {
            var result = "test".Assert().IsOfType<string>();
            Assert.NotNull(result);
        }

        [Fact]
        public void IsOfType_ThrowsForIncorrectType()
        {
            var exception = Assert.Throws<AssertionException>(() => "test".Assert().IsOfType<int>());
            Assert.Contains("Expected type 'Int32', but found 'String'", exception.Message);
        }

        [Fact]
        public void IsOfType_HandlesNullValue()
        {
            string nullValue = null;
            var exception = Assert.Throws<AssertionException>(() => nullValue.Assert().IsOfType<string>());
            Assert.Contains("Expected type 'String', but found 'null'", exception.Message);
        }

        [Fact]
        public void IsNotOfType_PassesForDifferentType()
        {
            var result = "test".Assert().IsNotOfType<int>();
            Assert.NotNull(result);
        }

        [Fact]
        public void IsNotOfType_ThrowsForSameType()
        {
            var exception = Assert.Throws<AssertionException>(() => "test".Assert().IsNotOfType<string>());
            Assert.Contains("Did not expect type 'String'", exception.Message);
        }

        [Fact]
        public void Satisfies_PassesWhenPredicateReturnsTrue()
        {
            var result = 10.Assert().Satisfies(x => x > 5, "Should be greater than 5");
            Assert.NotNull(result);
        }

        [Fact]
        public void Satisfies_ThrowsWhenPredicateReturnsFalse()
        {
            var exception = Assert.Throws<AssertionException>(() =>
                10.Assert().Satisfies(x => x > 20, "Should be greater than 20"));
            Assert.Contains("Should be greater than 20", exception.Message);
        }

        [Fact]
        public void Satisfies_ThrowsForNullPredicate()
        {
            Assert.Throws<ArgumentNullException>(() => 10.Assert().Satisfies(null));
        }

        [Fact]
        public void Fails_PassesWhenPredicateReturnsFalse()
        {
            var result = 10.Assert().Fails(x => x > 20, "Should not be greater than 20");
            Assert.NotNull(result);
        }

        [Fact]
        public void Fails_ThrowsWhenPredicateReturnsTrue()
        {
            var exception = Assert.Throws<AssertionException>(() =>
                10.Assert().Fails(x => x > 5, "Should not be greater than 5"));
            Assert.Contains("Should not be greater than 5", exception.Message);
        }

        [Fact]
        public void Fails_ThrowsForNullPredicate()
        {
            Assert.Throws<ArgumentNullException>(() => 10.Assert().Fails(null));
        }

        [Fact]
        public void IsInRange_PassesForValueInRange()
        {
            var result = 5.Assert().IsInRange(1, 10);
            Assert.NotNull(result);
        }

        [Fact]
        public void IsInRange_PassesForBoundaryValues()
        {
            1.Assert().IsInRange(1, 10);
            10.Assert().IsInRange(1, 10);
        }

        [Fact]
        public void IsInRange_ThrowsForValueBelowRange()
        {
            var exception = Assert.Throws<AssertionException>(() => 0.Assert().IsInRange(1, 10));
            Assert.Contains("Expected value to be between 1 and 10", exception.Message);
        }

        [Fact]
        public void IsInRange_ThrowsForValueAboveRange()
        {
            var exception = Assert.Throws<AssertionException>(() => 11.Assert().IsInRange(1, 10));
            Assert.Contains("Expected value to be between 1 and 10", exception.Message);
        }

        [Fact]
        public void Contains_PassesWhenItemExists()
        {
            var list = new List<int> { 1, 2, 3 };
            var result = list.Assert().Contains(2);
            Assert.NotNull(result);
        }

        [Fact]
        public void Contains_ThrowsWhenItemDoesNotExist()
        {
            var list = new List<int> { 1, 2, 3 };
            var exception = Assert.Throws<AssertionException>(() => list.Assert().Contains(4));
            Assert.Contains("Expected collection to contain '4'", exception.Message);
        }

        [Fact]
        public void DoesNotContain_PassesWhenItemDoesNotExist()
        {
            var list = new List<int> { 1, 2, 3 };
            var result = list.Assert().DoesNotContain(4);
            Assert.NotNull(result);
        }

        [Fact]
        public void DoesNotContain_ThrowsWhenItemExists()
        {
            var list = new List<int> { 1, 2, 3 };
            var exception = Assert.Throws<AssertionException>(() => list.Assert().DoesNotContain(2));
            Assert.Contains("Expected collection to not contain '2'", exception.Message);
        }

        [Fact]
        public void IsEmpty_PassesForEmptyCollection()
        {
            var list = new List<int>();
            var result = list.Assert().IsEmpty<int>();
            Assert.NotNull(result);
        }

        [Fact]
        public void IsEmpty_ThrowsForNonEmptyCollection()
        {
            var list = new List<int> { 1, 2, 3 };
            var exception = Assert.Throws<AssertionException>(() => list.Assert().IsEmpty<int>());
            Assert.Contains("Expected collection to be empty, but it contained 3 item(s)", exception.Message);
        }

        [Fact]
        public void IsNotEmpty_PassesForNonEmptyCollection()
        {
            var list = new List<int> { 1, 2, 3 };
            var result = list.Assert().IsNotEmpty<int>();
            Assert.NotNull(result);
        }

        [Fact]
        public void IsNotEmpty_ThrowsForEmptyCollection()
        {
            var list = new List<int>();
            var exception = Assert.Throws<AssertionException>(() => list.Assert().IsNotEmpty<int>());
            Assert.Contains("Expected collection to not be empty", exception.Message);
        }

        [Fact]
        public void HasCount_PassesForCorrectCount()
        {
            var list = new List<int> { 1, 2, 3 };
            var result = list.Assert().HasCount<int>(3);
            Assert.NotNull(result);
        }

        [Fact]
        public void HasCount_ThrowsForIncorrectCount()
        {
            var list = new List<int> { 1, 2, 3 };
            var exception = Assert.Throws<AssertionException>(() => list.Assert().HasCount<int>(5));
            Assert.Contains("Expected collection to have 5 item(s), but found 3", exception.Message);
        }

        [Fact]
        public void WithContext_AddsContextToErrorMessages()
        {
            var exception = Assert.Throws<AssertionException>(() =>
                42.Assert()
                    .WithContext("User age validation")
                    .IsEqualTo(18));
            Assert.Contains("[User age validation]", exception.Message);
        }

        [Fact]
        public void ChainedAssertions_WorkCorrectly()
        {
            "Hello, World!"
                .Assert()
                .IsNotNull()
                .IsOfType<string>()
                .IsEqualTo("Hello, World!")
                .IsNotEqualTo("Goodbye")
                .Satisfies(s => s.Length > 5, "String should be longer than 5")
                .Fails(s => s.Length < 5, "String should not be shorter than 5");
        }

        [Fact]
        public void ChainedAssertions_StopAtFirstFailure()
        {
            var exception = Assert.Throws<AssertionException>(() =>
                "Test"
                    .Assert()
                    .IsNotNull()
                    .IsEqualTo("Different") // This should fail
                    .IsOfType<string>()); // This should not be reached

            Assert.Contains("Expected value to be 'Different', but was 'Test'", exception.Message);
        }

        [Fact]
        public void Value_Property_ReturnsOriginalValue()
        {
            var assertive = "test".Assert();
            Assert.Equal("test", assertive.Value);
        }

        [Fact]
        public void AssertionException_StoresExpectedAndActual()
        {
            var exception = new AssertionException("Test message", "expected", "actual");
            Assert.Equal("Test message", exception.Message);
            Assert.Equal("expected", exception.Expected);
            Assert.Equal("actual", exception.Actual);
        }
    }
}
using System;
using System.Collections.Generic;
using NUnit.Framework;
using Noundry.Assertive;

namespace Noundry.Assertive.Tests
{
    [TestFixture]
    public class AssertiveTests
    {
        [Test]
        public void Assert_ReturnsAssertiveInstance()
        {
            var result = "test".Assert();
            NUnit.Framework.Assert.That(result, Is.Not.Null);
            NUnit.Framework.Assert.That(result, Is.TypeOf<Assertive<string>>());
        }

        [Test]
        public void IsNotNull_PassesForNonNullValue()
        {
            var result = "test".Assert().IsNotNull();
            NUnit.Framework.Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void IsNotNull_ThrowsForNullValue()
        {
            string nullValue = null;
            var exception = NUnit.Framework.Assert.Throws<AssertionException>(() => nullValue.Assert().IsNotNull());
            NUnit.Framework.Assert.That(exception.Message, Does.Contain("Expected value to not be null"));
        }

        [Test]
        public void IsNull_PassesForNullValue()
        {
            string nullValue = null;
            var result = nullValue.Assert().IsNull();
            NUnit.Framework.Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void IsNull_ThrowsForNonNullValue()
        {
            var exception = NUnit.Framework.Assert.Throws<AssertionException>(() => "test".Assert().IsNull());
            NUnit.Framework.Assert.That(exception.Message, Does.Contain("Expected value to be null"));
        }

        [Test]
        public void IsEqualTo_PassesForEqualValues()
        {
            var result = 42.Assert().IsEqualTo(42);
            NUnit.Framework.Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void IsEqualTo_ThrowsForDifferentValues()
        {
            var exception = NUnit.Framework.Assert.Throws<AssertionException>(() => 42.Assert().IsEqualTo(43));
            NUnit.Framework.Assert.That(exception.Message, Does.Contain("Expected value to be '43', but was '42'"));
            NUnit.Framework.Assert.That(exception.Expected, Is.EqualTo(43));
            NUnit.Framework.Assert.That(exception.Actual, Is.EqualTo(42));
        }

        [Test]
        public void IsNotEqualTo_PassesForDifferentValues()
        {
            var result = 42.Assert().IsNotEqualTo(43);
            NUnit.Framework.Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void IsNotEqualTo_ThrowsForEqualValues()
        {
            var exception = NUnit.Framework.Assert.Throws<AssertionException>(() => 42.Assert().IsNotEqualTo(42));
            NUnit.Framework.Assert.That(exception.Message, Does.Contain("Expected value to not be '42'"));
        }

        [Test]
        public void IsOfType_PassesForCorrectType()
        {
            var result = "test".Assert().IsOfType<string>();
            NUnit.Framework.Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void IsOfType_ThrowsForIncorrectType()
        {
            var exception = NUnit.Framework.Assert.Throws<AssertionException>(() => "test".Assert().IsOfType<int>());
            NUnit.Framework.Assert.That(exception.Message, Does.Contain("Expected type 'Int32', but found 'String'"));
        }

        [Test]
        public void IsOfType_HandlesNullValue()
        {
            string nullValue = null;
            var exception = NUnit.Framework.Assert.Throws<AssertionException>(() => nullValue.Assert().IsOfType<string>());
            NUnit.Framework.Assert.That(exception.Message, Does.Contain("Expected type 'String', but found 'null'"));
        }

        [Test]
        public void IsNotOfType_PassesForDifferentType()
        {
            var result = "test".Assert().IsNotOfType<int>();
            NUnit.Framework.Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void IsNotOfType_ThrowsForSameType()
        {
            var exception = NUnit.Framework.Assert.Throws<AssertionException>(() => "test".Assert().IsNotOfType<string>());
            NUnit.Framework.Assert.That(exception.Message, Does.Contain("Did not expect type 'String'"));
        }

        [Test]
        public void Satisfies_PassesWhenPredicateReturnsTrue()
        {
            var result = 10.Assert().Satisfies(x => x > 5, "Should be greater than 5");
            NUnit.Framework.Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void Satisfies_ThrowsWhenPredicateReturnsFalse()
        {
            var exception = NUnit.Framework.Assert.Throws<AssertionException>(() =>
                10.Assert().Satisfies(x => x > 20, "Should be greater than 20"));
            NUnit.Framework.Assert.That(exception.Message, Does.Contain("Should be greater than 20"));
        }

        [Test]
        public void Satisfies_ThrowsForNullPredicate()
        {
            NUnit.Framework.Assert.Throws<ArgumentNullException>(() => 10.Assert().Satisfies(null));
        }

        [Test]
        public void Fails_PassesWhenPredicateReturnsFalse()
        {
            var result = 10.Assert().Fails(x => x > 20, "Should not be greater than 20");
            NUnit.Framework.Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void Fails_ThrowsWhenPredicateReturnsTrue()
        {
            var exception = NUnit.Framework.Assert.Throws<AssertionException>(() =>
                10.Assert().Fails(x => x > 5, "Should not be greater than 5"));
            NUnit.Framework.Assert.That(exception.Message, Does.Contain("Should not be greater than 5"));
        }

        [Test]
        public void Fails_ThrowsForNullPredicate()
        {
            NUnit.Framework.Assert.Throws<ArgumentNullException>(() => 10.Assert().Fails(null));
        }

        [Test]
        public void IsInRange_PassesForValueInRange()
        {
            var result = 5.Assert().IsInRange(1, 10);
            NUnit.Framework.Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void IsInRange_PassesForBoundaryValues()
        {
            1.Assert().IsInRange(1, 10);
            10.Assert().IsInRange(1, 10);
        }

        [Test]
        public void IsInRange_ThrowsForValueBelowRange()
        {
            var exception = NUnit.Framework.Assert.Throws<AssertionException>(() => 0.Assert().IsInRange(1, 10));
            NUnit.Framework.Assert.That(exception.Message, Does.Contain("Expected value to be between 1 and 10"));
        }

        [Test]
        public void IsInRange_ThrowsForValueAboveRange()
        {
            var exception = NUnit.Framework.Assert.Throws<AssertionException>(() => 11.Assert().IsInRange(1, 10));
            NUnit.Framework.Assert.That(exception.Message, Does.Contain("Expected value to be between 1 and 10"));
        }

        [Test]
        public void Contains_PassesWhenItemExists()
        {
            var list = new List<int> { 1, 2, 3 };
            var result = list.Assert().Contains(2);
            NUnit.Framework.Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void Contains_ThrowsWhenItemDoesNotExist()
        {
            var list = new List<int> { 1, 2, 3 };
            var exception = NUnit.Framework.Assert.Throws<AssertionException>(() => list.Assert().Contains(4));
            NUnit.Framework.Assert.That(exception.Message, Does.Contain("Expected collection to contain '4'"));
        }

        [Test]
        public void DoesNotContain_PassesWhenItemDoesNotExist()
        {
            var list = new List<int> { 1, 2, 3 };
            var result = list.Assert().DoesNotContain(4);
            NUnit.Framework.Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void DoesNotContain_ThrowsWhenItemExists()
        {
            var list = new List<int> { 1, 2, 3 };
            var exception = NUnit.Framework.Assert.Throws<AssertionException>(() => list.Assert().DoesNotContain(2));
            NUnit.Framework.Assert.That(exception.Message, Does.Contain("Expected collection to not contain '2'"));
        }

        [Test]
        public void IsEmpty_PassesForEmptyCollection()
        {
            var list = new List<int>();
            var result = list.Assert().IsEmpty<int>();
            NUnit.Framework.Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void IsEmpty_ThrowsForNonEmptyCollection()
        {
            var list = new List<int> { 1, 2, 3 };
            var exception = NUnit.Framework.Assert.Throws<AssertionException>(() => list.Assert().IsEmpty<int>());
            NUnit.Framework.Assert.That(exception.Message, Does.Contain("Expected collection to be empty, but it contained 3 item(s)"));
        }

        [Test]
        public void IsNotEmpty_PassesForNonEmptyCollection()
        {
            var list = new List<int> { 1, 2, 3 };
            var result = list.Assert().IsNotEmpty<int>();
            NUnit.Framework.Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void IsNotEmpty_ThrowsForEmptyCollection()
        {
            var list = new List<int>();
            var exception = NUnit.Framework.Assert.Throws<AssertionException>(() => list.Assert().IsNotEmpty<int>());
            NUnit.Framework.Assert.That(exception.Message, Does.Contain("Expected collection to not be empty"));
        }

        [Test]
        public void HasCount_PassesForCorrectCount()
        {
            var list = new List<int> { 1, 2, 3 };
            var result = list.Assert().HasCount<int>(3);
            NUnit.Framework.Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void HasCount_ThrowsForIncorrectCount()
        {
            var list = new List<int> { 1, 2, 3 };
            var exception = NUnit.Framework.Assert.Throws<AssertionException>(() => list.Assert().HasCount<int>(5));
            NUnit.Framework.Assert.That(exception.Message, Does.Contain("Expected collection to have 5 item(s), but found 3"));
        }

        [Test]
        public void WithContext_AddsContextToErrorMessages()
        {
            var exception = NUnit.Framework.Assert.Throws<AssertionException>(() =>
                42.Assert()
                    .WithContext("User age validation")
                    .IsEqualTo(18));
            NUnit.Framework.Assert.That(exception.Message, Does.Contain("[User age validation]"));
        }

        [Test]
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

        [Test]
        public void ChainedAssertions_StopAtFirstFailure()
        {
            var exception = NUnit.Framework.Assert.Throws<AssertionException>(() =>
                "Test"
                    .Assert()
                    .IsNotNull()
                    .IsEqualTo("Different") // This should fail
                    .IsOfType<string>()); // This should not be reached

            NUnit.Framework.Assert.That(exception.Message, Does.Contain("Expected value to be 'Different', but was 'Test'"));
        }

        [Test]
        public void Value_Property_ReturnsOriginalValue()
        {
            var assertive = "test".Assert();
            NUnit.Framework.Assert.That(assertive.Value, Is.EqualTo("test"));
        }

        [Test]
        public void AssertionException_StoresExpectedAndActual()
        {
            var exception = new AssertionException("Test message", "expected", "actual");
            NUnit.Framework.Assert.That(exception.Message, Is.EqualTo("Test message"));
            NUnit.Framework.Assert.That(exception.Expected, Is.EqualTo("expected"));
            NUnit.Framework.Assert.That(exception.Actual, Is.EqualTo("actual"));
        }

        // Additional comprehensive tests for edge cases and missing coverage

        [Test]
        public void IsInRange_ThrowsForNonComparableType()
        {
            var nonComparable = new object();
            var exception = NUnit.Framework.Assert.Throws<AssertionException>(() => nonComparable.Assert().IsInRange(new object(), new object()));
            NUnit.Framework.Assert.That(exception.Message, Does.Contain("does not implement IComparable"));
        }

        [Test]
        public void IsInRange_ThrowsForNullValue()
        {
            string nullValue = null;
            var exception = NUnit.Framework.Assert.Throws<AssertionException>(() => nullValue.Assert().IsInRange("a", "z"));
            NUnit.Framework.Assert.That(exception.Message, Does.Contain("Expected value to be between a and z"));
        }

        [Test]
        public void Contains_ThrowsForNonEnumerableType()
        {
            var nonEnumerable = 42;
            var exception = NUnit.Framework.Assert.Throws<AssertionException>(() => nonEnumerable.Assert().Contains(42));
            NUnit.Framework.Assert.That(exception.Message, Does.Contain("Value is not an enumerable"));
        }

        [Test]
        public void DoesNotContain_ThrowsForNonEnumerableType()
        {
            var nonEnumerable = 42;
            var exception = NUnit.Framework.Assert.Throws<AssertionException>(() => nonEnumerable.Assert().DoesNotContain(42));
            NUnit.Framework.Assert.That(exception.Message, Does.Contain("Value is not an enumerable"));
        }

        [Test]
        public void IsEmpty_ThrowsForNonEnumerableType()
        {
            var nonEnumerable = 42;
            var exception = NUnit.Framework.Assert.Throws<AssertionException>(() => nonEnumerable.Assert().IsEmpty<int>());
            NUnit.Framework.Assert.That(exception.Message, Does.Contain("Value is not an enumerable"));
        }

        [Test]
        public void IsNotEmpty_ThrowsForNonEnumerableType()
        {
            var nonEnumerable = 42;
            var exception = NUnit.Framework.Assert.Throws<AssertionException>(() => nonEnumerable.Assert().IsNotEmpty<int>());
            NUnit.Framework.Assert.That(exception.Message, Does.Contain("Value is not an enumerable"));
        }

        [Test]
        public void HasCount_ThrowsForNonEnumerableType()
        {
            var nonEnumerable = 42;
            var exception = NUnit.Framework.Assert.Throws<AssertionException>(() => nonEnumerable.Assert().HasCount<int>(1));
            NUnit.Framework.Assert.That(exception.Message, Does.Contain("Value is not an enumerable"));
        }

        [Test]
        public void Contains_WorksWithStringCollections()
        {
            var list = new List<string> { "apple", "banana", "cherry" };
            var result = list.Assert().Contains("banana");
            NUnit.Framework.Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void Contains_WorksWithArrays()
        {
            var array = new[] { 10, 20, 30 };
            var result = array.Assert().Contains(20);
            NUnit.Framework.Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void IsOfType_WorksWithInheritance()
        {
            var list = new List<int>();
            var result = list.Assert().IsOfType<System.Collections.IEnumerable>();
            NUnit.Framework.Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void IsNotOfType_WorksWithInheritance()
        {
            var list = new List<int>();
            var result = list.Assert().IsNotOfType<string>();
            NUnit.Framework.Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void WithContext_WorksWithAllAssertions()
        {
            var contextName = "Integration Test Context";
            
            var exception1 = NUnit.Framework.Assert.Throws<AssertionException>(() =>
                42.Assert().WithContext(contextName).IsNull());
            NUnit.Framework.Assert.That(exception1.Message, Does.Contain($"[{contextName}]"));

            var exception2 = NUnit.Framework.Assert.Throws<AssertionException>(() =>
                "test".Assert().WithContext(contextName).IsOfType<int>());
            NUnit.Framework.Assert.That(exception2.Message, Does.Contain($"[{contextName}]"));

            var exception3 = NUnit.Framework.Assert.Throws<AssertionException>(() =>
                10.Assert().WithContext(contextName).Satisfies(x => x < 5, "Should be less than 5"));
            NUnit.Framework.Assert.That(exception3.Message, Does.Contain($"[{contextName}]"));
        }

        [Test]
        public void WithContext_ReturnsNewInstanceWithContext()
        {
            var original = "test".Assert();
            var withContext = original.WithContext("Test Context");
            
            // Should be different instances
            NUnit.Framework.Assert.That(withContext, Is.Not.SameAs(original));
            
            // But should have same value
            NUnit.Framework.Assert.That(withContext.Value, Is.EqualTo(original.Value));
        }

        [Test]
        public void Satisfies_WorksWithComplexPredicates()
        {
            var list = new List<int> { 1, 2, 3, 4, 5 };
            var result = list.Assert().Satisfies(l => l.Count == 5 && l.All(x => x > 0), "List should have 5 positive numbers");
            NUnit.Framework.Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void Fails_WorksWithComplexPredicates()
        {
            var list = new List<int> { 1, 2, 3, 4, 5 };
            var result = list.Assert().Fails(l => l.Count == 0 || l.Any(x => x < 0), "List should not be empty or contain negatives");
            NUnit.Framework.Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void IsInRange_WorksWithDifferentComparableTypes()
        {
            // DateTime
            var date = new DateTime(2023, 6, 15);
            var result1 = date.Assert().IsInRange(new DateTime(2023, 1, 1), new DateTime(2023, 12, 31));
            NUnit.Framework.Assert.That(result1, Is.Not.Null);

            // Decimal
            var result2 = 5.5m.Assert().IsInRange(1.0m, 10.0m);
            NUnit.Framework.Assert.That(result2, Is.Not.Null);

            // Character
            var result3 = 'M'.Assert().IsInRange('A', 'Z');
            NUnit.Framework.Assert.That(result3, Is.Not.Null);
        }

        [Test]
        public void HasCount_WorksWithDifferentCollectionTypes()
        {
            // Array
            var array = new[] { 1, 2, 3 };
            var result1 = array.Assert().HasCount<int>(3);
            NUnit.Framework.Assert.That(result1, Is.Not.Null);

            // HashSet
            var hashSet = new HashSet<string> { "a", "b", "c" };
            var result2 = hashSet.Assert().HasCount<string>(3);
            NUnit.Framework.Assert.That(result2, Is.Not.Null);

            // Dictionary values
            var dict = new Dictionary<int, string> { { 1, "one" }, { 2, "two" } };
            var result3 = dict.Values.Assert().HasCount<string>(2);
            NUnit.Framework.Assert.That(result3, Is.Not.Null);
        }

        [Test]
        public void AllAssertionExceptions_ContainProperExpectedAndActualValues()
        {
            // IsEqualTo
            var ex1 = NUnit.Framework.Assert.Throws<AssertionException>(() => "actual".Assert().IsEqualTo("expected"));
            NUnit.Framework.Assert.That(ex1.Expected, Is.EqualTo("expected"));
            NUnit.Framework.Assert.That(ex1.Actual, Is.EqualTo("actual"));

            // IsNotEqualTo
            var ex2 = NUnit.Framework.Assert.Throws<AssertionException>(() => "same".Assert().IsNotEqualTo("same"));
            NUnit.Framework.Assert.That(ex2.Expected, Is.EqualTo("Not same"));
            NUnit.Framework.Assert.That(ex2.Actual, Is.EqualTo("same"));

            // IsOfType
            var ex3 = NUnit.Framework.Assert.Throws<AssertionException>(() => "string".Assert().IsOfType<int>());
            NUnit.Framework.Assert.That(ex3.Expected, Is.EqualTo("Int32"));
            NUnit.Framework.Assert.That(ex3.Actual, Is.EqualTo("String"));
        }

        [Test]
        public void ErrorMessages_AreDescriptiveAndHelpful()
        {
            // Test various error message formats
            var ex1 = NUnit.Framework.Assert.Throws<AssertionException>(() => "".Assert().IsNull());
            NUnit.Framework.Assert.That(ex1.Message, Does.Contain("Expected value to be null, but was ''"));

            var ex2 = NUnit.Framework.Assert.Throws<AssertionException>(() => 
                new List<int>().Assert().HasCount<int>(5));
            NUnit.Framework.Assert.That(ex2.Message, Does.Contain("Expected collection to have 5 item(s), but found 0"));

            var ex3 = NUnit.Framework.Assert.Throws<AssertionException>(() => 
                100.Assert().IsInRange(1, 50));
            NUnit.Framework.Assert.That(ex3.Message, Does.Contain("Expected value to be between 1 and 50 (inclusive), but was 100"));
        }

        [Test]
        public void MultipleChainedAssertions_WithContext()
        {
            var testData = new { Name = "John", Age = 30, IsActive = true };
            
            testData.Name
                .Assert()
                .WithContext("User name validation")
                .IsNotNull()
                .IsOfType<string>()
                .IsNotEqualTo("")
                .Satisfies(name => name.Length > 0, "Name should not be empty");

            testData.Age
                .Assert()
                .WithContext("Age validation")
                .IsInRange(18, 120)
                .IsOfType<int>()
                .Satisfies(age => age > 0, "Age should be positive");
        }

        [Test]
        public void ComplexCollectionOperations()
        {
            var numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            
            numbers
                .Assert()
                .WithContext("Numbers collection")
                .IsNotEmpty<int>()
                .HasCount<int>(10)
                .Contains(5)
                .DoesNotContain(11)
                .Satisfies(list => list.Sum() == 55, "Sum should be 55")
                .Satisfies(list => list.All(x => x > 0), "All numbers should be positive");
        }

        [Test]
        public void NullCollectionHandling()
        {
            List<int> nullList = null;

            var ex1 = NUnit.Framework.Assert.Throws<AssertionException>(() => nullList.Assert().Contains(1));
            NUnit.Framework.Assert.That(ex1.Message, Does.Contain("Value is not an enumerable"));

            var ex2 = NUnit.Framework.Assert.Throws<AssertionException>(() => nullList.Assert().IsEmpty<int>());
            NUnit.Framework.Assert.That(ex2.Message, Does.Contain("Value is not an enumerable"));

            var ex3 = NUnit.Framework.Assert.Throws<AssertionException>(() => nullList.Assert().HasCount<int>(0));
            NUnit.Framework.Assert.That(ex3.Message, Does.Contain("Value is not an enumerable"));
        }

        [Test]
        public void EdgeCaseValues()
        {
            // Empty string
            "".Assert().IsNotNull().IsOfType<string>().IsEqualTo("");

            // Zero
            0.Assert().IsEqualTo(0).IsInRange(-10, 10).IsOfType<int>();

            // Negative numbers
            (-5).Assert().IsNotEqualTo(5).IsInRange(-10, 0).Satisfies(x => x < 0, "Should be negative");

            // Very large numbers
            long.MaxValue.Assert().IsOfType<long>().Satisfies(x => x > 0, "Should be positive");

            // Min/Max values
            int.MinValue.Assert().IsInRange(int.MinValue, int.MaxValue);
            int.MaxValue.Assert().IsInRange(int.MinValue, int.MaxValue);
        }

        [Test]
        public void DefaultValueHandling()
        {
            // Default struct values
            default(int).Assert().IsEqualTo(0);
            default(bool).Assert().IsEqualTo(false);
            default(DateTime).Assert().IsEqualTo(new DateTime());

            // Nullable types
            int? nullableInt = null;
            nullableInt.Assert().IsNull();

            int? nullableWithValue = 42;
            nullableWithValue.Assert().IsNotNull().IsEqualTo(42);
        }
    }
}
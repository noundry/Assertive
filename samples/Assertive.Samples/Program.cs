using System;
using System.Collections.Generic;
using Assertive;

namespace Assertive.Samples
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Assertive Library Samples ===\n");

            // Example 1: Basic string assertions
            Console.WriteLine("Example 1: String Assertions");
            try
            {
                "Hello, Assertive!"
                    .Assert()
                    .IsNotNull()
                    .IsOfType<string>()
                    .IsEqualTo("Hello, Assertive!")
                    .IsNotEqualTo("World")
                    .Satisfies(s => s.Length > 5, "String should be longer than 5 characters");
                Console.WriteLine("✓ All string assertions passed!\n");
            }
            catch (AssertionException ex)
            {
                Console.WriteLine($"✗ String assertion failed: {ex.Message}\n");
            }

            // Example 2: Integer assertions with predicates
            Console.WriteLine("Example 2: Integer Assertions");
            try
            {
                42
                    .Assert()
                    .IsNotNull()
                    .IsOfType<int>()
                    .IsEqualTo(42)
                    .IsNotEqualTo(100)
                    .IsInRange(1, 100)
                    .Satisfies(x => x > 0, "Number should be positive")
                    .Fails(x => x < 0, "Number should not be negative");
                Console.WriteLine("✓ All integer assertions passed!\n");
            }
            catch (AssertionException ex)
            {
                Console.WriteLine($"✗ Integer assertion failed: {ex.Message}\n");
            }

            // Example 3: Null value assertions
            Console.WriteLine("Example 3: Null Value Assertions");
            try
            {
                string nullValue = null;
                nullValue
                    .Assert()
                    .IsNull();
                Console.WriteLine("✓ Null assertion passed!\n");
            }
            catch (AssertionException ex)
            {
                Console.WriteLine($"✗ Null assertion failed: {ex.Message}\n");
            }

            // Example 4: DateTime assertions
            Console.WriteLine("Example 4: DateTime Assertions");
            try
            {
                var today = DateTime.Now;
                today
                    .Assert()
                    .IsNotNull()
                    .IsOfType<DateTime>()
                    .Satisfies(d => d.Year >= 2024, "Year should be at least 2024")
                    .Satisfies(d => d <= DateTime.Now, "Date should not be in the future");
                Console.WriteLine($"✓ DateTime assertions passed for: {today}\n");
            }
            catch (AssertionException ex)
            {
                Console.WriteLine($"✗ DateTime assertion failed: {ex.Message}\n");
            }

            // Example 5: Collection assertions
            Console.WriteLine("Example 5: Collection Assertions");
            try
            {
                var numbers = new List<int> { 1, 2, 3, 4, 5 };
                numbers
                    .Assert()
                    .IsNotNull()
                    .IsNotEmpty<int>()
                    .HasCount<int>(5)
                    .Contains(3)
                    .DoesNotContain(10)
                    .Satisfies(list => list.Count > 0, "List should not be empty");
                Console.WriteLine("✓ All collection assertions passed!\n");
            }
            catch (AssertionException ex)
            {
                Console.WriteLine($"✗ Collection assertion failed: {ex.Message}\n");
            }

            // Example 6: Empty collection assertions
            Console.WriteLine("Example 6: Empty Collection Assertions");
            try
            {
                var emptyList = new List<string>();
                emptyList
                    .Assert()
                    .IsNotNull()
                    .IsEmpty<string>();
                Console.WriteLine("✓ Empty collection assertion passed!\n");
            }
            catch (AssertionException ex)
            {
                Console.WriteLine($"✗ Empty collection assertion failed: {ex.Message}\n");
            }

            // Example 7: Custom object with context
            Console.WriteLine("Example 7: Custom Object with Context");
            try
            {
                var person = new Person { Name = "John Doe", Age = 30 };
                person
                    .Assert()
                    .WithContext("Person validation")
                    .IsNotNull()
                    .Satisfies(p => p.Age >= 18, "Person should be an adult")
                    .Satisfies(p => !string.IsNullOrEmpty(p.Name), "Person should have a name");
                Console.WriteLine($"✓ Person validation passed for: {person.Name}, Age: {person.Age}\n");
            }
            catch (AssertionException ex)
            {
                Console.WriteLine($"✗ Person validation failed: {ex.Message}\n");
            }

            // Example 8: Demonstrating failure (intentional)
            Console.WriteLine("Example 8: Demonstrating Assertion Failure");
            try
            {
                "Assertive"
                    .Assert()
                    .WithContext("Demo failure")
                    .IsEqualTo("Different String"); // This will fail
            }
            catch (AssertionException ex)
            {
                Console.WriteLine($"✗ Expected failure occurred: {ex.Message}");
                Console.WriteLine($"   Expected: {ex.Expected}");
                Console.WriteLine($"   Actual: {ex.Actual}\n");
            }

            // Example 9: Range validation
            Console.WriteLine("Example 9: Range Validation");
            try
            {
                var temperature = 25.5;
                temperature
                    .Assert()
                    .WithContext("Temperature check")
                    .IsInRange(-50.0, 50.0)
                    .Satisfies(t => t > 0, "Temperature is above freezing");
                Console.WriteLine($"✓ Temperature {temperature}°C is within acceptable range!\n");
            }
            catch (AssertionException ex)
            {
                Console.WriteLine($"✗ Temperature validation failed: {ex.Message}\n");
            }

            // Example 10: Complex chaining
            Console.WriteLine("Example 10: Complex Assertion Chaining");
            try
            {
                var data = new Dictionary<string, int> { ["a"] = 1, ["b"] = 2, ["c"] = 3 };
                data
                    .Assert()
                    .IsNotNull()
                    .IsOfType<Dictionary<string, int>>()
                    .Satisfies(d => d.Count == 3, "Dictionary should have exactly 3 items")
                    .Satisfies(d => d.ContainsKey("b"), "Dictionary should contain key 'b'")
                    .Satisfies(d => d["b"] == 2, "Value for key 'b' should be 2");
                Console.WriteLine("✓ All dictionary assertions passed!\n");
            }
            catch (AssertionException ex)
            {
                Console.WriteLine($"✗ Dictionary assertion failed: {ex.Message}\n");
            }

            Console.WriteLine("=== Sample execution completed ===");
            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }

    // Sample class for demonstration
    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}

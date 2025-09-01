# Noundry.Assertive: Fluent Assertions Made Easy

**Noundry.Assertive** is a simple yet powerful fluent assertion library for .NET, making your tests readable, intuitive, and expressive with zero runtime dependencies.

[![NuGet](https://img.shields.io/nuget/v/Noundry.Assertive.svg?style=flat-square)](https://www.nuget.org/packages/Noundry.Assertive)
![License](https://img.shields.io/github/license/noundry/Noundry.Assertive?style=flat-square)
[![Build Status](https://img.shields.io/github/actions/workflow/status/noundry/Noundry.Assertive/dotnet.yml?style=flat-square)](https://github.com/noundry/Noundry.Assertive/actions)

---

## Why Choose Noundry.Assertive?

- **Fluent and readable** assertions that make your tests self-documenting
- **Zero runtime dependencies** for clean, lightweight integration
- **Easy integration** into your existing projects
- **Intuitive API** for clear, expressive tests
- **Clean error messages** for quicker debugging
- **Comprehensive assertions** for common scenarios
- **Extensible design** for custom validation logic
- **High performance** with compiled binary distribution

---

## Quick Installation

Install directly from NuGet:

```bash
dotnet add package Noundry.Assertive
```

Or add to your `.csproj`:

```xml
<PackageReference Include="Noundry.Assertive" Version="1.0.0" />
```

---

## Usage Examples

### Basic Assertions

```csharp
using Noundry.Assertive;

"Hello, World!"
    .Assert()
    .IsNotNull()
    .IsEqualTo("Hello, World!")
    .IsOfType<string>()
    .Satisfies(s => s.Length > 5, "String length should exceed 5 characters");
```

### Numeric Assertions

```csharp
using Noundry.Assertive;

42.Assert()
    .IsNotNull()
    .IsEqualTo(42)
    .IsOfType<int>()
    .IsInRange(1, 100)
    .Satisfies(x => x > 0, "Number should be positive")
    .Fails(x => x < 0, "Number should not be negative");
```

### Collection Assertions

```csharp
using Noundry.Assertive;

var numbers = new List<int> { 1, 2, 3, 4, 5 };
numbers.Assert()
    .IsNotEmpty<int>()
    .HasCount<int>(5)
    .Contains(3)
    .DoesNotContain(10);
```

### Null Handling

```csharp
using Noundry.Assertive;

string nullValue = null;
nullValue.Assert().IsNull();

string notNull = "value";
notNull.Assert().IsNotNull();
```

### Custom Objects with Context

```csharp
using Noundry.Assertive;

var person = new Person { Name = "John", Age = 30 };
person.Assert()
    .WithContext("Person validation")
    .IsNotNull()
    .Satisfies(p => p.Age >= 18, "Person should be an adult")
    .Satisfies(p => !string.IsNullOrEmpty(p.Name), "Person should have a name");
```

---

## Complete API Reference

### Core Assertions

| Method | Description |
|--------|-------------|
| `IsNotNull()` | Ensures the object isn't null |
| `IsNull()` | Ensures the object is null |
| `IsEqualTo(value)` | Checks equality with expected value |
| `IsNotEqualTo(value)` | Checks inequality with specified value |

### Type Assertions

| Method | Description |
|--------|-------------|
| `IsOfType<T>()` | Checks object's exact type |
| `IsNotOfType<T>()` | Ensures object isn't the specified type |

### Predicate Assertions

| Method | Description |
|--------|-------------|
| `Satisfies(predicate, message)` | Validates custom conditions |
| `Fails(predicate, message)` | Ensures object doesn't match a condition |

### Range Assertions

| Method | Description |
|--------|-------------|
| `IsInRange(min, max)` | Checks if value is within range (inclusive) |

### Collection Assertions

| Method | Description |
|--------|-------------|
| `Contains(item)` | Ensures collection contains the item |
| `DoesNotContain(item)` | Ensures collection doesn't contain the item |
| `IsEmpty<T>()` | Ensures collection is empty |
| `IsNotEmpty<T>()` | Ensures collection is not empty |
| `HasCount<T>(count)` | Ensures collection has specific count |

### Context & Chaining

| Method | Description |
|--------|-------------|
| `WithContext(context)` | Adds context to error messages |
| `Value` | Property to access the underlying value |

---

## Error Handling

Assertive throws `AssertionException` when assertions fail, providing:
- Clear error messages
- Expected vs. actual values
- Optional context information

```csharp
using Noundry.Assertive;

try
{
    42.Assert()
        .WithContext("Age validation")
        .IsEqualTo(18);
}
catch (AssertionException ex)
{
    Console.WriteLine($"Message: {ex.Message}");
    Console.WriteLine($"Expected: {ex.Expected}");
    Console.WriteLine($"Actual: {ex.Actual}");
}
```

---

## Advanced Features

### Method Chaining
All assertion methods return the `Assertive<T>` instance, enabling fluent chaining:

```csharp
using Noundry.Assertive;

myObject
    .Assert()
    .IsNotNull()
    .IsOfType<MyClass>()
    .Satisfies(obj => obj.IsValid)
    .Satisfies(obj => obj.Count > 0);
```

### Custom Predicates
Use `Satisfies` and `Fails` for custom validation logic:

```csharp
using Noundry.Assertive;

email.Assert()
    .Satisfies(e => e.Contains("@"), "Email should contain @")
    .Satisfies(e => e.EndsWith(".com"), "Email should end with .com");
```

### Contextual Assertions
Add context to make error messages more descriptive:

```csharp
using Noundry.Assertive;

user.Assert()
    .WithContext("User registration validation")
    .Satisfies(u => u.Age >= 13, "User must be at least 13 years old");
```

---

## Best Practices

1. **Use descriptive messages** in `Satisfies` and `Fails` methods
2. **Add context** for complex validations using `WithContext`
3. **Chain assertions** logically for better readability
4. **Catch AssertionException** to handle validation failures gracefully
5. **Combine with test frameworks** like xUnit, NUnit, or MSTest

---

## Integration with Test Frameworks

### xUnit
```csharp
using Noundry.Assertive;
using Xunit;

[Fact]
public void TestMethod()
{
    var result = Calculate();
    result.Assert()
        .IsNotNull()
        .IsEqualTo(42);
}
```

### NUnit
```csharp
using Noundry.Assertive;
using NUnit.Framework;

[Test]
public void TestMethod()
{
    var result = Calculate();
    result.Assert()
        .IsNotNull()
        .IsEqualTo(42);
}
```

### MSTest
```csharp
using Noundry.Assertive;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestMethod]
public void TestMethod()
{
    var result = Calculate();
    result.Assert()
        .IsNotNull()
        .IsEqualTo(42);
}
```

---

## Contributing

We welcome contributions! To contribute:

1. Fork this repository
2. Create your feature branch: `git checkout -b feature/YourFeature`
3. Commit your changes: `git commit -m 'Add YourFeature'`
4. Push to the branch: `git push origin feature/YourFeature`
5. Open a Pull Request

### Development Setup

```bash
# Clone the repository
git clone https://github.com/noundry/Noundry.Assertive.git
cd Noundry.Assertive

# Restore dependencies
dotnet restore

# Build the solution
dotnet build

# Run tests
dotnet test

# Run examples
dotnet run --project examples/Noundry.Assertive.Examples
```

---

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

---

## Support

- **Issues**: [GitHub Issues](https://github.com/noundry/Noundry.Assertive/issues)
- **Discussions**: [GitHub Discussions](https://github.com/noundry/Noundry.Assertive/discussions)
- **NuGet**: [Noundry.Assertive](https://www.nuget.org/packages/Noundry.Assertive)

---

## Roadmap

- [ ] Add async assertion support
- [ ] Add more collection assertions
- [ ] Add string-specific assertions
- [ ] Add date/time specific assertions
- [ ] Performance optimizations
- [ ] Additional documentation and examples

---

Made with ❤️ by the Noundry.Assertive community
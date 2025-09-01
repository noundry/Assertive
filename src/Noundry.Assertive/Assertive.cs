using System;
using System.Collections.Generic;
using System.Linq;

namespace Noundry.Assertive
{
    /// <summary>
    /// Extension methods to enable fluent assertions on any object.
    /// </summary>
    public static class AssertiveExtensions
    {
        /// <summary>
        /// Begins a fluent assertion chain for the specified value.
        /// </summary>
        /// <typeparam name="T">The type of the value being asserted.</typeparam>
        /// <param name="value">The value to assert on.</param>
        /// <returns>An Assertive instance for chaining assertions.</returns>
        public static Assertive<T> Assert<T>(this T value)
        {
            return new Assertive<T>(value);
        }
    }

    /// <summary>
    /// Fluent assertion builder for chainable validation methods.
    /// </summary>
    /// <typeparam name="T">The type of the value being asserted.</typeparam>
    public class Assertive<T>
    {
        private readonly T _value;
        private readonly string? _context;

        /// <summary>
        /// Initializes a new instance of the Assertive class.
        /// </summary>
        /// <param name="value">The value to assert on.</param>
        /// <param name="context">Optional context for error messages.</param>
        public Assertive(T value, string? context = null)
        {
            _value = value;
            _context = context;
        }

        /// <summary>
        /// Gets the underlying value being asserted.
        /// </summary>
        public T Value => _value;

        /// <summary>
        /// Asserts that the value is not null.
        /// </summary>
        /// <returns>The current Assertive instance for chaining.</returns>
        /// <exception cref="AssertionException">Thrown when the value is null.</exception>
        public Assertive<T> IsNotNull()
        {
            if (_value == null)
            {
                throw new AssertionException(
                    FormatMessage($"Expected value to not be null."),
                    null,
                    _value);
            }

            return this;
        }

        /// <summary>
        /// Asserts that the value is null.
        /// </summary>
        /// <returns>The current Assertive instance for chaining.</returns>
        /// <exception cref="AssertionException">Thrown when the value is not null.</exception>
        public Assertive<T> IsNull()
        {
            if (_value != null)
            {
                throw new AssertionException(
                    FormatMessage($"Expected value to be null, but was '{_value}'."),
                    null,
                    _value);
            }

            return this;
        }

        /// <summary>
        /// Asserts that the value equals the expected value.
        /// </summary>
        /// <param name="expected">The expected value.</param>
        /// <returns>The current Assertive instance for chaining.</returns>
        /// <exception cref="AssertionException">Thrown when values are not equal.</exception>
        public Assertive<T> IsEqualTo(T expected)
        {
            if (!EqualityComparer<T>.Default.Equals(_value, expected))
            {
                throw new AssertionException(
                    FormatMessage($"Expected value to be '{expected}', but was '{_value}'."),
                    expected,
                    _value);
            }

            return this;
        }

        /// <summary>
        /// Asserts that the value does not equal the unexpected value.
        /// </summary>
        /// <param name="unexpected">The value that should not match.</param>
        /// <returns>The current Assertive instance for chaining.</returns>
        /// <exception cref="AssertionException">Thrown when values are equal.</exception>
        public Assertive<T> IsNotEqualTo(T unexpected)
        {
            if (EqualityComparer<T>.Default.Equals(_value, unexpected))
            {
                throw new AssertionException(
                    FormatMessage($"Expected value to not be '{unexpected}', but it was."),
                    $"Not {unexpected}",
                    _value);
            }

            return this;
        }

        /// <summary>
        /// Asserts that the value is of the specified type.
        /// </summary>
        /// <typeparam name="TExpected">The expected type.</typeparam>
        /// <returns>The current Assertive instance for chaining.</returns>
        /// <exception cref="AssertionException">Thrown when the value is not of the expected type.</exception>
        public Assertive<T> IsOfType<TExpected>()
        {
            if (!(_value is TExpected))
            {
                var actualType = _value == null ? "null" : _value.GetType().Name;
                throw new AssertionException(
                    FormatMessage($"Expected type '{typeof(TExpected).Name}', but found '{actualType}'."),
                    typeof(TExpected).Name,
                    actualType);
            }

            return this;
        }

        /// <summary>
        /// Asserts that the value is not of the specified type.
        /// </summary>
        /// <typeparam name="TUnexpected">The type that should not match.</typeparam>
        /// <returns>The current Assertive instance for chaining.</returns>
        /// <exception cref="AssertionException">Thrown when the value is of the unexpected type.</exception>
        public Assertive<T> IsNotOfType<TUnexpected>()
        {
            if (_value is TUnexpected)
            {
                throw new AssertionException(
                    FormatMessage($"Did not expect type '{typeof(TUnexpected).Name}', but found it."),
                    $"Not {typeof(TUnexpected).Name}",
                    _value?.GetType().Name ?? "null");
            }

            return this;
        }

        /// <summary>
        /// Asserts that the value satisfies the specified predicate.
        /// </summary>
        /// <param name="predicate">The condition to check.</param>
        /// <param name="message">Custom message for assertion failure.</param>
        /// <returns>The current Assertive instance for chaining.</returns>
        /// <exception cref="AssertionException">Thrown when the predicate returns false.</exception>
        public Assertive<T> Satisfies(Func<T, bool> predicate, string? message = null)
        {
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            if (!predicate(_value))
            {
                throw new AssertionException(
                    FormatMessage(message ?? "Value did not satisfy the specified condition."),
                    "Satisfied condition",
                    _value);
            }

            return this;
        }

        /// <summary>
        /// Asserts that the value fails the specified predicate.
        /// </summary>
        /// <param name="predicate">The condition that should not be met.</param>
        /// <param name="message">Custom message for assertion failure.</param>
        /// <returns>The current Assertive instance for chaining.</returns>
        /// <exception cref="AssertionException">Thrown when the predicate returns true.</exception>
        public Assertive<T> Fails(Func<T, bool> predicate, string? message = null)
        {
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            if (predicate(_value))
            {
                throw new AssertionException(
                    FormatMessage(message ?? "Value unexpectedly satisfied the condition."),
                    "Failed condition",
                    _value);
            }

            return this;
        }

        /// <summary>
        /// Asserts that the value is within the specified range (inclusive).
        /// </summary>
        /// <param name="min">The minimum value (inclusive).</param>
        /// <param name="max">The maximum value (inclusive).</param>
        /// <returns>The current Assertive instance for chaining.</returns>
        public Assertive<T> IsInRange(T min, T max)
        {
            var comparable = _value as IComparable<T>;
            if (comparable == null && _value != null)
            {
                throw new AssertionException(
                    FormatMessage($"Value of type {_value.GetType().Name} does not implement IComparable<{typeof(T).Name}>."),
                    null,
                    null);
            }

            if (_value == null || comparable.CompareTo(min) < 0 || comparable.CompareTo(max) > 0)
            {
                throw new AssertionException(
                    FormatMessage($"Expected value to be between {min} and {max} (inclusive), but was {_value}."),
                    $"[{min}, {max}]",
                    _value);
            }

            return this;
        }

        /// <summary>
        /// Asserts that the collection contains the specified item.
        /// </summary>
        /// <param name="item">The item to look for.</param>
        /// <returns>The current Assertive instance for chaining.</returns>
        public Assertive<T> Contains<TItem>(TItem item)
        {
            var enumerable = _value as IEnumerable<TItem>;
            if (enumerable == null)
            {
                throw new AssertionException(
                    FormatMessage($"Value is not an enumerable of {typeof(TItem).Name}."),
                    null,
                    null);
            }

            if (!enumerable.Contains(item))
            {
                throw new AssertionException(
                    FormatMessage($"Expected collection to contain '{item}'."),
                    $"Contains {item}",
                    "Does not contain");
            }

            return this;
        }

        /// <summary>
        /// Asserts that the collection does not contain the specified item.
        /// </summary>
        /// <param name="item">The item that should not be present.</param>
        /// <returns>The current Assertive instance for chaining.</returns>
        public Assertive<T> DoesNotContain<TItem>(TItem item)
        {
            var enumerable = _value as IEnumerable<TItem>;
            if (enumerable == null)
            {
                throw new AssertionException(
                    FormatMessage($"Value is not an enumerable of {typeof(TItem).Name}."),
                    null,
                    null);
            }

            if (enumerable.Contains(item))
            {
                throw new AssertionException(
                    FormatMessage($"Expected collection to not contain '{item}'."),
                    $"Does not contain {item}",
                    "Contains");
            }

            return this;
        }

        /// <summary>
        /// Asserts that the collection is empty.
        /// </summary>
        /// <returns>The current Assertive instance for chaining.</returns>
        public Assertive<T> IsEmpty<TItem>()
        {
            var enumerable = _value as IEnumerable<TItem>;
            if (enumerable == null)
            {
                throw new AssertionException(
                    FormatMessage($"Value is not an enumerable."),
                    null,
                    null);
            }

            if (enumerable.Any())
            {
                var count = enumerable.Count();
                throw new AssertionException(
                    FormatMessage($"Expected collection to be empty, but it contained {count} item(s)."),
                    "Empty",
                    $"Count: {count}");
            }

            return this;
        }

        /// <summary>
        /// Asserts that the collection is not empty.
        /// </summary>
        /// <returns>The current Assertive instance for chaining.</returns>
        public Assertive<T> IsNotEmpty<TItem>()
        {
            var enumerable = _value as IEnumerable<TItem>;
            if (enumerable == null)
            {
                throw new AssertionException(
                    FormatMessage($"Value is not an enumerable."),
                    null,
                    null);
            }

            if (!enumerable.Any())
            {
                throw new AssertionException(
                    FormatMessage($"Expected collection to not be empty, but it was."),
                    "Not empty",
                    "Empty");
            }

            return this;
        }

        /// <summary>
        /// Asserts that the collection has the specified count.
        /// </summary>
        /// <param name="expectedCount">The expected number of items.</param>
        /// <returns>The current Assertive instance for chaining.</returns>
        public Assertive<T> HasCount<TItem>(int expectedCount)
        {
            var enumerable = _value as IEnumerable<TItem>;
            if (enumerable == null)
            {
                throw new AssertionException(
                    FormatMessage($"Value is not an enumerable."),
                    null,
                    null);
            }

            var actualCount = enumerable.Count();
            if (actualCount != expectedCount)
            {
                throw new AssertionException(
                    FormatMessage($"Expected collection to have {expectedCount} item(s), but found {actualCount}."),
                    expectedCount,
                    actualCount);
            }

            return this;
        }

        /// <summary>
        /// Provides a named context for better error messages.
        /// </summary>
        /// <param name="context">The context description.</param>
        /// <returns>A new Assertive instance with the specified context.</returns>
        public Assertive<T> WithContext(string context)
        {
            return new Assertive<T>(_value, context);
        }

        private string FormatMessage(string message)
        {
            return string.IsNullOrEmpty(_context) ? message : $"[{_context}] {message}";
        }
    }

    /// <summary>
    /// Custom exception thrown when an assertion fails.
    /// </summary>
    public class AssertionException : Exception
    {
        /// <summary>
        /// Gets the expected value that caused the assertion to fail.
        /// </summary>
        public object? Expected { get; }

        /// <summary>
        /// Gets the actual value that caused the assertion to fail.
        /// </summary>
        public object? Actual { get; }

        /// <summary>
        /// Initializes a new instance of the AssertionException class.
        /// </summary>
        /// <param name="message">The error message.</param>
        /// <param name="expected">The expected value.</param>
        /// <param name="actual">The actual value.</param>
        public AssertionException(string message, object? expected = null, object? actual = null)
            : base(message)
        {
            Expected = expected;
            Actual = actual;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Simple_calculator
{
    public static class Calculator
    {
        private static readonly IReadOnlyDictionary<string, Func<double, double, string>> _operations;
        private static readonly NumberFormatInfo _numberFormat;

        private static double _left;
        private static double _right;

        static Calculator()
        {
            _numberFormat = CultureInfo.InvariantCulture.NumberFormat;

            _operations = new Dictionary<string, Func<double, double, string>>(6, StringComparer.Ordinal)
            {
                ["+"] = Add,
                ["-"] = Subtract,
                ["*"] = Multiply,
                ["/"] = Divide,
                ["%"] = Remainder,
                ["^"] = Pow
            };
        }

        public static void SetLeft(string str)
        {
            _left = ParseInput(str);
        }

        public static void SetRight(string str)
        {
            _right = ParseInput(str);
        }

        public static string GetResult(string op)
        {
            if (string.IsNullOrEmpty(op))
                throw new ArgumentException("Operation cannot be null or empty.", nameof(op));

            if (!_operations.TryGetValue(op, out var operation))
                throw new ArgumentException($"Unsupported operation: {op}.", nameof(op));

            return operation(_left, _right);
        }

        private static double ParseInput(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                throw new ArgumentException("Input cannot be null or whitespace.", nameof(input));

            if (!double.TryParse(
                input,
                NumberStyles.Float | NumberStyles.AllowThousands,
                CultureInfo.InvariantCulture,
                out double result))
            {
                throw new ArgumentException($"Invalid number format: {input}.", nameof(input));
            }

            return result;
        }

        private static string FormatResult(double value) =>
            Math.Abs(value - Math.Round(value, 8)) < double.Epsilon
                ? value.ToString("0.########", _numberFormat)
                : value.ToString(CultureInfo.InvariantCulture);

        private static string Add(double x, double y) =>
            FormatResult(x + y);

        private static string Subtract(double x, double y) =>
            FormatResult(x - y);

        private static string Multiply(double x, double y) =>
            FormatResult(x * y);

        private static string Divide(double x, double y)
        {
            if (Math.Abs(y) < double.Epsilon)
                throw new DivideByZeroException("Division by zero is not allowed.");

            return FormatResult(x / y);
        }

        private static string Remainder(double x, double y)
        {
            if (Math.Abs(y) < double.Epsilon)
                throw new DivideByZeroException("Modulo by zero is not allowed.");

            return FormatResult(x % y);
        }

        private static string Pow(double x, double y)
        {
            if (double.IsNaN(x) || double.IsNaN(y))
                throw new ArgumentException("Invalid input for power operation.");

            return FormatResult(Math.Pow(x, y));
        }
    }
}
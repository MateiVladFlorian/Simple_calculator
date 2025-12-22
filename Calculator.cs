using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Simple_calculator
{
    public static class Calculator
    {
        private static readonly IReadOnlyDictionary<string, Func<double, double, string>> _binaryOperations;
        private static readonly IReadOnlyDictionary<string, Func<double, string>> _unaryOperations;
        private static readonly NumberFormatInfo _numberFormat;

        private static double _left;
        private static double _right;
        private static double _memory;

        static Calculator()
        {
            _numberFormat = CultureInfo.InvariantCulture.NumberFormat;

            _binaryOperations = new Dictionary<string, Func<double, double, string>>(6, StringComparer.Ordinal)
            {
                ["+"] = Add,
                ["-"] = Subtract,
                ["*"] = Multiply,
                ["/"] = Divide,
                ["%"] = Remainder,
                ["^"] = Power
            };

            _unaryOperations = new Dictionary<string, Func<double, string>>(3, StringComparer.Ordinal)
            {
                ["1/x"] = Reciprocal,
                ["x²"] = Square,
                ["√x"] = SquareRoot
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

        public static void SetMemory(string str)
        {
            _memory = ParseInput(str);
        }

        public static string GetMemory()
        {
            return FormatResult(_memory);
        }

        public static string GetResult(string operation)
        {
            if (string.IsNullOrEmpty(operation))
                throw new ArgumentException("Operation cannot be null or empty.", nameof(operation));

            /* check unary operations first */
            if (_unaryOperations.TryGetValue(operation, out var unaryOp))
                return unaryOp(_left);

            /* check binary operations */
            if (_binaryOperations.TryGetValue(operation, out var binaryOp))
                return binaryOp(_left, _right);

            throw new ArgumentException($"Unsupported operation: {operation}.", nameof(operation));
        }

        public static string GetResult(string operation, double value)
        {
            if (string.IsNullOrEmpty(operation))
                throw new ArgumentException("Operation cannot be null or empty.", nameof(operation));

            if (_unaryOperations.TryGetValue(operation, out var unaryOp))
                return unaryOp(value);

            throw new ArgumentException($"Unsupported unary operation: {operation}.", nameof(operation));
        }

        private static double ParseInput(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                throw new ArgumentException("Input cannot be null or whitespace.", nameof(input));

            if (!double.TryParse(
                input, NumberStyles.Float | NumberStyles.AllowThousands,
                CultureInfo.InvariantCulture, out double result))
                throw new ArgumentException($"Invalid number format: {input}.", nameof(input));

            return result;
        }

        private static string FormatResult(double value)
        {
            if (double.IsNaN(value))
                return "NaN";
            if (double.IsPositiveInfinity(value))
                return "∞";
            if (double.IsNegativeInfinity(value))
                return "-∞";

            /* handle very small numbers to avoid scientific notation */
            if (Math.Abs(value) > 0 && Math.Abs(value) < 0.0001)
                return value.ToString("G6", CultureInfo.InvariantCulture);

            /* for normal numbers, use consistent formatting */
            var formatted = value.ToString("0.########", _numberFormat);

            /* remove trailing zeros and decimal point if not needed */
            if (formatted.Contains('.'))
                formatted = formatted.TrimEnd('0').TrimEnd('.');

            return formatted;
        }

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

        private static string Power(double x, double y)
        {
            if (double.IsNaN(x) || double.IsNaN(y))
                throw new ArgumentException("Invalid input for power operation.");

            return FormatResult(Math.Pow(x, y));
        }

        private static string Reciprocal(double x)
        {
            if (Math.Abs(x) < double.Epsilon)
                throw new DivideByZeroException("Reciprocal of zero is not allowed.");

            return FormatResult(1.0 / x);
        }

        private static string Square(double x) =>
            FormatResult(x * x);

        private static string SquareRoot(double x)
        {
            if (x < 0)
                throw new ArgumentOutOfRangeException(nameof(x),
                    "Square root of negative numbers is not allowed in real domain.");

            return FormatResult(Math.Sqrt(x));
        }

        private static string Negate(double x) =>
            FormatResult(-x);

        private static string Percent(double x) =>
            FormatResult(x / 100.0);

        private static string Factorial(double x)
        {
            if (x < 0 || x > 170)
                throw new ArgumentOutOfRangeException(nameof(x),
                    "Factorial is only defined for non-negative integers <= 170.");

            if (Math.Abs(x - Math.Round(x)) > double.Epsilon)
                throw new ArgumentException("Factorial is only defined for integers.", nameof(x));

            double result = 1;

            for (int i = 2; i <= (int)x; i++)
                result *= i;

            return FormatResult(result);
        }
    }
}
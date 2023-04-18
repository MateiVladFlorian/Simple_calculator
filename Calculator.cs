using System;
using System.Collections.Generic;

namespace Simple_calculator
{
    public class Calculator
    {
        static double left;
        static double right;

        public static void SetLeft(string str)
        {
            left = Convert.ToDouble(str);
        }

        public static void SetRight(string str)
        {
            right = Convert.ToDouble(str);
        }

        public static string GetResult(string op)
        {
            string res = "0";

            switch(op)
            {
                case "+":
                    res = (left + right).ToString();
                    break;
                case "-":
                    res = (left - right).ToString();
                    break;
                case "*":
                    res = (left * right).ToString();
                    break;
                case "/":
                    if (right == 0)
                        res = null;
                    else
                        res = (left / right).ToString();
                    break;
                case "%":
                    if (right == 0)
                        res = null;
                    else
                        res = (left % right).ToString();
                    break;
                case "^":
                    res = Math.Pow(left, right).ToString();
                    break;
            }

            return res;
        }
    }
}

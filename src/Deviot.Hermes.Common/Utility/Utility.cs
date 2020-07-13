using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Deviot.Hermes.Common
{
    public static class Utility
    {
        public static bool CheckHexadecimalId(string id)
        {
            if (id.Length == 24 && CheckHexadecimal(id))
                return true;

            return false;
        }

        public static bool CheckHexadecimal(string value)
        {
            if (value == null)
                return false;

            Regex regex = new Regex("^[a-fA-F0-9]*$");
            if (regex.IsMatch(value))
                return true;

            return false;
        }

        public static bool CheckAlphanumeric(string value)
        {
            if (value == null)
                return false;

            Regex regex = new Regex("^[a-zA-Z0-9]*$");
            if (regex.IsMatch(value))
                return true;

            return false;
        }

        public static bool ValidateAlphanumericWithUnderline(string value)
        {
            if (value == null)
                return false;

            Regex regex = new Regex("^[a-zA-Z0-9_]*$");
            if (regex.IsMatch(value))
                return true;

            return false;
        }

        public static bool ValidateTopic(string value)
        {
            if (value == null)
                return false;

            if (value.Contains($"//"))
                return false;

            Regex regex = new Regex("^[a-zA-Z0-9/]*$");
            if (regex.IsMatch(value))
                return true;

            return false;
        }

        public static List<string> GetAllExceptionMessages(Exception exception)
        {
            var exceptions = new List<string>();
            if (exception.InnerException != null)
                exceptions = GetAllExceptionMessages(exception.InnerException);

            exceptions.Add(exception.Message);
            return exceptions;
        }

        public static bool CheckObjectNullOrEmpty(object value)
        {
            if (value == null)
                return true;

            if (value.ToString() == string.Empty)
                return true;

            return false;
        }

        public static Double ConvertDouble(string value, bool returnZeroOnError = false)
        {
            try
            {
                return Double.Parse(value.Replace('.', ','));
            }
            catch (Exception)
            {
                if (returnZeroOnError)
                    return 0;
                else
                    throw new Exception($"Não foi possível converter o valor {value} em Double.");
            }
        }

        public static Double ConvertDouble(object value, bool returnZeroOnError = false)
        {
            string stringValue = "0";
            if(value == null && !returnZeroOnError)
                throw new Exception($"Não foi possível converter o valor {value} em Double.");
            else
                stringValue = value.ToString();

            return ConvertDouble(stringValue, returnZeroOnError);
        }

        public static double Evaluate(string expr)
        {
            expr = expr.Replace(" ", string.Empty);

            Stack<String> stack = new Stack<String>();
            string value = string.Empty;
            for(int i = 0; i < expr.Length; i++)
            {
                String s = expr.Substring(i, 1);
                char chr = char.Parse(s);

                if (!char.IsDigit(chr) && chr != ',' && value != string.Empty)
                {
                    stack.Push(value);
                    value = string.Empty;
                }

                if (s.Equals("("))
                {

                    string innerExp = "";
                    i++; 
                    int bracketCount = 0;
                    for (; i < expr.Length; i++)
                    {
                        s = expr.Substring(i, 1);

                        if (s.Equals("("))
                            bracketCount++;

                        if (s.Equals(")"))
                            if (bracketCount == 0)
                                break;
                            else
                                bracketCount--;


                        innerExp += s;
                    }

                    stack.Push(Evaluate(innerExp).ToString());
                }
                else if (s.Equals("+")) stack.Push(s);
                else if (s.Equals("-")) stack.Push(s);
                else if (s.Equals("*")) stack.Push(s);
                else if (s.Equals("/")) stack.Push(s);
                else if (char.IsDigit(chr) || chr == ',')
                {
                    value += s;

                    if (value.Split(',').Length > 2)
                        throw new Exception("Valor decimal inválido");

                    if (i == (expr.Length - 1))
                        stack.Push(value);
                }
                else
                {
                    throw new Exception("Só é permitido valores númericos e operadores matemáticos");
                }
            }

            double result = 0;
            while (stack.Count >= 3)
            {
                double right = Convert.ToDouble(stack.Pop());
                string op = stack.Pop();
                double left = Convert.ToDouble(stack.Pop());

                if (op == "+") result = left + right;
                else if (op == "-") result = left - right;
                else if (op == "*") result = left * right;
                else if (op == "/") result = left / right;

                stack.Push(result.ToString());
            }

            return Math.Round(Convert.ToDouble(stack.Pop()),6);
        }
    }
}

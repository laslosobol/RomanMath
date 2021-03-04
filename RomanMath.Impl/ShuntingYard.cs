using System;
using System.Collections.Generic;
using System.Linq;

namespace RomanMath.Impl
{
    public static class ShuntingYard
    {
        public static readonly Dictionary<string, (string symbol, int precedence, bool rightAssociative)> Operators
            = new (string symbol, int precedence, bool rightAssociative)[]
            {
                ("*", 3, false),
                ("/", 3, false),
                ("+", 2, false),
                ("-", 2, false)
            }.ToDictionary(op => op.symbol);

        public static List<string> ToPostfix(string infix)
        {
            var tokens = infix.Split(' ');
            var stack = new Stack<string>();
            var output = new List<string>();
            foreach (var token in tokens)
            {
                if (int.TryParse(token, out _))
                {
                    output.Add(token);
                    Print(token);
                }
                else if (Operators.TryGetValue(token, out var op1))
                {
                    while (stack.Count > 0 && Operators.TryGetValue(stack.Peek(), out var op2))
                    {
                        var c = op1.precedence.CompareTo(op2.precedence);
                        if (c < 0 || !op1.rightAssociative && c <= 0)
                        {
                            output.Add(stack.Pop());
                        }
                        else
                        {
                            break;
                        }
                    }

                    stack.Push(token);
                    Print(token);
                }
            }

            while (stack.Count > 0)
            {
                var top = stack.Pop();
                if (!Operators.ContainsKey(top)) throw new ArgumentException("No matching right parenthesis.");
                output.Add(top);
            }

            Print("pop");
            return output;

            //A little more readable?
            void Print(string action)
                => System.Console.WriteLine("{0,-4} {1,-18} {2}", action + ":",
                    $"stack[ {string.Join(" ", stack.Reverse())} ]", $"out[ {string.Join(" ", output)} ]");
        }
    }
}
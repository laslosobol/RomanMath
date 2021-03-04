using System;
using System.Collections.Generic;
using System.Linq;

namespace RomanMath.Impl
{
	public static class Service
	{
		private static readonly Dictionary<char, int> RomanToArabic = new Dictionary<char, int>()
		{
			{'I', 1},
			{'V', 5},
			{'X', 10},
			{'L', 50},
			{'C', 100},
			{'D', 500},
			{'M', 1000}
		};
		public static int Evaluate(string expression)
		{
			const string allowedTokens = "IVXLCDM+-/* ";
			if (string.IsNullOrEmpty(expression))
			{
				throw new ArgumentNullException();
			}
			if (expression.Any(_ => !allowedTokens.Contains(_)))
				throw new ArgumentException("Forbidden Token!");
			var numerical = expression.Split(" ");
			var numericalExpression = "";
			foreach (var elem in numerical)
			{
				if (ShuntingYard.Operators.ContainsKey(elem))
				{
					numericalExpression += $" {elem} ";
					continue;
				}
				var replace = Convert(elem).ToString();
				numericalExpression += replace;
			}
			try
			{
				return (int) Calculate(ShuntingYard.ToPostfix(numericalExpression));
			}
			catch (ArgumentOutOfRangeException)
			{
				throw new ArgumentException("Invalid expression!");
			}
			
		}

		public static int Convert(string expression)
		{
			var outVal = 0;
			for (var i = 0; i < expression.Length; i++)
			{
				var character = expression[i];

				if (i != expression.Length - 1 && GetRomanValue(expression[i]) < GetRomanValue(expression[i + 1]))
				{
					outVal += GetRomanValue(expression[i + 1]) - GetRomanValue(expression[i]);
					i++;
					continue;
				}
				else
				{
					outVal += GetRomanValue(expression[i]);
				}
			}
			return outVal;
		}
		public static int GetRomanValue(char value)
		{
			return value switch
			{
				'I' => RomanToArabic['I'],
				'V' => RomanToArabic['V'],
				'X' => RomanToArabic['X'],
				'L' => RomanToArabic['L'],
				'C' => RomanToArabic['C'],
				'D' => RomanToArabic['D'],
				'M' => RomanToArabic['M'],
				_ => default
			};
		}

		public static decimal Calculate(IList<string> expression)
		{
			while (expression.Count > 1)
			{
				for (var index = 0; index < expression.Count; index++)
				{
					if (ShuntingYard.Operators.ContainsKey(expression[index]))
					{
						switch (expression[index])
						{
							case "+":
								expression[index] =
									(System.Convert.ToDecimal(expression[index - 1]) +
									 System.Convert.ToDecimal(expression[index - 2])).ToString();
								index -= 2;
								expression.RemoveAt(index);
								expression.RemoveAt(index);
								break;
							case "-":
								expression[index] =
									Math.Abs(System.Convert.ToDecimal(expression[index - 1]) -
									 System.Convert.ToDecimal(expression[index - 2])).ToString();
								index -= 2;
								expression.RemoveAt(index);
								expression.RemoveAt(index);
								break;
							case "*":
								expression[index] =
									(System.Convert.ToDecimal(expression[index - 1]) *
									 System.Convert.ToDecimal(expression[index - 2])).ToString();
								index -= 2;
								expression.RemoveAt(index);
								expression.RemoveAt(index);
								break;
								 
							case "/":
								expression[index] =
									(System.Convert.ToDecimal(expression[index - 2]) /
									 System.Convert.ToDecimal(expression[index - 1])).ToString();
								index -= 2;
								expression.RemoveAt(index);
								expression.RemoveAt(index);
								break;
						}
						break;
					}
				}
			}
			return System.Convert.ToDecimal(expression[0]);
		}
	}
}

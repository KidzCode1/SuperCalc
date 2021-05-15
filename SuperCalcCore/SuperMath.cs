using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCalcCore
{
	public static class SuperMath
	{
		public static bool IsFactor(int mainNumber, int numberToCheck)
		{
			return mainNumber % numberToCheck == 0;
		}

		public static List<int> GetFactors(int value)
		{
			List<int> result = new List<int>();

			if (value == 1)
			{
				result.Add(1);
				return result;
			}

			int testNumber = 2;

			while (testNumber <= value)
			{
				while (IsFactor(value, testNumber))
				{
					result.Add(testNumber);
					value = value / testNumber;
					
					if (value == 1)
						return result;
				}
				testNumber++;
			}

			return result;
		}

		public static int GetDecimalPlaces(decimal n)
		{
			n = Math.Abs(n);  // n == 1.234
			n -= (int)n;  // 1.234 -= 1 ---> n == 0.234
			var decimalPlaces = 0;
			while (n > 0)   // 0.234 true; 0.34 true; 0.4 true
			{
				decimalPlaces++;  // 1; 2; 3
				n *= 10;   // n == 2.34; n == 3.4; 4
				n -= (int)n;  // n == 0.34; n == 0.4; n == 0
			}
			return decimalPlaces;  // 3
		}

		public static List<int> GetCommon(List<int> list1, List<int> list2)
		{
			List<int> list2Clone = new List<int>(list2);
			List<int> answer = new List<int>();
			foreach (int number in list1)
			{
				if (list2Clone.Contains(number))
				{
					list2Clone.Remove(number);
					answer.Add(number);
				}
			}
			return answer;
		}
	}
}

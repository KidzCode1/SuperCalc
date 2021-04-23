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

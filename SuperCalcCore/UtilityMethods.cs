using System;
using System.Linq;

namespace SuperCalcCore
{
	public static class UtilityMethods
	{
		public static string MakeSingular(string unit)
		{
			if (unit == "octopuses")
				return "octopus";

			if (unit.EndsWith("ies") && unit.Length > 5)
				return unit.Substring(0, unit.Length - 3) + 'y';
			if (unit.EndsWith("ses") && unit.Length > 5)
				return unit.Substring(0, unit.Length - 3);

			int lastIndex = unit.Length - 1;
			if (unit.Length > 1 && (unit[lastIndex] == 's' || unit[lastIndex] == 'S'))
				return unit.Substring(0, lastIndex);

			return unit;
		}

		public static double SuperScriptToNormal(string power)
		{
			double result = 0;

			bool isNegative = false;
			foreach (char item in power)
			{
				if (item == 'ˉ')
					isNegative = true;
				else if (item == '⁰')
					result = result*10 + 0;
				else if (item == '¹')
					result = result * 10 + 1;
				else if (item == '²')
					result = result * 10 + 2;
				else if (item == '³')
					result = result * 10 + 3;
				else if (item == '⁴')
					result = result * 10 + 4;
				else if (item == '⁵')
					result = result * 10 + 5;
				else if (item == '⁶')
					result = result * 10 + 6;
				else if (item == '⁷')
					result = result * 10 + 7;
				else if (item == '⁸')
					result = result * 10 + 8;
				else if (item == '⁹')
					result = result * 10 + 9;
			}
			if (isNegative)
				return -result;
			return result;
		}

		public static string NormalToSuperScript(decimal power)
		{
			string str = power.ToString();
			string result = "";
			
			foreach (char chr in str)
			{
				if (chr == '-')
					result += 'ˉ';
				if (chr == '0')
					result += '⁰';
				if (chr == '1')
					result += '¹';
				if (chr == '2')
					result += '²';
				if (chr == '3')
					result += '³';
				if (chr == '4')
					result += '⁴';
				if (chr == '5')
					result += '⁵';
				if (chr == '6')
					result += '⁶';
				if (chr == '7')
					result += '⁷';
				if (chr == '8')
					result += '⁸';
				if (chr == '9')
					result += '⁹';
			}
			return result;

		}
	}
}
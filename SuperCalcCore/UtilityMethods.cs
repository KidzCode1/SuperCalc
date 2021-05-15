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
	}
}
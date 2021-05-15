using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace SuperCalcCore
{
	public static class RegexHelper
	{
		public static T GetValue<T>(MatchCollection matches, string groupName)
		{
			foreach (Match match in matches)
			{
				GroupCollection groups = match.Groups;
				Group group = groups[groupName];
				if (group == null)
					continue;

				string value = group.Value;

				if (string.IsNullOrEmpty(value))
					return default(T);

				if (typeof(T).Name == typeof(double).Name)
					if (double.TryParse(value, out double result))
						return (T)(object)result;

				return (T)(object)value;
			}

			return default(T);
		}
	}
}
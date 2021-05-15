using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace SuperCalcCore
{
	public class FindNumberUnit
	{
		public string number { get; set; }
		public string units { get; set; }

		/// <summary>
		/// Creates a new FindNumberUnit based on the specified input text.
		/// </summary>
		/// <param name="input">The input text to get a match for. For example, "1 1/2 m/h".</param>
		/// <returns>Returns the new FindNumberUnit, or null if a no matches were found for the specified input.</returns>
		public static FindNumberUnit Create(string input)
		{
			const string pattern = @"^(?<number>[\d\s/]+)\s*(?<units>[a-zA-Z].*)$";

			Regex regex = new Regex(pattern);
			MatchCollection matches = regex.Matches(input);
			if (matches.Count == 0)
				return null;

			FindNumberUnit findNumberUnit = new FindNumberUnit();
			findNumberUnit.number = RegexHelper.GetValue<string>(matches, "number").Trim();
			findNumberUnit.units = RegexHelper.GetValue<string>(matches, "units").Trim();
			return findNumberUnit;
		}
	}
}













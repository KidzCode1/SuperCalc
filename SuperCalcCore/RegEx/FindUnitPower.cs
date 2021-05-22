using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace SuperCalcCore
{
	public class FindUnitPower
	{
		public string unit { get; set; }
		public double power { get; set; }
		public string superScriptPower { get; set; }

		/// <summary>
		/// Creates a new FindUnitPower based on the specified input text.
		/// </summary>
		/// <param name="input">The input text to get a match for. For example, "m^123.3".</param>
		/// <returns>Returns the new FindUnitPower, or null if a no matches were found for the specified input.</returns>
		public static FindUnitPower Create(string input)
		{
			const string pattern = @"^((?<unit>[a-zA-Z]+)(\^?(?<power>\-?[\d]+\.?\d*))?(?<superScriptPower>ˉ?[⁰¹²³⁴⁵⁶⁷⁸⁹]+\.?[⁰¹²³⁴⁵⁶⁷⁸⁹]*)?)$";

			Regex regex = new Regex(pattern);
			MatchCollection matches = regex.Matches(input);
			if (matches.Count == 0)
				return null;

			FindUnitPower findUnitPower = new FindUnitPower();
			findUnitPower.unit = RegexHelper.GetValue<string>(matches, "unit");
			findUnitPower.power = RegexHelper.GetValue<double>(matches, "power");
			findUnitPower.superScriptPower = RegexHelper.GetValue<string>(matches, "superScriptPower");
			return findUnitPower;
		}
	}
}
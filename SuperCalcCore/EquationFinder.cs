using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace SuperCalcCore
{
	public class EquationFinder
	{
		public string Left { get; set; }
		public string Right { get; set; }

		/// <summary>
		/// Creates a new EquationFinder based on the specified input text.
		/// </summary>
		/// <param name="input">The input text to get a match for. For example, "1 3/4 / 1 8/7 = 2".</param>
		/// <returns>Returns the new EquationFinder, or null if a no matches were found for the specified input.</returns>
		public static EquationFinder Create(string input)
		{
			const string pattern = @"^(?<Left>.+)\s=\s(?<Right>.+)$";

			Regex regex = new Regex(pattern);
			MatchCollection matches = regex.Matches(input);
			if (matches.Count == 0)
				return null;

			EquationFinder equationFinder = new EquationFinder();
			equationFinder.Left = RegexHelper.GetValue<string>(matches, "Left");
			equationFinder.Right = RegexHelper.GetValue<string>(matches, "Right");
			return equationFinder;
		}
	}
}













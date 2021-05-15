using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace SuperCalcCore
{
	public class DecimalFractionator
	{
		public double wholeNumber { get; set; }
		public double decimalValue { get; set; }
		public double numerator { get; set; }
		public double denominator { get; set; }

		/// <summary>
		/// Creates a new DecimalFractionator based on the specified input text.
		/// </summary>
		/// <param name="input">The input text to get a match for. For example, "1 1/3".</param>
		/// <returns>Returns the new DecimalFractionator, or null if a no matches were found for the specified input.</returns>
		public static DecimalFractionator Create(string input)
		{
			const string pattern = @"^((?<decimal>[+-]?((\d+(\.\d+)?)))|(((?<wholeNumber>[+-]?(\d+))\s)?(?<numerator>[+-]?(\d+))\s?/(?<denominator>(\d+))))$";

			Regex regex = new Regex(pattern);
			MatchCollection matches = regex.Matches(input);
			if (matches.Count == 0)
				return null;

			DecimalFractionator decimalFractionator = new DecimalFractionator();
			decimalFractionator.decimalValue = RegexHelper.GetValue<double>(matches, "decimal");
			decimalFractionator.wholeNumber = RegexHelper.GetValue<double>(matches, "wholeNumber");
			decimalFractionator.numerator = RegexHelper.GetValue<double>(matches, "numerator");
			decimalFractionator.denominator = RegexHelper.GetValue<double>(matches, "denominator");
			return decimalFractionator;
		}
	}
}













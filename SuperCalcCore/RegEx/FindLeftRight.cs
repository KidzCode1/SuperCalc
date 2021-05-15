using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace SuperCalcCore
{
	public class FindLeftRight
	{
		public string Left { get; set; }
		public string Operator { get; set; }
		public string Right { get; set; }

		/// <summary>
		/// Creates a new MyClass based on the specified input text.
		/// </summary>
		/// <param name="input">The input text to get a match for. For example, "1 3/4 / 1 8/7".</param>
		/// <returns>Returns the new MyClass, or null if a no matches were found for the specified input.</returns>
		public static FindLeftRight Create(string input)
		{
			const string pattern = @"^(?<Left>.+)\s(?<Operator>[+\-*/])\s(?<Right>.+)$";

			Regex regex = new Regex(pattern);
			MatchCollection matches = regex.Matches(input);
			if (matches.Count == 0)
				return null;

			FindLeftRight myClass = new FindLeftRight();
			myClass.Left = RegexHelper.GetValue<string>(matches, "Left");
			myClass.Operator = RegexHelper.GetValue<string>(matches, "Operator");
			myClass.Right = RegexHelper.GetValue<string>(matches, "Right");
			return myClass;
		}
	}
}

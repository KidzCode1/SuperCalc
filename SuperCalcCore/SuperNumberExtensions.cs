using System;
using System.Linq;

namespace SuperCalcCore
{
	public static class SuperNumberExtensions
	{
		public static bool Eval(this string str)
		{
			EquationFinder equationFinder = EquationFinder.Create(str);
			if (equationFinder == null)
				throw new ArgumentException($"Could not find equation in \"{str}\"!");

			SuperNumber leftNum = equationFinder.Left.ToNum();
			SuperNumber rightNum = equationFinder.Right.ToNum();

			if (leftNum == rightNum)
				return true;

			return false;
		}

		public static SuperNumber ToNum(this string str)
		{
			FindLeftRight leftRight = FindLeftRight.Create(str);
			if (leftRight != null)
			{
				SuperNumber left = new SuperNumber(leftRight.Left);
				SuperNumber right = new SuperNumber(leftRight.Right);
				switch (leftRight.Operator)
				{
					case "+":
						return left + right;
					case "-":
						return left - right;
					case "*":
						return left * right;
					case "/":
						return left / right;
				}
			}
			return new SuperNumber(str);
		}
	}
}
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace SuperCalcCore
{
	[DebuggerDisplay("{ValueAsStr}")]
	public class SuperNumber
	{
		string valueAsStr;
		decimal valueField;
		public decimal Value
		{
			get
			{
				if (Type == NumberType.Fraction)
					valueField = WholeNumber + (decimal)Numerator / (decimal)Denominator;
				return valueField;
			}

			set => valueField = value;
		}

		
		public string ValueAsStr
		{
			get
			{
				if (Type == NumberType.Decimal)
					return Value.ToString();
				return $"{WholeNumber} {Numerator} / {Denominator}";
			}
		}
		public int WholeNumber { get; set; }
		public int Numerator { get; set; }
		public int Denominator { get; set; }

		public NumberType Type { get; set; }

		public SuperNumber(int wholeNumber, int numerator, int denominator)
		{
			Type = NumberType.Fraction;
			WholeNumber = wholeNumber;
			Numerator = numerator;
			Denominator = denominator;
		}

		public SuperNumber(decimal value)
		{
			Type = NumberType.Decimal;
			Value = value;
		}

		// Implicit conversion to a Decimal:
		public static implicit operator decimal(SuperNumber d) => d.Value;

		public static bool operator ==(SuperNumber left, SuperNumber right)
		{
			if ((object)left == null)
				return (object)right == null;
			else
				return left.Equals(right);
		}

		public static bool operator !=(SuperNumber left, SuperNumber right)
		{
			return !(left == right);
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			if (obj is SuperNumber)
				return Equals((SuperNumber)obj);
			else if (obj is SuperNumber)
				return Equals((SuperNumber)obj);
			else
				return base.Equals(obj);
		}

		public bool Equals(SuperNumber superNumber)
		{
			if (Type == NumberType.Fraction && superNumber.Type == NumberType.Fraction)
			{
				return AreFractionsEquivalent(superNumber);
			}
			return Value == superNumber.Value;
		}

		SuperNumber Clone()
		{
			if (this.Type == NumberType.Decimal)
				return new SuperNumber(this.Value);

			return new SuperNumber(this.WholeNumber, this.Numerator, this.Denominator);
		}

		public bool AreFractionsEquivalent(SuperNumber superNumber)
		{
			SuperNumber thisSuperNumber = Clone();
			SuperNumber compareNumber = superNumber.Clone();

			thisSuperNumber.MakeFractionProper();
			compareNumber.MakeFractionProper();

			return thisSuperNumber.WholeNumber == compareNumber.WholeNumber &&
						thisSuperNumber.Numerator == compareNumber.Numerator &&
						thisSuperNumber.Denominator == compareNumber.Denominator;
		}

		public static SuperNumber operator +(SuperNumber superNumber1, SuperNumber superNumber2)
		{
			if (superNumber1.Type == NumberType.Decimal || superNumber2.Type == NumberType.Decimal)
				return new SuperNumber(superNumber1.Value + superNumber2.Value);

			int newWholeNum;
			int newNumerator;
			int newDenominator;

			newWholeNum = superNumber1.WholeNumber + superNumber2.WholeNumber;
			newDenominator = superNumber1.Denominator * superNumber2.Denominator;
			newNumerator = superNumber1.Numerator * superNumber2.Denominator + superNumber2.Numerator * superNumber1.Denominator;

			SuperNumber answer = new SuperNumber(newWholeNum, newNumerator, newDenominator);
			answer.MakeFractionProper();
			return answer;
		}

		public SuperNumber()
		{

		}
		public void MakeFractionProper()
		{
			List<int> numeratorFactors = SuperMath.GetFactors(Numerator);
			List<int> denominatorFactors = SuperMath.GetFactors(Denominator);
			List<int> commonFactors = SuperMath.GetCommon(numeratorFactors, denominatorFactors);
			foreach (int commonFactor in commonFactors)
			{
				Numerator /= commonFactor;
				Denominator /= commonFactor;
			}
			if (Numerator > Denominator)
			{
				int offset = Numerator / Denominator;
				WholeNumber += offset;
				Numerator -= offset * Denominator;
			}
		}
	}
}













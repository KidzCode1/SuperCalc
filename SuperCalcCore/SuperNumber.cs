using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace SuperCalcCore
{

	[DebuggerDisplay("{ValueAsStr}")]
	public class SuperNumber
	{
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

		public SuperNumber()
		{

		}

		public SuperNumber(string str)
		{
			/* str would be "1 1/2m" */

			// We need a number/units splitter.
			// 1 1/2m -> 1 1/2 and "m" to the power of one.
			// 1 bird -> 1 and "bird" to the power of one.
			// 2m³ -> 2 and "m" to the power of three.

			// What if we found and stripped away units here?
			// DecimalFractionator works well with numbers like this: 1 1/2  0.3456
			// But DecimalFractionator fails with anything with a unit.

			string numberStr;
			FindNumberUnit findNumberUnit = FindNumberUnit.Create(str);
			if (findNumberUnit == null)  // It's just a number - no units.
			{
				numberStr = str;  // Just use that 
			}
			else  // Found units!
			{
				// TODO: Add the units/variables to this number, along with their powers.
				// TODO: Modify all the operator overloads so they still work correctly with units and variables.
				// 
				numberStr = findNumberUnit.number;
			}

			DecimalFractionator decimalFractionator = DecimalFractionator.Create(numberStr);
			if (decimalFractionator == null)
				throw new ArgumentException($"SuperNumber initialization string is invalid: \"{str}\"!");

			if (decimalFractionator.decimalValue != 0)
			{
				InitializeDecimal((decimal)decimalFractionator.decimalValue);
				return;
			}

			WholeNumber = (int)decimalFractionator.wholeNumber;
			Numerator = (int)decimalFractionator.numerator;
			Denominator = (int)decimalFractionator.denominator;
			InitializeFraction(WholeNumber, Numerator, Denominator);
		}

		/// <summary>
		/// Returns a new SuperNumber as a simple fraction.
		/// </summary>
		public SuperNumber Simplify()
		{
			SuperNumber clone = Clone();
			clone.MakeFractionProper();
			return clone;
		}

		string GetFractionDisplayStr()
		{
			//if (Denominator == 1)
			//	return $"{WholeNumber + Numerator}";
			if (WholeNumber == 0)
			{
				return $"{Numerator}/{Denominator}";
			}
			if (Numerator != 0)
			{
				return $"{WholeNumber} {Numerator}/{Denominator}"; // 1 1/2
			}
			else
			{
				return $"{WholeNumber}";
			}
		}

		public string DisplayStr
		{
			get
			{
				if (Type == NumberType.Fraction)
				{
					SuperNumber superNumber = Simplify();
					return superNumber.GetFractionDisplayStr();
				}

				return Value.ToString();
			}
		}
		

		public override string ToString()
		{
			return $"SN({DisplayStr})";
		}


		public SuperNumber(int wholeNumber, int numerator, int denominator)
		{
			InitializeFraction(wholeNumber, numerator, denominator);
		}

		private void InitializeFraction(int wholeNumber, int numerator, int denominator)
		{
			if (numerator == 0 && denominator == 0)
				denominator = 1;  // It's cool. 

			if (denominator <= 0)
				throw new ArgumentException($"Denominator ({denominator}) must always have a positive value!");

			if (Math.Sign(wholeNumber) == 1 && Math.Sign(numerator) == -1)
				throw new ArgumentException($"Make the wholeNumber ({wholeNumber}) negative instead of the numerator ({numerator})!");

			Type = NumberType.Fraction;
			WholeNumber = wholeNumber;
			int multiplier = 1;
			if (wholeNumber == 0)
				multiplier = Math.Sign(numerator);
			else
				multiplier = Math.Sign(wholeNumber);
			Numerator = Math.Abs(numerator) * multiplier;
			Denominator = Math.Abs(denominator);
		}

		public SuperNumber(decimal value)
		{
			InitializeDecimal(value);
		}

		bool IsWholeNumber(decimal value)
		{
			decimal absValue = Math.Abs(value);
			decimal decimalPart = absValue - Math.Floor(absValue);
			if (decimalPart == 0)
				return true;
			return false;
		}

		private void InitializeDecimal(decimal value)
		{
			if (IsWholeNumber(value))
			{
				InitializeFraction((int)value, 0, 0);
				return;
			}
			Type = NumberType.Decimal;
			Value = value;
		}

		// Implicit conversion to a Decimal:
		public static implicit operator decimal(SuperNumber d) => d.Value;
		public static implicit operator string(SuperNumber d) => d.DisplayStr;

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

		public SuperNumber Clone()
		{
			if (Type == NumberType.Decimal)
				return new SuperNumber(Value);

			return new SuperNumber(WholeNumber, Numerator, Denominator);
		}

		public bool AreFractionsEquivalent(SuperNumber superNumber)
		{
			SuperNumber thisSuperNumber = Clone();
			SuperNumber compareNumber = superNumber.Clone();

			thisSuperNumber.MakeFractionProper();
			compareNumber.MakeFractionProper();

			bool denominatorsMatch;
			if (compareNumber.Numerator == 0)
				denominatorsMatch = true;
			else
				denominatorsMatch = thisSuperNumber.Denominator == compareNumber.Denominator;

			return thisSuperNumber.WholeNumber == compareNumber.WholeNumber &&
						thisSuperNumber.Numerator == compareNumber.Numerator &&
						denominatorsMatch;
		}

		public static SuperNumber operator +(SuperNumber superNumber1, SuperNumber superNumber2)
		{
			if (superNumber1.Type == NumberType.Decimal || superNumber2.Type == NumberType.Decimal)
				return new SuperNumber(superNumber1.Value + superNumber2.Value);

			int newWholeNum;
			int newNumerator;
			int newDenominator;

			SuperNumber improperNum1 = superNumber1.CreateImproper();
			SuperNumber improperNum2 = superNumber2.CreateImproper();

			newWholeNum = 0;
			newNumerator = improperNum1.Numerator * improperNum2.Denominator + improperNum2.Numerator * improperNum1.Denominator;
			newDenominator = improperNum1.Denominator * improperNum2.Denominator;

			SuperNumber answer = new SuperNumber(newWholeNum, newNumerator, newDenominator);
			answer.MakeFractionProper();
			return answer;
		}

		public static SuperNumber operator -(SuperNumber superNumber1, SuperNumber superNumber2)
		{
			SuperNumber negativeSup2 = superNumber2.CreateNegative();
			return superNumber1 + negativeSup2;
		}
		public static SuperNumber operator /(SuperNumber superNumber1, SuperNumber superNumber2)
		{
			if (superNumber2.Type == NumberType.Decimal)
				return new SuperNumber(superNumber1.Value / superNumber2.Value);
			
			SuperNumber reciprocalSup2 = superNumber2.CreateReciprocal();
			return superNumber1 * reciprocalSup2;
		}



		public static SuperNumber operator *(SuperNumber superNumber1, SuperNumber superNumber2)
		{
			if (superNumber1.Type == NumberType.Decimal || superNumber2.Type == NumberType.Decimal)
				return new SuperNumber(superNumber1.Value * superNumber2.Value);
			
			int newWholeNum;
			int newNumerator;
			int newDenominator;

			int newSign;

			if (superNumber1.Sign == superNumber2.Sign)
				newSign = 1;
			else
				newSign = -1;

			SuperNumber sn1 = superNumber1.CreateImproper();
			SuperNumber sn2 = superNumber2.CreateImproper();

			newWholeNum = 0;
			newNumerator = Math.Abs(sn1.Numerator * sn2.Numerator);
			newDenominator = Math.Abs(sn1.Denominator * sn2.Denominator);

			SuperNumber answer = new SuperNumber(newSign * newWholeNum, newSign * newNumerator, newDenominator);
			answer.MakeFractionProper();
			return answer;
		}

		public int Sign
		{
			get
			{
				if (Type == NumberType.Decimal)
					return Math.Sign(Value);

				if (Numerator == 0 && WholeNumber == 0)
					return 0;

				if (Numerator == 0)
					return Math.Sign(WholeNumber);

				return Math.Sign(Numerator);
			}
		}

		SuperNumber ConvertToFraction()
		{
			int decimalPlaces = SuperMath.GetDecimalPlaces(Value);
			// Value = 0.5
			// decimalPlaces == 1
			// int tenPower = 10^decimalPlaces;
			// numerator = Value * tenPower = 5
			// denominator =  tenPower

			int tenPower = (int)Math.Pow(10, decimalPlaces);
			int denominator = tenPower;
			int numerator = (int)(Value * tenPower);

			return new SuperNumber(0, numerator, denominator).Simplify();
		}

		public string ImproperFractionStr
		{
			get
			{
				if (Type == NumberType.Decimal)
					return ConvertToFraction().CreateImproper().ImproperFractionStr;
				else
					return $"{Denominator * WholeNumber + Numerator}/{Denominator}";
			}
		}
		public string MixedNumberFractionStr
		{
			get
			{
				if (Type == NumberType.Decimal)
					return ConvertToFraction().MixedNumberFractionStr;
				else if (WholeNumber == 0)
					return $"{Numerator}/{Denominator}";
				return $"{WholeNumber} {Numerator}/{Denominator}";
			}
		}

		/// <summary>
		/// Simplifies the fraction...
		/// </summary>
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
			if (Numerator >= Denominator)
			{
				int offset = Numerator / Denominator;
				WholeNumber += offset;
				Numerator -= offset * Denominator;
			}
		}

		public SuperNumber CreateImproper()
		{
			if (Type == NumberType.Decimal)
				throw new Exception($"Cannot make a decimal into a fraction!");

			// Goals: Make the fraction the same, but I want WholeNumber 0.
			int newNumerator = Numerator + (Denominator * WholeNumber);
			return new SuperNumber(0, newNumerator, Denominator);
		}

		public SuperNumber CreateNegative()
		{
			if (Type == NumberType.Decimal)
				return new SuperNumber(-Value);
			return new SuperNumber(-WholeNumber, -Numerator, Denominator);
		}
	
		public SuperNumber CreateReciprocal()
		{
			if (Type == NumberType.Decimal)
				if (Value == 0)
					throw new Exception("Cannot take the Reciprocal of zero!");
				else
					return new SuperNumber(1 / Value);
			SuperNumber improperFraction = CreateImproper();
			int newNumerator = improperFraction.Denominator;
			int newDenominator = improperFraction.Numerator;
			if (newDenominator < 0)
			{
				newDenominator *= -1;
				newNumerator *= -1;
			}
			return new SuperNumber(0, newNumerator, newDenominator);
		}
	}
}
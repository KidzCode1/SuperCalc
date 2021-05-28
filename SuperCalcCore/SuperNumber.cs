using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace SuperCalcCore
{
	[DebuggerDisplay("{ValueAsStr}")]
	public class SuperNumber
	{
		List<UnitPower> unitPowers = new List<UnitPower>();
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
				return MixedNumberFractionStr;
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
				numberStr = findNumberUnit.number.Trim();
				string[] unitStrs = findNumberUnit.units.Split(' ');
				foreach (string unitStr in unitStrs)
				{
					int slashPos = unitStr.IndexOf("/");
					if (slashPos > 0)
					{
						AddUnit(unitStr.Substring(0, slashPos));
						AddUnit(unitStr.Substring(slashPos + 1), -1);
					}
					else
						AddUnit(unitStr);
				}
				//FindUnitPower
				// TODO: Add the units/variables to this number, along with their powers.
				// TODO: Modify all the operator overloads so they still work correctly with units and variables.
				// TODO: Make sure we use the units/variables in our answers!

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

		private void AddUnit(string unitStr, double powerMultiplier = 1)
		{
			FindUnitPower findUnitPower = FindUnitPower.Create(unitStr);

			if (findUnitPower != null)
			{
				// We got something!!!
				if (findUnitPower.superScriptPower != null)
				{
					// We found the "¹²³" superscript power syntax!
					// ˉ⁰¹²³⁴⁵⁶⁷⁸⁹
					AddUnitPower(findUnitPower.unit, UtilityMethods.SuperScriptToNormal(findUnitPower.superScriptPower) * powerMultiplier);
				}
				else
				{
					// We found the "^123" power syntax!
					AddUnitPower(findUnitPower.unit, findUnitPower.power * powerMultiplier);
				}
				//findUnitPower.unit
			}
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

		bool HasAnyUnitsLongerThan1()
		{
			foreach (UnitPower unitPower in unitPowers)
				if (unitPower.Unit.Length > 1)
					return true;

			return false;
		}

		string GetUnitsDisplayStr()
		{
			string result = "";

			// List all powers on top first.

			// Then check to see if we have any powers on the bottom.

			// If so, add a slash, then list the powers on the bottom.

			bool needLeadingSpaces = HasAnyUnitsLongerThan1();

			bool anyPowersFoundOnTop = false;
			bool firstTimeThrough = true;
			foreach (UnitPower unitPower in unitPowers)
			{
				if (unitPower.Power > 0)
				{
					anyPowersFoundOnTop = true;
					string powerStr = GetSuperscriptPowerStr(unitPower.Power);
					string leadingSpace = GetLeadingSpace(needLeadingSpaces, firstTimeThrough);
					result += $"{leadingSpace}{unitPower.Unit}{powerStr}";

					firstTimeThrough = false;
				}
			}

			firstTimeThrough = true;
			foreach (UnitPower unitPower in unitPowers)
			{
				if (unitPower.Power < 0)
				{
					// At this point, unitPower.Power is negative.
					decimal multiplier = 1;
					if (anyPowersFoundOnTop)
						multiplier = -1;
					string powerStr = GetSuperscriptPowerStr(unitPower.Power * multiplier);
					string leadingSpace = GetLeadingSpace(needLeadingSpaces, firstTimeThrough);

					if (firstTimeThrough)
						if (anyPowersFoundOnTop)  // Example, result at this point == "m" 
						{
							leadingSpace = "";
							result += "/";
						}

					result += $"{leadingSpace}{unitPower.Unit}{powerStr}";
					firstTimeThrough = false;
				}
			}

			return result;  // " m³s"
		}

		private string GetLeadingSpace(bool needLeadingSpaces, bool firstTimeThrough)
		{
			string leadingSpace = " ";
			if (firstTimeThrough && HasDenominator())
			{
				// We need the leading space
			}
			else if (!needLeadingSpaces)
				leadingSpace = "";
			return leadingSpace;
		}

		private static string GetSuperscriptPowerStr(decimal power)
		{
			string powerStr = "";
			if (power != 1)
				powerStr = $"{UtilityMethods.NormalToSuperScript(power)}";
			return powerStr;
		}

		private bool HasDenominator()
		{
			if (Type == NumberType.Fraction && Numerator != 0)
				return true;

			return false;
		}

		string GetFractionDisplayStr()
		{
			return GetMixedFraction() + GetUnitsDisplayStr();
		}

		private string GetMixedFraction()
		{
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

				return Value.ToString() + GetUnitsDisplayStr();
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

		void CopyUnitsFrom(SuperNumber source)
		{
			foreach (UnitPower unitPower in source.unitPowers)
				unitPowers.Add(new UnitPower(unitPower));
		}
		public SuperNumber Clone()
		{
			SuperNumber clone;
			if (Type == NumberType.Decimal)
				clone = new SuperNumber(Value);
			else
				clone = new SuperNumber(WholeNumber, Numerator, Denominator);
			clone.CopyUnitsFrom(this);
			return clone;
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
			if (!superNumber1.HasMatchingUnits(superNumber2))
				throw new ArgumentException("Units do not match");
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
			answer.CopyUnitsFrom(superNumber1);
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
			SuperNumber reciprocalSup2 = superNumber2.CreateReciprocal();
			return superNumber1 * reciprocalSup2;
		}

		public static SuperNumber operator *(SuperNumber superNumber1, SuperNumber superNumber2)
		{
			SuperNumber answer = null;

			if (superNumber1.Type == NumberType.Decimal || superNumber2.Type == NumberType.Decimal)
				answer = new SuperNumber(superNumber1.Value * superNumber2.Value);
			else
				answer = MultiplyFractions(superNumber1, superNumber2);

			// TODO: We are going to need to multiply the units!!!
			answer.MultiplyUnits(superNumber1);
			answer.MultiplyUnits(superNumber2);
			answer.CleanUpAnyZeroPowerUnits();

			return answer;
		}

		private static SuperNumber MultiplyFractions(SuperNumber superNumber1, SuperNumber superNumber2)
		{
			SuperNumber answer;
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

			answer = new SuperNumber(newSign * newWholeNum, newSign * newNumerator, newDenominator);
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

		public SuperNumber ConvertToFraction()
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
					return $"{Denominator * WholeNumber + Numerator}/{Denominator}" + GetUnitsDisplayStr();
			}
		}

		public string MixedNumberFractionStr
		{
			get
			{
				if (Type == NumberType.Decimal)
					return ConvertToFraction().MixedNumberFractionStr;
				else if (WholeNumber == 0)
					return $"{Numerator}/{Denominator}" + GetUnitsDisplayStr();
				return $"{WholeNumber} {Numerator}/{Denominator}" + GetUnitsDisplayStr();
			}
		}
		public string DisplayDecimalStr
		{
			get
			{
				if (Type == NumberType.Decimal)
					return Value + GetUnitsDisplayStr();
				else if (Type == NumberType.Fraction)
					return ((decimal)WholeNumber + (decimal)Numerator / (decimal)Denominator) + GetUnitsDisplayStr();
				return "Unknown type!!!";
			}
		}

		/// <summary>
		/// Simplifies the fraction...
		/// </summary>
		public void MakeFractionProper()
		{
			int savedSign = Sign;
			Numerator = Math.Abs(Numerator);
			WholeNumber = Math.Abs(WholeNumber);
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
			if (savedSign == -1)
			{
				Numerator *= -1;
				WholeNumber *= -1;
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
			SuperNumber result;
			
			if (Type == NumberType.Decimal)
				result = new SuperNumber(-Value);
			else
				result = new SuperNumber(-WholeNumber, -Numerator, Denominator);

			result.CopyUnitsFrom(this);
			return result;
		}

		void CreateReciprocalUnits()
		{
			foreach (UnitPower unitPower in unitPowers)
			{
				unitPower.Power *= -1;
			}
		}

		public SuperNumber CreateReciprocal()
		{
			// TODO: We need to invert the units, which means multiply their powers by -1.

			SuperNumber result = null;

			if (Type == NumberType.Decimal)
				if (Value == 0)
					throw new Exception("Cannot take the Reciprocal of zero!");
				else
					result = new SuperNumber(1 / Value);

			if (result == null)
			{
				SuperNumber improperFraction = CreateImproper();
				int newNumerator = improperFraction.Denominator;
				int newDenominator = improperFraction.Numerator;
				if (newDenominator < 0)
				{
					newDenominator *= -1;
					newNumerator *= -1;
				}
				if (newDenominator == 0)
					throw new Exception("Cannot take the Reciprocal of zero!");
				result = new SuperNumber(0, newNumerator, newDenominator);
			}

			result.CopyUnitsFrom(this);
			result.CreateReciprocalUnits();
			return result;
		}

		void AddUnitPower(string unit, double power)
		{
			UnitPower foundUnit = FindUnit(unit);
			if (foundUnit != null)
			{
				// We found the unit. It was here all along!!!
				foundUnit.Power += (decimal)power;
			}
			else // Need to add this...
			{
				unitPowers.Add(new UnitPower()
				{
					Unit = unit,
					Power = (decimal)power
				});
			}
		}

		public decimal GetPower(string unitName)
		{
			UnitPower findUnit = FindUnit(unitName);
			if (findUnit == null)
				return 0;
			return findUnit.Power;
		}

		private UnitPower FindUnit(string unit)
		{
			return unitPowers.FirstOrDefault(x => UnitNamesMatch(unit, x.Unit));
		}

		private static bool UnitNamesMatch(string unitName1, string unitName2)
		{
			string singularUnitName1 = UtilityMethods.MakeSingular(unitName1).ToLower();
			string singularUnitName2 = UtilityMethods.MakeSingular(unitName2).ToLower();
			return singularUnitName1 == singularUnitName2;
		}

		bool HasMatchingUnits(SuperNumber otherSuperNumber)
		{
			List<UnitPower> otherUnitPowers = otherSuperNumber.unitPowers;
			if (unitPowers.Count != otherUnitPowers.Count)
				return false;
			foreach (UnitPower unitPower in unitPowers)
			{
				UnitPower foundUnit = otherUnitPowers.FirstOrDefault(x => UnitNamesMatch(x.Unit, unitPower.Unit) && x.Power == unitPower.Power);
				if (foundUnit == null)
					return false;
			}

			return true;
		}

		void MultiplyUnits(SuperNumber superNumber)
		{
			foreach (UnitPower unitPower in superNumber.unitPowers)
			{
				UnitPower findUnit = FindUnit(unitPower.Unit);
				if (findUnit == null)
					unitPowers.Add(new UnitPower(unitPower));
				else
					findUnit.Power += unitPower.Power;
			}
		}

		void CleanUpAnyZeroPowerUnits()
		{
			for (int i = unitPowers.Count - 1; i >= 0; i--)  // We have to count backwards to remove elements from the list.
				if (unitPowers[i].Power == 0)
					unitPowers.RemoveAt(i);
		}
	}
}
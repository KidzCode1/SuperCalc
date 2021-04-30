﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using SuperCalcCore;

namespace SuperCalcTests
{
	[TestClass]
	public class FractionTests
	{
		SuperNumber oneSixth = new SuperNumber(0, 1, 6);
		SuperNumber twoTwelths = new SuperNumber(0, 2, 12);
		SuperNumber negOneTwoThirds = new SuperNumber(-1, 2, 3) /* <formula 3; -1 \frac{2}{3}>  */;

		SuperNumber negOneHalf = new SuperNumber(0, -1, 2) /* <formula 3; \frac{-1}{2}>  */;

		SuperNumber zero = new SuperNumber(0, 0, 0);
		SuperNumber one = new SuperNumber(1, 0, 0);
		SuperNumber negativeOne = new SuperNumber(-1, 0, 0);
		SuperNumber oneHalf = new SuperNumber(0, 1, 2);


		[TestMethod]
		public void TestFractionToDecimalConversion()
		{
			SuperNumber superNumber = new SuperNumber(1, 1, 2);//` <formula 3; 1 \frac{1}{2}>
			Assert.AreEqual(1.5m, superNumber);

			SuperNumber superNumber2 = new SuperNumber(5, 3, 4);//` <formula 3; 5 \frac{3}{4}>
			Assert.AreEqual(5.75m, superNumber2);

			SuperNumber superNumber3 = new SuperNumber(0, 1, 3); //` <formula 3; \frac{1}{3}>
			Assert.AreEqual(0.3333333333333333333333333333m, superNumber3);
		}

		// 0.3333333333333333333333333333m
		// 0.6666666666666666666666666666m
		// 0.9999999999999999999999999999m => 1

		[TestMethod]
		public void TestFractionalAdd()
		{
			const decimal oneThird = 0.3333333333333333333333333333m;
			Assert.AreEqual(1m, new SuperNumber(0, 1, 3) + new SuperNumber(0, 2, 3)); //` <formula 3; \frac{1}{3} + \frac{2}{3}>

			Assert.AreEqual(100.5m, new SuperNumber(0, 400, 4) + new SuperNumber(0, 7, 14)); //` <formula 3; \frac{400}{4} + \frac{7}{14}>


			Assert.AreEqual(-oneThird, new SuperNumber(0, 1, 3) + new SuperNumber(0, -2, 3)); //` <formula 3; \frac{1}{3} + \frac{-2}{3}>



			SuperNumber result = oneSixth + twoTwelths; //` <formula 3; \frac{1}{6} + \frac{2}{12}>
			// Test that it simplifies after the add:
			Assert.AreEqual(1, result.Numerator);
			Assert.AreEqual(3, result.Denominator);
			Assert.AreEqual(0, result.WholeNumber);
		}

		[TestMethod]
		public void MatchesZero()
		{
			Assert.AreEqual(zero, new SuperNumber(0, 0, 36));
			Assert.AreEqual(zero, new SuperNumber(0, 0, 0));
			Assert.AreEqual(0, (decimal)new SuperNumber(0, 0, 1));
			Assert.AreEqual(0, (decimal)new SuperNumber(0, 0, 0));
		}

		[TestMethod]
		public void TestMultiplication()
		{
			Assert.AreEqual(new SuperNumber(0, 1, 36) /* <formula 3; \frac{1}{36}>  */, oneSixth * twoTwelths);
			Assert.AreEqual(zero, twoTwelths * zero);
			Assert.AreEqual(new SuperNumber(0, 5, 6).Value /* <formula 3; \frac{5}{6}>  */, (negOneTwoThirds * negOneHalf).Value);

		}

		[TestMethod]
		public void TestDivide()
		{
			Assert.AreEqual(0.6m, 1 / new SuperNumber(1, 2, 3));
		}

		[TestMethod]
		public void TestInvert()
		{
			SuperNumber thirtySix = new SuperNumber(0, 36, 1);
			Assert.AreEqual(new SuperNumber(0, 1, 36) /* <formula 3; \frac{1}{36}>  */, thirtySix.CreateReciprocal());
		}

		[TestMethod]
		public void TestSign()
		{
			Assert.AreEqual(-1, negOneTwoThirds.Sign);
			Assert.AreEqual(-1, negOneHalf.Sign);

			Assert.AreEqual(0, zero.Sign);

			Assert.AreEqual(1, one.Sign);

			Assert.AreEqual(-1, negativeOne.Sign);

			Assert.AreEqual(1, oneHalf.Sign);
		}

		[TestMethod]
		public void CheckValueConversion()
		{
			SuperNumber supNum = new SuperNumber(-1, 1, 2) /* <formula 3; -1 \frac{1}{2}>  */;
			Assert.AreEqual(-1.5m, supNum);
		}

		[TestMethod]
		public void TestFractionalAddImproper()
		{
			SuperNumber superNumber1 = new SuperNumber(0, 4, 3);
			SuperNumber superNumber2 = new SuperNumber(0, 2, 6);
			SuperNumber superNumber3 = superNumber1 +  superNumber2;
			Assert.AreEqual(new SuperNumber(1, 2, 3), superNumber3);
		}

		[TestMethod]
		public void TestFractionalSubtractImproper()
		{
			SuperNumber superNumber1 = new SuperNumber(0, 4, 3);
			SuperNumber superNumber2 = new SuperNumber(0, 2, 6);
			SuperNumber superNumber3 = superNumber1 - superNumber2;
			Assert.AreEqual(1, (decimal)superNumber3);
		}

		[TestMethod]
		public void TestNegativeSubtraction()
		{
			SuperNumber superNumber1 = new SuperNumber(0, 1, 3);
			SuperNumber superNumber2 = new SuperNumber(1, 0, 1);
			SuperNumber superNumber3 = superNumber1 - superNumber2;
			Assert.AreEqual(new SuperNumber(0, -2, 3), superNumber3);
		}

		[TestMethod]
		public void TestEquivalentFractions()
		{
			SuperNumber superNumber1 = new SuperNumber(0, 2, 3);
			SuperNumber superNumber2 = new SuperNumber(0, 4, 6);
			Assert.IsTrue(superNumber1.AreFractionsEquivalent(superNumber2));
		}

		[TestMethod]
		public void TestMakeProperFraction()
		{
			SuperNumber superNumber = new SuperNumber(0, 4, 6);
			Assert.AreEqual(new SuperNumber(0, 4, 6), superNumber);
			superNumber.MakeFractionProper();
			Assert.AreEqual(new SuperNumber(0, 2, 3), superNumber);
		}

		[TestMethod]
		public void TestMakeNegative()
		{
			SuperNumber superNumber = new SuperNumber(0, 4, 6);
			SuperNumber negSuperNumber = superNumber.CreateNegative();
			Assert.AreEqual(new SuperNumber(0, -4, 6), negSuperNumber);
		}



		[TestMethod]
		public void TestMakeProperFraction2()
		{
			SuperNumber superNumber = new SuperNumber(0, 8, 12);
			superNumber.MakeFractionProper();
			Assert.AreEqual(new SuperNumber(0, 2, 3), superNumber);
		}

		[TestMethod]
		public void  TestMakeProperFraction3()
		{
			SuperNumber superNumber = new SuperNumber(0, 4, 3);
			superNumber.MakeFractionProper();
			Assert.AreEqual(new SuperNumber(1, 1, 3), superNumber);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void BadData()
		{
			SuperNumber badNegOneTwoThirds = new SuperNumber(1, -2, 3) /* <formula 3; 1 \frac{-2}{3}>  */;

		}
	}
}



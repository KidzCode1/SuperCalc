using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using SuperCalcCore;

namespace SuperCalcTests
{
	[TestClass]
	public class FractionTests
	{
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
    	SuperNumber superNumber1 = new SuperNumber(0, 1, 3);
			SuperNumber superNumber2 = new SuperNumber(0, 2, 3);
			SuperNumber superNumber3 = superNumber1 + superNumber2;
			Assert.AreEqual(1m, superNumber3);
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
	}
}

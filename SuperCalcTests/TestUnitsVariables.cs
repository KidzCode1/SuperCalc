using Microsoft.VisualStudio.TestTools.UnitTesting;
using SuperCalcCore;
using System;

namespace SuperCalcTests
{
	[TestClass]
	public class TestUnitsVariables
	{
		private TestContext testContextInstance;

		/// <summary>
		///Gets or sets the test context which provides
		///information about and functionality for the current test run.
		///</summary>
		public TestContext TestContext
		{
			get
			{
				return testContextInstance;
			}
			set
			{
				testContextInstance = value;
			}
		}

		[TestMethod]
		public void TestUnitsPowers1()
		{
			// TODO: Support meters/second (dividing units) as a valid unit
			// exponents: ⁰¹²³⁴⁵⁶⁷⁸⁹ˉ
			Assert.AreEqual("3m⁴", "1 1/2 m³ * 2m".ToNum());
		}

		[TestMethod]
		public void TestDecimalUnits()
		{
			Assert.AreEqual("0.5m", "0.5m".ToNum());
		}

		[TestMethod]
		public void TestUnitsPowers2()
		{
			Assert.AreEqual("8x²", "4x * 2x".ToNum());
			Assert.AreEqual("-1/3 v", "-2/3v + 1/3v".ToNum());
		}

		[TestMethod]
		public void TestReciprocal()
		{
			Assert.AreEqual("1/8 xˉ⁹", "8x⁹".ToNum().CreateReciprocal());
			//Assert.AreEqual("1/8 1/x⁹", "8x⁹".ToNum().CreateReciprocal());
		}

		[TestMethod]
		public void TestLikeUnitDivision()
		{
			Assert.AreEqual("14", "7 birds / 1/2 bird".ToNum());
		}

		[TestMethod]
		public void TestUnitDivision()
		{
			Assert.AreEqual("21 birds", "7 birds / 1/3".ToNum());
		}

		[TestMethod]
		public void TestSimpleLikeUnits()
		{
			Assert.AreEqual("1 bird", "1 bird".ToNum().DisplayStr);
			Assert.AreEqual("1m²", "1m²".ToNum().DisplayStr);
		}

		[TestMethod]
		public void TestLikeUnits()
		{
			Assert.AreEqual("3m", "1m + 2m".ToNum());
			Assert.AreEqual("3m²", "1m² + 2m²".ToNum());
			Assert.AreEqual("3 bird²", "1 bird² + 2 bird²".ToNum());
			Assert.AreEqual("21 birds", "7 birds / 1/3".ToNum());
			//Assert.AreEqual("1 bird + 2 snakes", "1 bird + 2 snakes".ToNum());
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void TestUnlikeUnits()
		{
			//! Should throw the exception:
			"1m + 2s".ToNum();
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void TestUnlikeUnits2()
		{
			//! Should throw the exception:
			Assert.AreEqual("3m", "1m + 2m²".ToNum());
		}
	}
}
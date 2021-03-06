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
		public void TestSingleVariables()
		{
			SuperNumber.ClearAllKnownUnitVars();
			SuperNumber.AddUnitVar("m", UnitVarType.Unit);
			SuperNumber.AddUnitVar("x", UnitVarType.Variable);
			SuperNumber.AddUnitVar("cm", UnitVarType.Unit, UnitFormat.SuppressLeadingSpace);

			Assert.AreEqual("2x cm²", "x cm * 2 cm".ToNum());
			Assert.AreEqual("x²cm²", "x cm * x cm".ToNum());
			Assert.AreEqual("2x²", "2x * x".ToNum());
			Assert.AreEqual("2x²", "x * 2x".ToNum());
			Assert.AreEqual("2x²", "1/2 x * 4x".ToNum());
			Assert.AreEqual("x²", "x * x".ToNum());
			Assert.AreEqual("x²", "2x * 1/2 x".ToNum());
			Assert.AreEqual("1", "x / x".ToNum());
			Assert.AreEqual("2x", "x + x".ToNum());
			Assert.AreEqual("0", "x - x".ToNum());
			
			Assert.AreEqual("0", "1x - 1x".ToNum());
			Assert.AreEqual("0m", "1m - 1m".ToNum());

			Assert.AreEqual("x²", "x * x".ToNum());
			Assert.AreEqual("0", "x - x".ToNum());
			Assert.AreEqual("0m", "x m - x m".ToNum());
			Assert.AreEqual("1cm²", "1cm * 1cm".ToNum());
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
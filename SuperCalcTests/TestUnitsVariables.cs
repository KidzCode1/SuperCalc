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
		public void TestUnitsPowers2()
		{
			Assert.AreEqual("8x²", "4x * 2x".ToNum());
		}

		[TestMethod]
		public void TestLikeUnitDivision()
		{
			Assert.AreEqual("14", "7 birds / 1/2 bird".ToNum());
			Assert.AreEqual("21 birds", "7 birds / 1/3".ToNum());
		}

		[TestMethod]
		public void TestLikeUnits()
		{
			Assert.AreEqual("1 bird + 2 snakes", "1 bird + 2 snakes".ToNum());
			Assert.AreEqual("3 bird²", "1 bird² + 2 bird²".ToNum());
			Assert.AreEqual("21 birds", "7 birds / 1/3".ToNum());
		}


	}
}



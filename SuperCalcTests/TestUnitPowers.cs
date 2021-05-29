using Microsoft.VisualStudio.TestTools.UnitTesting;
using SuperCalcCore;
using System;

namespace SuperCalcTests
{
	[TestClass]
	public class TestUnitPowers
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
		public void TestMetersPerSecond()
		{
			Assert.AreEqual("2x", "2 * x".ToNum());
			Assert.AreEqual("8m/sec", "4 m/sec * 2".ToNum());
			Assert.AreEqual("1/2 m/sec", "1/2 m / 1 sec".ToNum());  // 0.5 m secˉ¹
			Assert.AreEqual("5 secˉ¹", "5 / 1 sec".ToNum());
			Assert.AreEqual("8 birds", "4 birds * 2".ToNum());
		}

		[TestMethod]
		public void TestMatchingUnitsWithPowers()
		{
			Assert.AreEqual("-3/56 x³", "4/7 x³ - 5/8 x³".ToNum());
		}

		[TestMethod]
		public void Test()
		{
			Assert.AreEqual(3, "1 m^3".ToNum().GetPower("m"));
			Assert.AreEqual(-123, "1 meters^-123".ToNum().GetPower("meter"));
			Assert.AreEqual("1.3504273504273504273504273501", "8 7/9 / 6.5".ToNum());
		}
		[TestMethod]
		public void TestFindUnitPowers()
		{
			FindUnitPower findUnitPower = FindUnitPower.Create("m");
			Assert.AreEqual(1, findUnitPower.power);
			Assert.AreEqual("m", findUnitPower.unit);
		}
	}
}



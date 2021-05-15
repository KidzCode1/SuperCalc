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
		public void Test()
		{
			Assert.AreEqual(3, "1 m^3".ToNum().GetPower("m"));
			Assert.AreEqual(-123, "1 meters^-123".ToNum().GetPower("meter"));
		}
	}
}



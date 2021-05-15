using Microsoft.VisualStudio.TestTools.UnitTesting;
using SuperCalcCore;
using System;

namespace SuperCalcTests
{
	[TestClass]
	public class TestMixedNumberFractions
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
		public void TestMixedNumWithDecimal()
		{
			Assert.AreEqual("14", "6.5 8/2 + 7/2".ToNum());
		}
	}
}



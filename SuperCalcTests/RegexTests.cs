using Microsoft.VisualStudio.TestTools.UnitTesting;
using SuperCalcCore;
using System;

namespace SuperCalcTests
{
	[TestClass]
	public class RegexTests
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
		public void TestFindNumberUnits()
		{
			FindNumberUnit findNumberUnit = FindNumberUnit.Create("1 1/2m");
			Assert.AreEqual("1 1/2", findNumberUnit.number);
			Assert.AreEqual("m", findNumberUnit.units);

			findNumberUnit = FindNumberUnit.Create("2m³");
			Assert.AreEqual("2", findNumberUnit.number);
			Assert.AreEqual("m³", findNumberUnit.units);

			findNumberUnit = FindNumberUnit.Create("1 bird");
			Assert.AreEqual("1", findNumberUnit.number);
			Assert.AreEqual("bird", findNumberUnit.units);

			findNumberUnit = FindNumberUnit.Create("1 m/sec");
			Assert.AreEqual("1", findNumberUnit.number);
			Assert.AreEqual("m/sec", findNumberUnit.units);
		}
	}
}



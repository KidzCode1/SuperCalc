using Microsoft.VisualStudio.TestTools.UnitTesting;
using SuperCalcCore;
using System;

namespace SuperCalcTests
{
	[TestClass]
	public class UtilityMethodTests
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
		public void TestSingular()
		{
			Assert.AreEqual("meter", UtilityMethods.MakeSingular("meters"));
		}

		[TestMethod]
		public void TestOctopuses()
		{
			Assert.AreEqual("octopus", UtilityMethods.MakeSingular("octopuses"));
		}

		[TestMethod]
		public void TestDaisies()
		{
			Assert.AreEqual("daisy", UtilityMethods.MakeSingular("daisies"));
			Assert.AreEqual("pansy", UtilityMethods.MakeSingular("pansies"));
			Assert.AreEqual("patty", UtilityMethods.MakeSingular("patties"));
		}

		[TestMethod]
		public void GollumTests()
		{
			Assert.AreEqual("hobbit", UtilityMethods.MakeSingular("hobbitses"));
		}
	}
}



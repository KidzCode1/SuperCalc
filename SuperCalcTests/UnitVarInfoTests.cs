using Microsoft.VisualStudio.TestTools.UnitTesting;
using SuperCalcCore;
using System;

namespace SuperCalcTests
{
	[TestClass]
	public class UnitVarInfoTests
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
			SuperNumber.AddUnitVar("meters", UnitVarType.Unit);
			UnitVarInfo unitVarInfo = SuperNumber.GetUnitVarInfo("meters");
			Assert.IsNotNull(unitVarInfo);
			Assert.AreEqual(UnitVarType.Unit, unitVarInfo.UnitVarType);

			// Now test the clear...
			SuperNumber.ClearAllKnownUnitVars();
			unitVarInfo = SuperNumber.GetUnitVarInfo("meters");
			Assert.IsNull(unitVarInfo);
		}
	}
}



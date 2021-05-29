using System;
using System.Linq;

namespace SuperCalcCore
{
	public class UnitPower
	{
		public string UnitVarName { get; set; }
		public decimal Power { get; set; }
		public UnitPower()
		{

		}

		public UnitPower(UnitPower unitPower)
		{
			UnitVarName = unitPower.UnitVarName;
			Power = unitPower.Power;
		}
	}
}
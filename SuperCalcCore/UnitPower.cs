using System;
using System.Linq;

namespace SuperCalcCore
{
	public class UnitPower
	{
		public string Unit { get; set; }
		public decimal Power { get; set; }
		public UnitPower()
		{

		}

		public UnitPower(UnitPower unitPower)
		{
			Unit = unitPower.Unit;
			Power = unitPower.Power;
		}
	}
}
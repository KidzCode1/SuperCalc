using System;
using System.Linq;

namespace SuperCalcCore
{
	public class UnitPower
	{
		public string Name { get; set; }
		public decimal Power { get; set; }
		public UnitPower()
		{

		}

		public UnitPower(UnitPower unitPower)
		{
			Name = unitPower.Name;
			Power = unitPower.Power;
		}
	}
}
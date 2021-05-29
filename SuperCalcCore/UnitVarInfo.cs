using System;
using System.Linq;

namespace SuperCalcCore
{
	public class UnitVarInfo
	{
		public string UnitName { get; set; }
		public UnitVarType UnitVarType { get; set; }
		public UnitFormat UnitFormat { get; set; }
		public UnitVarInfo(string unitName, UnitVarType unitVarType, UnitFormat unitFormat = UnitFormat.Normal)
		{
			UnitFormat = unitFormat;
			UnitVarType = unitVarType;
			UnitName = unitName;
		}
	}
}
using System;

namespace Weaver.Utils;

public enum WeightUnit {
	g, kg, T
}

public class Weight : UnitValue<int, WeightUnit> {

	public Weight() { }
	public Weight( int val, WeightUnit unit ) : base(val, unit){ }
	/// <summary>
	/// 
	/// </summary>
	/// <param name="u"></param>
	/// <param name="v"></param>
	/// <returns>u/v</returns>
	public override double Ratio( WeightUnit u, WeightUnit v ) {
		if(u == v)
			return 1;
		switch(u) {
			case WeightUnit.g:
			if(v == WeightUnit.kg)
				return 1e-3;
			return 1e-6;//Tonne
			case WeightUnit.kg:
			if(v == WeightUnit.g)
				return 1e3;
			return 1e-3;//Tonne
			case WeightUnit.T:
			if(v == WeightUnit.g)
				return 1e6;
			return 1e3;//kg
			default:
			return 0;
		}
	}
}
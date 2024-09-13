using System;

namespace Weaver.Utils;

/// <summary>
/// Units of distance.
/// The int value is n with "enum = 10^(n-1)"
/// </summary>
public enum DistanceUnit { mm=1,cm=2,dm=3,m=4,km=7 }

/// <summary>
/// A distance in space, with IRL Units.
/// </summary>
public class Distance : UnitValue<int, DistanceUnit> {
	public Distance() { }
	public Distance( int val, DistanceUnit unit ) : base(val, unit){ }
	public override double Ratio( DistanceUnit u, DistanceUnit v ) {
		return System.Math.Pow(10, u - v);
	}
}

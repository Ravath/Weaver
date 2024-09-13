using System;

namespace Weaver.Utils;

/// <summary>
/// A Value determined by an Unit.
/// </summary>
public interface IUnitValue {
	object Value { get; }
	object Unit { get; }
}

/// <summary>
/// A Value determined by an Unit.
/// </summary>
public class UnitValue<V, U> : IUnitValue,
		IComparable<UnitValue<V, U>>,
		IEquatable<UnitValue<V, U>>
	where V : struct 
	where U : struct
{
	public V Value { get; set; }
	public U Unit { get; set; }

	object IUnitValue.Value {
		get { return Value; }
	}

	object IUnitValue.Unit {
		get { return Unit; }
	}

	public UnitValue() { }
	public UnitValue(V val, U unit) {
		Value = val;
		Unit = unit;
	}
	/// <summary>
	/// Returns the Ratio U/V
	/// </summary>
	/// <param name="u"></param>
	/// <param name="v"></param>
	/// <returns>U/V</returns>
	public virtual double Ratio(U u, U v)
	{
		int iu = Array.IndexOf(Enum.GetValues(u.GetType()), u);
		int iv = Array.IndexOf(Enum.GetValues(v.GetType()), v);

		return System.Math.Pow(10, iu) / System.Math.Pow(10, iv);
	}
	/// <summary>
	/// Si supérieur à 0 : other est plus petit
	/// Si inférieur à 0 : other est plus grand
	/// </summary>
	/// <param name="other"></param>
	/// <returns></returns>
	public int CompareTo( UnitValue<V, U>? other ) {
		if(other == null)
			return 1;
		double d = Convert.ToDouble(other.Value) / Convert.ToDouble(Value) * Ratio(other.Unit, Unit);
		if(d < 1)
			return 1;
		if(d > 1)
			return -1;
		return 0;
	}

	public bool Equals( UnitValue<V, U>? other ) {
		if(base.Equals(other)) { return true; }
		if(other == null)
			return false;
		return CompareTo(other) == 0;
	}
	public override bool Equals( object? obj ) {
		if(obj is UnitValue<V, U> cp)
		{
			return CompareTo(cp) == 0;
		}
		return false;
	}

	public static bool operator >( UnitValue<V, U> u, UnitValue<V, U> v ) {
		return u.CompareTo(v) > 0;
	}

	public static bool operator <( UnitValue<V, U> u, UnitValue<V, U> v ) {
		return u.CompareTo(v) < 0;
	}

	public static bool operator >=( UnitValue<V, U> u, UnitValue<V, U> v ) {
		return !(u.CompareTo(v) < 0);
	}

	public static bool operator <=( UnitValue<V, U> u, UnitValue<V, U> v ) {
		return !(u.CompareTo(v) > 0);
	}

	public static bool operator ==( UnitValue<V, U>? u, UnitValue<V, U>? v )
	{
		if (u is null)
		{
			return v is null;
		}
		return u.Equals(v);
	}

	public static bool operator !=(UnitValue<V, U>? u, UnitValue<V, U>? v) => !(u == v);

	public override int GetHashCode() {
		return base.GetHashCode();
	}

	public override string ToString() {
		return Value + " " + Unit;
	}

}
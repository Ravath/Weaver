using System;

namespace Weaver.Utils;

public static class Operation
{
	public static int RangeThreshold(int value, int min, int max)
	{
		return System.Math.Min(System.Math.Max(min, value), max);
	}
	public static int MinThreshold(int value, int min)
	{
		return (value < min) ? min : value;
	}
	public static int MaxThreshold(int value, int max)
	{
		return (value > max) ? max : value;
	}
}
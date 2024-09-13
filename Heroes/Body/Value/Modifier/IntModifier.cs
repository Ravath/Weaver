using System;

namespace Weaver.Heroes.Body.Value.Modifier;


public static class IntModifier
{
    /// <summary>
    /// Implementation of the default computation method.
    /// Just does a sum of the value and the modificator.
    /// </summary>
    /// <param name="mod">The modifier.</param>
    /// <param name="val">The value to modify.</param>
    /// <returns>The final value.</returns>
    public static int DefaultSum(int mod, int val)
    {
        return mod + val;
    }

    /// <summary>
    /// Implementation of a high limit maximum threshold.
    /// </summary>
    /// <typeparam name="T">Type of the threshold.</typeparam>
    /// <param name="limit">Maximum value.</param>
    /// <param name="val">The value to check.</param>
    /// <returns>Limit if val>limit.</returns>
    private static T ActuateUpperLimit<T>(T limit, T val) where T : IComparable
    {
        int comparison = limit.CompareTo(val);
        if(comparison == 1)
        {
            return val;
        }
        return limit;
    }

    /// <summary>
    /// Implementation of a low limit minimum threshold.
    /// </summary>
    /// <typeparam name="T">Type of the threshold.</typeparam>
    /// <param name="limit">Minimum value.</param>
    /// <param name="val">The value to check.</param>
    /// <returns>Limit if val<limit.</returns>
    private static T ActuateLowerLimit<T>(T limit, T val) where T : IComparable
    {
        int comparison = limit.CompareTo(val);
        if(comparison == -1)
        {
            return val;
        }
        return limit;
    }

    public static RefModifier<int> IntSumModifier(IValue<int> baseValue, int priority = 2)
    {
        return new RefModifier<int>(baseValue, DefaultSum, priority);
    }

    public static RefModifier<T> LimiterModifier<T>(IValue<T> baseValue, bool upperLimiter = true, int priority = 4) where T : IComparable
    {
        if(upperLimiter)
            return new RefModifier<T>(baseValue, ActuateUpperLimit, priority);
        return new RefModifier<T>(baseValue, ActuateLowerLimit, priority);

    }
}
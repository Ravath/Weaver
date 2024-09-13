using System;

namespace Weaver.Utils;

/// <summary>
/// Units of time.
/// </summary>
public enum TimeUnit {
    s, min, hour, day, week, month, year, century, millennium
}

/// <summary>
/// A period of time, with IRL Units.
/// </summary>
public class TimePeriod : UnitValue<int, TimeUnit> {
    public TimePeriod() { }
    public TimePeriod( int val, TimeUnit unit ) : base(val, unit){ }
    /// <summary>
    /// Uses: 1 month = 3O days, and 1 month = 30/7 weeks
    /// </summary>
    /// <param name="u"></param>
    /// <param name="v"></param>
    /// <returns>u/v</returns>
    public override double Ratio( TimeUnit u, TimeUnit v ) {
        if(u == v) return 1;
        bool umin = u < v;
        TimeUnit min, max;
        double ratio = 1;
        if(umin) {
            min = u;
            max = v;
        } else {
            min = v;
            max = u;
        } while(min != max) {
            switch(min) {
                case TimeUnit.s:
                min = TimeUnit.min;
                ratio *= 60;
                break;
                case TimeUnit.min:
                min = TimeUnit.hour;
                ratio *= 60;
                break;
                case TimeUnit.hour:
                min = TimeUnit.day;
                ratio *= 24;
                break;
                case TimeUnit.day:
                min = TimeUnit.week;
                ratio *= 7;
                break;
                case TimeUnit.week:
                min = TimeUnit.month;
                ratio *= 30d/7d;
                break;
                case TimeUnit.month:
                min = TimeUnit.year;
                ratio *= 12;
                break;
                case TimeUnit.year:
                min = TimeUnit.century;
                ratio *= 100;
                break;
                case TimeUnit.century:
                min = TimeUnit.millennium;
                ratio *= 10;
                break;
                case TimeUnit.millennium:
                return 0;//should not append
                default:
                return 0;//should not append
            }
        }

        return umin ? 1 / ratio : ratio;
    }
}
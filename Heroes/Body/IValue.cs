using System;

namespace Weaver.Heroes.Body;

/// <summary>
/// The given value has changed. 
/// </summary>
/// <typeparam name="T"></typeparam>
/// <param name="val"></param>
public delegate void ValueChangedHandler<T>( IValue<T> sender );

/// <summary>
/// Interface to a managed value.
/// The value has a name, a base value and a modified value.
/// OnValueChanged is fired when any of these is changed.
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IValue<T> {
    /// <summary>
    /// Real value.
    /// </summary>
    T Value { get; }
    /// <summary>
    /// Name of the value.
    /// </summary>
    string Label { get; }
    /// <summary>
    /// Fired when the Value has changed.
    /// </summary>
    public event ValueChangedHandler<T> OnValueChanged;
}
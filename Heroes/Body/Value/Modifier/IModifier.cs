using System;

namespace Weaver.Heroes.Body.Value.Modifier;


/// <summary>
/// The interface of a modifier.
/// Modifies a value, generally by a computation involving at least another value.
/// </summary>
/// <typeparam name="T">Type of the value to modify.</typeparam>
public interface IModifier<T>
{
    T ModifierValue { get; }
    int Priority { get; }
    T Modify(T val);
    event ValueChangedHandler<T> OnValueChanged;
}
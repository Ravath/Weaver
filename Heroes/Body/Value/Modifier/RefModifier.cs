using System;
using Weaver.Heroes.Body.Value;
using Weaver.Heroes.Body.Value.Modifier;

namespace Weaver.Heroes.Body.Value.Modifier;


/// <summary>
/// Implementation of the Modifier computation.
/// </summary>
/// <typeparam name="T"></typeparam>
/// <param name="mod"></param>
/// <param name="val"></param>
/// <returns></returns>
public delegate T DelegateModifierHandler<T>(T mod, T val);

/// <summary>
/// The default Modifier implementation 
/// for Value modification mechanism.
/// </summary>
public class RefModifier<T> : IModifier<T>
{
    public event ValueChangedHandler<T> OnValueChanged
    {
        add { ModifierIValue.OnValueChanged += value; }
        remove { ModifierIValue.OnValueChanged -= value; }
    }

    /// <summary>
    /// The IValue of the Value used for computation.
    /// </summary>
    public IValue<T> ModifierIValue { get; private set; }

    /// <summary>
    /// The value used for computation.
    /// </summary>
    public T ModifierValue{ get { return ModifierIValue.Value; } }

    /// <summary>
    /// Delegate for the computation of the modification.
    /// </summary>
    public DelegateModifierHandler<T> Computation { get; internal set; }

    /// <summary>
    /// The priority of the modifier, as 0 for the higher priority.
    /// See 
    /// </summary>
    public int Priority { get; private set; }

    public RefModifier(IValue<T> monitoredValue, DelegateModifierHandler<T> computationMethod, int priority = 2)
    {
        this.ModifierIValue = monitoredValue;
        this.Priority = priority;
        this.Computation = computationMethod;
    }

    /// <summary>
    /// Applies the modification on the given value.
    /// </summary>
    /// <param name="value">The value to modify.</param>
    /// <returns>The final value.</returns>
    public T Modify(T value)
    {
        return Computation(ModifierValue, value);
    }
}
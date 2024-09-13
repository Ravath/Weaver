using System;

namespace Weaver.Heroes.Body.Value.Modifier;

/// <summary>
/// A module or alike able to manage and be modified by IModifiers.
/// </summary>
/// <typeparam name="T">The modified value type.</typeparam>
public interface IModifiable<T> {
    /// <summary>
    /// Value before modifications.
    /// </summary>
    T BaseValue { get; }
    /// <summary>
    /// Final value after modifications.
    /// </summary>
    T ModifiedValue { get; }
    /// <summary>
    /// The modifiers of the base value.
    /// </summary>
    IEnumerable<IModifier<T>> Modifiers { get; }
    void AddModifier( IModifier<T> mod );
    void RemoveModifier( IModifier<T> mod );
}

public interface IModifiableValue<T> : IValue<T>, IModifiable<T>, IBaseValue<T>
{

}
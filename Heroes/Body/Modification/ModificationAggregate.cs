using System;

namespace Weaver.Heroes.Body.Modification;

/// <summary>
/// <para>
/// Applies a collection of Modifications on a Module.
/// </para>
/// <para>
/// Great use for race, traits, classes and other implementations
/// involving multiple modifications on a character.
/// </para>
/// </summary>
/// <typeparam name="M"></typeparam>
public class ModificationAggregate<M> : IModuleModification<M>
{
    /// <summary>
    /// The stored modifiers.
    /// </summary>
    private List<IModuleModification<M>> _modifiers = new();

    /// <summary>
    /// A delegate for personalising adding conditions.
    /// </summary>
    public CheckCondition<M>? AddCheck;

    public ModificationAggregate(params IModuleModification<M>[] modifiers)
    {
        _modifiers.AddRange(modifiers);
    }

    public bool CanApply(M target)
    {
        foreach(IModuleModification<M> modifier in _modifiers)
        {
            if(!modifier.CanApply(target))
                return false;
        }
        if(AddCheck != null)
            return AddCheck(target);
        return true;
    }

    public void ApplyModification(M target)
    {
        foreach(IModuleModification<M> modifier in _modifiers)
        {
            modifier.ApplyModification(target);
        }
    }

    public void RemoveModification(M target)
    {
        foreach(IModuleModification<M> modifier in _modifiers)
        {
            modifier.RemoveModification(target);
        }
    }
}

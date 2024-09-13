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
public class ModificationAggregate<M> : IModuleModification<M> where M : Module
{
    /// <summary>
    /// The stored modifiers.
    /// </summary>
    private List<IModuleModification<M>> _modifiers = new();

    /// <summary>
    /// A delegate for personalising adding conditions.
    /// </summary>
    public CheckCondition<Module>? AddCheck;

    public ModificationAggregate(params IModuleModification<M>[] modifiers)
    {
        _modifiers.AddRange(modifiers);
    }

    public bool CanApply(M module)
    {
        foreach(IModuleModification<M> modifier in _modifiers)
        {
            if(!modifier.CanApply(module))
                return false;
        }
        if(AddCheck != null)
            return AddCheck(module);
        return true;
    }

    public void ApplyModification(M module)
    {
        foreach(IModuleModification<M> modifier in _modifiers)
        {
            modifier.ApplyModification(module);
        }
    }

    public void RemoveModification(M module)
    {
        foreach(IModuleModification<M> modifier in _modifiers)
        {
            modifier.RemoveModification(module);
        }
    }
}

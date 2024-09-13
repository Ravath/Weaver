using System;

namespace Weaver.Heroes.Body.Modification;


/// <summary>
/// <para>
/// A module collection managing modifications.
/// The modules are applied on registration.
/// </para>
/// <para>
/// Great use for race, traits, and other managements
/// involving a modifications on the character.
/// </para>
/// </summary>
/// <typeparam name="M"></typeparam>
public class ModificationCollectionModule<M> : CollectionModule<IModuleModification<M>> where M : Module
{
    /// <summary>
    /// The module modified by the applied modifications.
    /// </summary>
    private M _modified;

    public ModificationCollectionModule(string name, M modified) : base(name)
    {
        _modified = modified;
    }

    public override bool CanAdd(IModuleModification<M> item)
    {
        return base.CanAdd(item) && item.CanApply(_modified);
    }

    public override void Add(IModuleModification<M> item)
    {
        item.ApplyModification(_modified);
        base.Add(item);
    }

    public override void Remove(IModuleModification<M> item)
    {
        item.RemoveModification(_modified);
        base.Remove(item);
    }
}

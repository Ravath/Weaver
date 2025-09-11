using System;

namespace Weaver.Heroes.Body.Modification;

/// <summary>
/// <para>
/// Applies Modifications on a module.
/// </para>
/// <para>
/// Great use for race, attribute bonuses, traits,
/// and other implementations
/// involving a modifications on the character.
/// Use simple implementations for single traits
/// (ie : attribute bonuses, vision type, ...), 
/// and a collection of simple implementations for 
/// more important modifications, such as race, classes and such.
/// </para>
/// </summary>
/// <typeparam name="M"></typeparam>
public interface IModuleModification<M>
{
    bool CanApply(M target);
    void ApplyModification(M target);
    void RemoveModification(M target);
}

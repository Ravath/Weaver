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
public interface IModuleModification<M> where M : Module
{
    bool CanApply(M module);
    void ApplyModification(M module);
    void RemoveModification(M module);
}

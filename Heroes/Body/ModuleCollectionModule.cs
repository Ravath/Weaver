using System;

namespace Weaver.Heroes.Body;

/// <summary>
/// A module storing a collection of Modules.
/// </summary>
/// <typeparam name="T"></typeparam>
public class ModuleCollectionModule<T> : CollectionModule<T> where T : Module
{
    public ModuleCollectionModule(string moduleName) : base(moduleName)
    {
    }

    public override void Add(T item)
    {
        Register(item);
        base.Add(item);
    }

    public override void Remove(T item)
    {
        Unregister(item);
        base.Remove(item);
    }

    /// <summary>
    /// Check if the module contains a direct child with the given name.
    /// </summary>
    /// <param name="moduleName">A module name.</param>
    /// <returns>True if found.</returns>
    public override bool HasRegistered(string moduleName)
    {
        if(base.HasRegistered(moduleName))
        {
            return true;
        }
        else if(int.TryParse(moduleName, out var index))
        {
            return index < _collection.Count;
        }
        return false;
    }

    /// <summary>
    /// Get the direct child module with the given name.
    /// </summary>
    /// <param name="moduleName">A module name.</param>
    /// <returns>A module.</returns>
    public override Module GetRegistered(string moduleName)
    {
        if(base.HasRegistered(moduleName))
        {
            return base.GetRegistered(moduleName);
        }
        else if(int.TryParse(moduleName, out var index))
        {
            if(index < _collection.Count)
                return _collection[index];
        }
#pragma warning disable CS8603 // Possible null reference return.
        return null;
#pragma warning restore CS8603 // Possible null reference return.
    }
}

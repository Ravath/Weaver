using System;
using System.Collections;

namespace Weaver.Heroes.Body;

public delegate bool CheckCondition<T>(T item);
public delegate void ModuleChanged<T>(T module);

/// <summary>
/// A module storing a collection of items.
/// </summary>
/// <typeparam name="T"></typeparam>
public class CollectionModule<T> : Module
{
    protected List<T> _collection = new();

    /// <summary>
    /// A delegate for personalising adding conditions.
    /// </summary>
    public CheckCondition<T>? AddCheck;
    /// <summary>
    /// The items of the collection.
    /// </summary>
    public IEnumerable<T> Collection{ get { return _collection; } }
    /// <summary>
    /// Fired when a item is added or removed from the collection.
    /// </summary>
    public ModuleChanged<CollectionModule<T>>? OnCollectionChanged;

    public CollectionModule(string name) : base(name)
    {

    }

    public virtual bool CanAdd(T item)
    {
        if(AddCheck == null)
        {
            return true;
        }
        return AddCheck(item);
    }

    public virtual void Add(T item)
    {
        _collection.Add(item);
        OnCollectionChanged?.Invoke(this);
    }

    public virtual void Remove(T item)
    {
        _collection.Remove(item);
        OnCollectionChanged?.Invoke(this);
    }
}

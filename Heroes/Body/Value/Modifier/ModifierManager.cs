using System;
using System.Text.Json.Serialization;

namespace Weaver.Heroes.Body.Value.Modifier;

public interface IBaseValue<T> : IValue<T> {
    T BaseValue{ get; }
}

public class ModifierManager<T> : IModifiable<T>
{
    /// <summary>
    /// Fired when the a modifier is changed, added or removed.
    /// </summary>
    public event ValueChangedHandler<T>? OnModificationsChanged;

    private IBaseValue<T> _managedValue;

    public T BaseValue {
        get {
            return _managedValue.BaseValue;
        }
    }

    public T ModifiedValue {
        get {
            return ApplyModifiers(BaseValue);
        }
    }
    
    private SortedDictionary<int, List<IModifier<T>>> _modifiers = new();

    /// <summary>
    /// The list of the value modifiers.
    /// </summary>
    [JsonIgnore]
    public IEnumerable<IModifier<T>> Modifiers {
        get { return _modifiers.SelectMany(k=>k.Value); }
    }

    internal ModifierManager(IBaseValue<T> managedValue)
    {
        _managedValue = managedValue;
    }

    internal T ApplyModifiers(T val)
    {
        // Apply modifiers starting from priority 0 and increasing.
        foreach (int priority in _modifiers.Keys)
        {
            // Same priority modifiers are applied in non specific order.
            foreach(IModifier<T> modifier in _modifiers[priority])
            {
                val = modifier.Modify(val);
            }
        }
        return val;
    }

    public void AddModifier( IModifier<T> mod ) {
        if(!_modifiers.Keys.Contains(mod.Priority))
            _modifiers.Add(mod.Priority, new List<IModifier<T>>());
        _modifiers[mod.Priority].Add(mod);
        mod.OnValueChanged += ModifierChanged;
        OnModificationsChanged?.Invoke(_managedValue);
    }

    public void RemoveModifier( IModifier<T> mod ) {
        // Apply modifiers starting from priority 0 and increasing.
        foreach (int priority in _modifiers.Keys)
        {
            if(_modifiers[priority].Contains(mod))
                _modifiers[priority].Remove(mod);
        }
        mod.OnValueChanged -= ModifierChanged;
        OnModificationsChanged?.Invoke(_managedValue);
    }

    protected void ModifierChanged( IValue<T> mod ) {
        OnModificationsChanged?.Invoke(_managedValue);
    }
}

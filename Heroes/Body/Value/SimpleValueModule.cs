using System;

namespace Weaver.Heroes.Body.Value;


public class SimpleValueModule<T> : Module, IValue<T>
{
    /// <summary>
    /// Fired when the base value is changed or a modifier is changed, added or removed.
    /// </summary>
    public event ValueChangedHandler<T>? OnValueChanged;

    private T _value;

    /// <summary>
    /// The Module value without modifiers.
    /// </summary>
    public virtual T Value {
        get { return _value; }
        set {
            _value = value;
            OnValueChanged?.Invoke(this);
        }
    }

    /// <summary>
    /// The module name.
    /// </summary>
    public string Label { get { return this.ModuleName; } }

    public SimpleValueModule(string moduleName, T baseValue) : base(moduleName)
    {
        _value = baseValue;
    }

    public override string ToString() {
        return string.Format("{0}", Value);
    }
}
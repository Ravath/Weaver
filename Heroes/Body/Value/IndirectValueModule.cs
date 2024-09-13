using System;
using System.Text.Json.Serialization;
using Weaver.Heroes.Body.Value.Modifier;

namespace Weaver.Heroes.Body.Value;

public class IndirectValueModule<T> : Module, IBaseValue<T>
{
    /// <summary>
    /// Fired when the base value is changed or a modifier is changed, added or removed.
    /// </summary>
    public event ValueChangedHandler<T> OnValueChanged;

    private IValue<T> _value;
    private ModifierManager<T> _modifiers;

    /// <summary>
    /// The Module value without modifiers.
    /// </summary>
    public T BaseValue {
        get { return _value.Value; }
    }

    /// <summary>
    /// The Module value without modifiers.
    /// </summary>
    public virtual T Value {
        get { return _modifiers.ModifiedValue; }
    }

    /// <summary>
    /// The module value with modifiers.
    /// </summary>
    public T ModifiedValue {
        get
        {
            return _modifiers.ModifiedValue;
        }
    }

    /// <summary>
    /// The module name.
    /// </summary>
    public string Label { get { return this.ModuleName; } }

    /// <summary>
    /// The list of the value modifiers.
    /// </summary>
    [JsonIgnore]
    public IEnumerable<IModifier<T>> Modifiers => _modifiers.Modifiers;

    public IndirectValueModule(string moduleName, IValue<T> refValue) : base(moduleName)
    {
        _value = refValue;
        _modifiers = new(this);
        _value.OnValueChanged += OnRefValueChanged;
        _modifiers.OnModificationsChanged += OnModifChanged;
    }

    private void OnModifChanged(IValue<T> val)
    {
        OnValueChanged?.Invoke(this);
    }

    private void OnRefValueChanged(IValue<T> val)
    {
        OnValueChanged?.Invoke(this);
    }

    public void AddModifier(IModifier<T> mod)
    {
        _modifiers.AddModifier(mod);
    }

    public void RemoveModifier(IModifier<T> mod)
    {
        _modifiers.RemoveModifier(mod);
    }
    public override string ToString() {
        return string.Format("{0} ({1})", Value, ModifiedValue);
    }
}

using System;

namespace Weaver.Heroes.Body;

/// <summary>
/// <para>
/// Monitors a Flag raised as long as 
/// at least one provider is registered.
/// </para>
/// <para>
/// Usefull when can multiple sources can 
/// activate the same option but only one is
/// necessary and bonuses don't cumulate.
/// </para>
/// </summary>
public class ProviderModule : Module, IValue<bool>
{
    public event ValueChangedHandler<bool> OnValueChanged;

    private List<object> _providers = new();

    public ProviderModule(string moduleName) : base(moduleName)
    {
    }
    
    /// <summary>
    /// True while at least one provider remains.
    /// </summary>
    public bool Value{
        get { return _providers.Count > 0; }
    }

    public string Label => ModuleName;

    public void AddProvider( object p ) {
        _providers.Add(p);
        if(_providers.Count == 1)
            OnValueChanged?.Invoke(this);
    }

    public void RemoveProvider( object p ) {
        _providers.Remove(p);
        if(_providers.Count == 0)
            OnValueChanged?.Invoke(this);
    }

    public override string ToString() {
        return Value ? "True" : "False";
    }
}

using System;
using Weaver.Heroes.Body;
using Weaver.Heroes.Body.Value;

namespace Weaver.Heroes.Mind;


/// <summary>
/// Deprecated for uselessness and not using Values
/// </summary>
public class XPModule : Module {
    public SimpleValueModule<int> _totalXp { get; init; }
    public SimpleValueModule<int> _spentXp { get; init; }

    public XPModule(string moduleName = "xp") : base(moduleName)
    {
        _totalXp = new("total", 0);
        _spentXp = new("spent", 0);
        Register(_totalXp);
        Register(_spentXp);
    }

    public int TotalXp { get { return _totalXp.Value; } }
    public int XpDepense { get { return _spentXp.Value; } }
    public int XpRestant { get { return TotalXp - XpDepense; } }

    public void GagnerXP(int xp ) {
        _totalXp.Value += xp;
    }

    public bool DepenserXp(int xp ) {
        if(xp > XpRestant)
            return false;
        _spentXp.Value += xp;
        return true;
    }

    public void Reset() {
        _totalXp.Value = 0;
        _spentXp.Value = 0;
    }

    public override string ToString() {
        return TotalXp + " - " + XpRestant;
    }
}
using System;
using Weaver.Heroes.Body;
using Weaver.Heroes.Body.Modification;

namespace Weaver.Heroes.Mind;


public abstract class AlterationStateModule : Module, IModuleModification<AgentModule>
{
    public AlterationStateModule(string moduleName) : base(moduleName)
    {
    }

    public abstract void ApplyModification(AgentModule module);
    public abstract bool CanApply(AgentModule module);
    public abstract void RemoveModification(AgentModule module);
}
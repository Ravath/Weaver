using System;
using Weaver.Heroes.Body;
using Weaver.Heroes.Body.Value;

namespace Weaver.Heroes.Mind;


public abstract class ActionModule : Module, ITargeting
{

    public TargetType TargetType { get; init; }
    public ValueModule<int> TargetRangeModule { get; } = new("TargetRange", 1);
    public int TargetRange { get => TargetRangeModule.ModifiedValue; }
    public ValueModule<int> MaxTargetsModule { get; } = new("MaxTarget", 1);
    public int MaxTargets { get => MaxTargetsModule.ModifiedValue; }

    public ActionModule(string moduleName) : base(moduleName)
    {
		Register(TargetRangeModule);
		Register(MaxTargetsModule);
    }

    public abstract bool IsActionAvailable(AgentModule caster);
    public abstract bool CanTarget(ITargetable targetCandidate);
    public abstract bool AffectTargets(AgentModule caster, params ITargetable[] targetCandidate);
    public abstract bool ConsumeUsage(AgentModule caster);
}
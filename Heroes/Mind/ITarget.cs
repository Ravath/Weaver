using System;
using Weaver.Heroes.Body;
using Weaver.Heroes.Body.Value;

namespace Weaver.Heroes.Mind;


public interface ITargetable
{
    TargetType TargetType { get; }
}


public interface ITargeting {

	/// <summary>
	/// The type of target that can be affected.
	/// <br/>
	/// For the purpose of interface usage, this is mandatory,
	/// and can't have multiple values at the same time 
	/// even if the implemented action should.
	/// In such case, implement different actions for each target type.
	/// <br/>
	/// In case the target type is not restrictive enough,
	/// use 'CanTarget' to narrow it down.
	/// </summary>
	TargetType TargetType { get; }

	/// <summary>
	/// Range in meters, squares or any appropriate unit used by the implementation.
	/// -1 if no limit.
	/// </summary>
	int TargetRange { get; }
	
	/// <summary>
	/// How many targets the caster can affect at the same time.
	/// -1 if no limit.
	/// </summary>
	int MaxTargets { get; }

	/// <summary>
	/// Check if the agent can use this ability at this very moment.
	/// </summary>
	/// <param name="Caster"></param>
	/// <returns>True if the ability can be used.</returns>
	bool IsActionAvailable(AgentModule caster);

	bool CanTarget(ITargetable targetCandidate);

	bool AffectTargets(AgentModule caster, params ITargetable[] targetCandidate);

	bool ConsumeUsage(AgentModule caster);
}
using System;

namespace Weaver.Heroes.Mind;

public interface ICombatStepListener
{
    /// <summary>
    /// The step when the time is counted and actions are performed.
    /// </summary>
    public CombatStep UpdateStep { get; }
    /// <summary>
    /// The step when the time is counted and actions are performed.
    /// </summary>
    public int UpdatePhase { get; }
    /// <summary>
    /// Must be set to true when last activity is done
    /// to be removed by the delegate managers.
    /// </summary>
    bool Finished { get; }
    void OnCombatStep(Combat sender);
}
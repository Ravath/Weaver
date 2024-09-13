namespace Weaver.Heroes.Mind;


public delegate void CombatStepEventHandler(Combat sender, CombatStep step, int phase);


public enum CombatStep
{
    /// <summary>
    /// When the combat starts.
    /// </summary>
    CombatStart,
    /// <summary>
    /// When the combat ends.
    /// </summary>
    CombatEnd,
    /// <summary>
    /// When a combat round starts.
    /// </summary>
    RoundStart,
    /// <summary>
    /// When a combat round ends.
    /// </summary>
    RoundEnd,
    /// <summary>
    /// When a combat phase starts.
    /// Not fired if the rules don't have phase mechanics.
    /// </summary>
    PhaseStart,
    /// <summary>
    /// When a combat phase ends.
    /// Not fired if the rules don't have phase mechanics.
    /// </summary>
    PhaseEnd,
    /// <summary>
    /// When a character's turn starts.
    /// </summary>
    TurnStart,
    /// <summary>
    /// When a character's turn ends.
    /// </summary>
    TurnEnd
}

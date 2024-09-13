using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using Weaver.Heroes.Mind;

namespace Weaver.Heroes.Mind;

using CombatAgent = Tuple< int[],AgentModule,Player? >;

/// <summary>
/// A initiative management and combat phase events management.
/// <br/>
/// Before use :
/// <list>
/// <item> - Implement a initiative roll mechanism.</item>
/// <item> - configure number of phases.</item>
/// </list>
/// </summary>
public class Combat
{
    #region Events
    /// <summary>
    /// Fired when a new phase is reached.
    /// </summary>
    public event CombatStepEventHandler OnCombatStep;
    #endregion

    private List<CombatAgent> _agents = new();

    /// <summary>
    /// The implementation of the current RPG
    /// for rolling initiative.
    /// </summary>
    public IRollInitiative InitiativeDelegate{ get; private set; }

    /// <summary>
    /// True if the higher initiative score should play first.
    /// </summary>
    public bool DecreasingOrderInitiative { get; private set; } = true;

    /// <summary>
    /// The number of phases used in the game.
    /// There are multiple phases during one round.
    /// <br/>
    /// If 1 Phase number, the PhaseStart and PhaseEnd events will not be fired.
    /// <br/>
    /// Phase mechanisms examples :
    /// <list>
    /// <item> - L5R4 : prephase of choosing a stanse.</item>
    /// <item> - Shadowrun 3 : fast characters can have a supplementary round.</item>
    /// <item> - Polaris : a declaration and a action rounds.</item>
    /// </list>
    /// </summary>
    public int PhaseNumber { get; private set; } = 1;
    public bool HasMoreThanOnePhase => PhaseNumber > 1;

    /// <summary>
    /// The current phase identifier.
    /// Starting from 0 and up to 'PhaseNumber-1'.
    /// </summary>
    public int CurrentPhase { get; private set; } = 1;

    /// <summary>
    /// The currently playing character.
    /// </summary>
    public CombatAgent ActiveAgent { get; private set;}

    public Combat(IRollInitiative initiativeDelegate, bool decreasingOrderInitiative = true, int phaseNumber = 1)
    {
        InitiativeDelegate = initiativeDelegate;
        DecreasingOrderInitiative = decreasingOrderInitiative;
        PhaseNumber = phaseNumber;
    }

    public void AddAgent( AgentModule agent, Player? player = null )
    {
        Contract.Assert( InitiativeDelegate != null );

        // Roll init and register
        int[] init = InitiativeDelegate.RollInitiatives( agent );
        _agents.Add( Tuple.Create( init, agent, player ));
        OnCombatStep += agent.CombatStepReceivedEvent;

        // Sort
        Comparison<CombatAgent> comp = CompareInit;
        if(DecreasingOrderInitiative)
            comp = (x,y) => CompareInit(y, x);
        _agents.Sort(comp);
    }

    public void RemoveAgents()
    {
        foreach( CombatAgent agent in _agents)
        {
            OnCombatStep -= agent.Item2.CombatStepReceivedEvent;
        }
        _agents.Clear();
    }

    public void ActivateNextTurnAgent()
    {
        int ci = _agents.IndexOf(ActiveAgent);
        int index = (ci+1)%_agents.Count;
        ActiveAgent = _agents[index];
    }

    /// <summary>
    /// Sorts the agents by there initiative results.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns>
    ///     A signed integer that indicates the relative values of x and y, as shown in the
    ///     following table.
    ///
    ///     Value           – Meaning
    ///     Less than 0     –x is less than y.
    ///     0               –x equals y.
    ///     Greater than 0  –x is greater than y.
    /// </returns>
    private int CompareInit(CombatAgent x, CombatAgent y)
    {
        Debug.Assert(x.Item1.Length == y.Item1.Length);
        for(int i = 0; i<x.Item1.Length ; i++)
        {
            if(x.Item1[i] > y.Item1[i])
                return 1;
            else if(x.Item1[i] < y.Item1[i])
                return -1;
        }
        Debug.Fail("Should always be different by design.");
        return 0;
    }

    public void StartCombat()
    {
        ActiveAgent = _agents[0];
        OnCombatStep?.Invoke(this, CombatStep.CombatStart, PhaseNumber);
        OnCombatStep?.Invoke(this, CombatStep.RoundStart, PhaseNumber);
        OnCombatStep?.Invoke(this, CombatStep.TurnStart, PhaseNumber);
    }

    public void EndTurn()
    {
        bool endPhase = _agents.Last() == ActiveAgent;
        bool endRound = endPhase && PhaseNumber == CurrentPhase;

        CurrentPhase = (CurrentPhase+1)%PhaseNumber;
        
        OnCombatStep?.Invoke(this, CombatStep.TurnEnd, CurrentPhase);
        if(endPhase && HasMoreThanOnePhase)
            OnCombatStep?.Invoke(this, CombatStep.PhaseEnd, CurrentPhase);
        if(endRound)
            OnCombatStep?.Invoke(this, CombatStep.RoundEnd, CurrentPhase);
        ActivateNextTurnAgent();
        if(endRound)
            OnCombatStep?.Invoke(this, CombatStep.RoundStart, CurrentPhase);
        if(endPhase && HasMoreThanOnePhase)
            OnCombatStep?.Invoke(this, CombatStep.PhaseStart, CurrentPhase);
        OnCombatStep?.Invoke(this, CombatStep.TurnStart, CurrentPhase);
    }

    public void EndCombat()
    {
        OnCombatStep?.Invoke(this, CombatStep.TurnEnd, CurrentPhase);
        OnCombatStep?.Invoke(this, CombatStep.RoundEnd, CurrentPhase);
        OnCombatStep?.Invoke(this, CombatStep.CombatEnd, CurrentPhase);
    }

}

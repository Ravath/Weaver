using System;
using System.Diagnostics.Contracts;
using System.Runtime;
using Weaver.Heroes.Body;
using Weaver.Utils;

namespace Weaver.Heroes.Mind;


public class AgentModule : Module, INamed, ITargetable
{
    public string Name { get; set; }
    public TargetType TargetType { get; set; } = TargetType.Agent;
    
    public AgentModule(string moduleName, int nbrActionCategory) : base(moduleName)
    {
        // Initialize the actions categories for the implemented RPG.
        _actions = new ModuleCollectionModule<ActionModule>[nbrActionCategory];
        for (int cat = 0; cat < _actions.Length; cat++)
        {
            _actions[cat] = new ModuleCollectionModule<ActionModule>("action_" + cat.ToString());
            Register(_actions[cat]);
        }
        _alterationStates = new("alterations");
        Register(_alterationStates);
        _skills = new("skills");
        Register(_skills);
        
    }

    #region Action region
    private readonly ModuleCollectionModule<ActionModule>[] _actions;
    public ModuleCollectionModule<ActionModule>[] Actions => _actions;

    /// <summary>
    /// Add an action the agent can do.
    /// </summary>
    /// <param name="action">The action to add.</param>
    /// <param name="category">
    /// Enum depending on the RPG implementation.
    /// Generally :
    /// <list type="bullet">
    /// <item>0 : DEFAULT</item>
    /// <item>1 : COMBAT</item>
    /// <item>2 : SKILL</item>
    /// <item>3 : SPLELL</item>
    /// <item>4...: OTHERS...</item>
    /// </list>
    /// </param>
    public void AddAction(ActionModule action, int category)
    {
        _actions[category].Add(action);
    }

#pragma warning disable CA1822 // Mark members as static
    public void RemoveAction(ActionModule action)
#pragma warning restore CA1822 // Mark members as static
    {
        Contract.Assert(action.Parent != null);
        action.Parent.Unregister(action);
    }
    #endregion


    #region Movement region
    // TODO movements ?
    // Use a cost table ?
    #endregion
    #region Perception region
    // TODO Perception ?
    // Use inhibition/bool triggers ?
    #endregion
    #region Temporary states region
    private readonly ModuleCollectionModule<SkillModule> _skills;
    public ModuleCollectionModule<SkillModule> Skills => _skills;

    /// <summary>
    /// Add an action the agent can do.
    /// </summary>
    /// <param name="skill">The action to add.</param>
    /// <param name="category">
    /// Enum depending on the RPG implementation.
    /// Generally :
    /// <list type="bullet">
    /// <item>0 : DEFAULT</item>
    /// <item>1 : COMBAT</item>
    /// <item>2 : SKILL</item>
    /// <item>3 : SPLELL</item>
    /// <item>4...: OTHERS...</item>
    /// </list>
    /// </param>
    public void AddSkill(SkillModule skill)
    {
        _skills.Add(skill);
    }

#pragma warning disable CA1822 // Mark members as static
    public void RemoveSkill(SkillModule skill)
#pragma warning restore CA1822 // Mark members as static
    {
        Contract.Assert(skill.Parent != null);
        skill.Parent.Unregister(skill);
    }
    #endregion
    #region Temporary states region
    private readonly ModuleCollectionModule<AlterationStateModule> _alterationStates;
    public ModuleCollectionModule<AlterationStateModule> Alterations => _alterationStates;

    /// <summary>
    /// Add an action the agent can do.
    /// </summary>
    /// <param name="alteration">The action to add.</param>
    /// <param name="category">
    /// Enum depending on the RPG implementation.
    /// Generally :
    /// <list type="bullet">
    /// <item>0 : DEFAULT</item>
    /// <item>1 : COMBAT</item>
    /// <item>2 : SKILL</item>
    /// <item>3 : SPLELL</item>
    /// <item>4...: OTHERS...</item>
    /// </list>
    /// </param>
    public void AddAlteration(AlterationStateModule alteration)
    {
        _alterationStates.Add(alteration);
    }

#pragma warning disable CA1822 // Mark members as static
    public void RemoveAlteration(AlterationStateModule alteration)
#pragma warning restore CA1822 // Mark members as static
    {
        Contract.Assert(alteration.Parent != null);
        alteration.Parent.Unregister(alteration);
    }
    #endregion
    #region Can Play region
    // TODO Can Play ?
    // To know if can play : paralysed, dead or what.
    #endregion

    #region Combat Step events
    /// <summary>
    /// The listeners to CombatStep events.
    /// </summary>
    private List<ICombatStepListener> OnCombatStepListeners = new();
    public void AddCombatStepListener(ICombatStepListener listener)
    {
        OnCombatStepListeners.Add(listener);
    }
    public void RemoveCombatStepListener(ICombatStepListener listener)
    {
        OnCombatStepListeners.Remove(listener);
    }
    /// <summary>
    /// When a Combat Step event is received, call the listeners.
    /// Then, remove the completed ones.
    /// </summary>
    /// <param name="sender">The combat firing the events.</param>
    /// <param name="step">The currently fired Combat Step.</param>
    /// <param name="phase">The current phase of the combat.</param>
    public void CombatStepReceivedEvent(Combat sender, CombatStep step, int phase)
    {
        List<ICombatStepListener> toremove = new();

        foreach(ICombatStepListener cbl in OnCombatStepListeners
        // Filter by step and phase
        .Where((c)=>c.UpdateStep == step && c.UpdatePhase == phase)
        // Filter Turn steps of other characters
        .Where((c)=>c.UpdateStep != CombatStep.TurnStart && c.UpdateStep != CombatStep.TurnEnd
        || sender.ActiveAgent.Item2 == this))
        {
            // Perform listener action
            cbl.OnCombatStep(sender);
            if(cbl.Finished)
                toremove.Add(cbl);
        }

        // Remove finished listeners
        foreach(ICombatStepListener listener in toremove)
        {
            RemoveCombatStepListener(listener);
        }
    }
    #endregion

    #region Action Events
    public event Action<AgentModule, ActionModule> OnPerformingAction;
    public event Action<AgentModule, ActionModule> OnTargetedReaction;
    public void PerformingAction(ActionModule action)
    {
        OnPerformingAction?.Invoke(this, action);
    }
    public void TargetedBy(AgentModule caster, ActionModule targetingAction)
    {
        OnTargetedReaction?.Invoke(caster, targetingAction);
    }
    #endregion
   
}
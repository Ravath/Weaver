using System;

namespace Weaver.Heroes.Mind;


public class CombatTimer : ICombatStepListener
{
    /// <summary>
    /// The step when the time is counted and actions are performed.
    /// </summary>
    public CombatStep UpdateStep { get; } = CombatStep.TurnStart;
    /// <summary>
    /// The step when the time is counted and actions are performed.
    /// </summary>
    public int UpdatePhase { get; } = 0;
    /// <summary>
    /// Must be set to true when last activity is done
    /// to be removed by the delegate managers.
    /// </summary>
    public bool Finished { get; private set; } = false;
    /// <summary>
    /// The time before 2 Actions.
    /// </summary>
    public int PeriodTime { get; init; }
    /// <summary>
    /// The number of complete period to last
    /// before LastAction.
    /// <br/>
    /// -1 is inifinity.
    /// </summary>
    public int PeriodNumber { get; init; }
    /// <summary>
    /// The current time between 2 Actions.
    /// </summary>
    public int CurrentTime { get; set; } = 0;
    /// <summary>
    /// The number of completed periods.
    /// </summary>
    public int CurrentPeriod { get; set; } = 0;
    /// <summary>
    /// Count the number of rounds and perform actions when period time has elapsed.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="step"></param>
    /// <param name="phase"></param>
    public void OnCombatStep(Combat sender)
    {
        // Check period time
        CurrentTime++;
        if(CurrentTime == PeriodTime)
        {
            // Period finished : reset and perform Action.
            CurrentTime = 0;
            CurrentPeriod ++;
            CombatStepAction?.Invoke();
            // If Last Period : perform LastAction and ask removal
            if(CurrentPeriod == PeriodNumber)
            {
                LastAction?.Invoke();
                Finished = true;
            }
        }
    }
    /// <summary>
    /// Action performed at the end of a completed Period.
    /// </summary>
    public Action CombatStepAction { get; init; }
    /// <summary>
    /// Action performed when every Period has ended.
    /// </summary>
    public Action LastAction { get; init; }
}

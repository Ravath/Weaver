using System;

namespace Weaver.Heroes.Mind;


/// <summary>
/// The interface for a delegate implementation pattern.
/// The Initiative mechanism is dependant on the rpg
/// and should be implemented by the RPG module.
/// </summary>
public interface IRollInitiative {
    /// <summary>
    /// The given agent rolls for initiative.
    /// <br/>
    /// When implementing, index_0 is the basic initiative result,
    /// and further index are for the draw ties.
    /// Two given initiative results should not be equal.
    /// For that, a solution is to use an incrementing value
    /// for the last index.
    /// <br/>
    /// If the original rules have a draw mechanics, 
    /// consider if it may not be one of this two categories :
    /// <list>
    /// <item>
    /// - The players and ennemies all play at ones.
    /// Just use a Player proxy and a Ennemy proxy in the turn order.
    /// </item>
    /// <item>
    /// - They are narrative mechanics and you do not need 
    /// a combat implementation that much.
    /// </item>
    /// </list>
    /// </summary>
    /// <param name="agent"></param>
    /// <returns></returns>
    int[] RollInitiatives(AgentModule agent);
}
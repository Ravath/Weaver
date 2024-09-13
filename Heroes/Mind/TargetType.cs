namespace Weaver.Heroes.Mind;


/// <summary>
/// The type of target that can be affected by a capacity or action.
/// Used for interface purposes only.
/// </summary>
public enum TargetType {
    /// <summary>
    /// A character.
    /// The user must interact with Agents to target.
    /// </summary>
	Agent,
    /// <summary>
    /// A inanimate object.
    /// The user must interact with objects to target.
    /// </summary>
    Object,
    /// <summary>
    /// A position on the map, in the world.
    /// The user must interact with the map to target.
    /// </summary>
    Place,
    /// <summary>
    /// No target needed.
    /// The user can use the ability without targeting.
    /// Some kind of interacting is still recommanded
    /// for confirmation purposes.
    /// </summary>
    Void
}
using System;

namespace Weaver.Tales;

/// <summary>
/// A choice the user has to make to continue the story.
/// </summary>
public interface IStoryChoice
{
    /// <summary>
    /// The choice to display.
    /// <remarks>If no text, it is meant to mean "Continue", generaly done when there are no other alternatives.</remarks>
    /// </summary>
    public string Text { get; }
    /// <summary>
    /// The reference to the next story paragraph resulting from this choice.
    /// </summary>
    /// <remarks>Not Null.</remarks>
    public StoryParagraph Next { get; }
}

public class StoryChoice : IStoryChoice
{
    /// <summary>
    /// The choice to display.
    /// <remarks>If no text, it is meant to mean "Continue", generaly done when there are no other alternatives.</remarks>
    /// </summary>
    public string Text { get; set; }
    /// <summary>
    /// The identifier of a StoryParagraph.
    /// </summary>
    public string Label { get; set; }
    /// <summary>
    /// The reference to the next story paragraph resulting from this choice.
    /// </summary>
    /// <remarks>Not Null.</remarks>
    public StoryParagraph Next { get; set; }
}

public interface IStoryParagraphProvider {
    StoryParagraph GetNextStoryChoice(IStoryChoice choice);
}

/// <summary>
/// A choice made automatically by the system, as implemented.
/// The result is delegated to a 'provider'.
/// </summary>
public class ComputedStoryChoice : IStoryChoice
{
    /// <summary>
    /// Delegate resolving the story choice.
    /// </summary>
    readonly IStoryParagraphProvider _provider;

    public ComputedStoryChoice(IStoryParagraphProvider provider, string text = "")
    {
        _provider = provider;
        Text = text;
    }

    /// <summary>
    /// The choice to display.
    /// <remarks>If no text, it is meant to mean "Continue", generaly done when there are no other alternatives.</remarks>
    /// </summary>
    public string Text { get; set; }

    public StoryParagraph Next => _provider.GetNextStoryChoice(this);
}
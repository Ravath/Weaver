using System;

namespace Weaver.Tales;

public interface IStoryChoice
{
    /// <summary>
    /// If no text, just "Continue".
    /// </summary>
    public string Text { get; }
    /// <summary>
    /// Not Null.
    /// </summary>
    public StoryParagraph Next { get; }
}

public class StoryChoice : IStoryChoice
{
    /// <summary>
    /// If no text, just "Continue".
    /// </summary>
    public string Text { get; set; }
    /// <summary>
    /// The identifier of a StoryParagraph.
    /// </summary>
    public string Label { get; set; }
    /// <summary>
    /// Not Null.
    /// </summary>
    public StoryParagraph Next { get; set; }
}

public interface IStoryParagraphProvider {
    StoryParagraph GetNextStoryChoice(IStoryChoice choice);
}

public class ComputedStoryChoice : IStoryChoice
{
    IStoryParagraphProvider _provider;

    public ComputedStoryChoice(IStoryParagraphProvider provider, string text = "")
    {
        _provider = provider;
        Text = text;
    }

    /// <summary>
    /// If no text, just "Continue".
    /// </summary>
    public string Text { get; set; }

    public StoryParagraph Next => _provider.GetNextStoryChoice(this);
}
using System;
using System.Collections.Generic;

namespace Weaver.Tales;

/// <summary>
/// The node of a story tree.
/// A collection of choices can be made from this point and lead to the next paragraph.
/// Comes with a list of effects to apply for narrative purposes.
/// </summary>
public class StoryParagraph
{
    /// <summary>
    /// Identifier of the Paragraph.
    /// </summary>
    public string Label { get; set; }

    /// <summary>
    /// This paragraphs text.
    /// </summary>
    public string Text { get; set; }

    /// <summary>
    /// Development comments.
    /// </summary>
    public string DevNotes { get; set; }

    /// <summary>
    /// The list of choices that can be made from this paragraph.
    /// </summary>
    public List<IStoryChoice> Choices { get; set; } = new List<IStoryChoice>();

    /// <summary>
    /// The list of effects applied when this paragraph is enacted.
    /// </summary>
    public List<IStoryEffect> Effects { get; set; } = new List<IStoryEffect>();

    /// <summary>
    /// The list of effects applied when a choice has been made and a next paragraph will be enacted.
    /// </summary>
    public List<IStoryEffect> PostEffects { get; set; } = new List<IStoryEffect>();
}
using System;
using System.Collections.Generic;

namespace Weaver.Tales;

/// <summary>
/// The entry point of a Story narrative structure.
/// The story is structured as a graph, and can be used to implement multiple-choice narratives.
/// </summary>
public class Story
{
    private readonly List<StoryParagraph> _paragraphs = new();
    private readonly Dictionary<string, StoryParagraph> _paragraphsIndex = new();

    /// <summary>
    /// The title of the story.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Description of the story.
    /// </summary>
    public string Description { get; set; }
    // TODO Creation Date, Author(s) and other credits, language, 
    // TODO Combat system, default character sheet and character creation

    /// <summary>
    /// The complete list of the direct children paragraphs.
    /// </summary>
    public IEnumerable<StoryParagraph> Paragraphs { get { return _paragraphs; } }

    /// <summary>
    /// Add a paragraph child as a story entry point. Uses the paragraph's Label as a key.
    /// </summary>
    /// <param name="chunk"></param>
    public void AddChunk(StoryParagraph chunk)
    {
        _paragraphsIndex.Add(chunk.Label, chunk);
        _paragraphs.Add(chunk);
    }

    /// <summary>
    /// Checks if the given string if the key of a direct paragraph child.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public bool HasChunk(string id)
    {
        return _paragraphsIndex.ContainsKey(id);
    }

    /// <summary>
    /// Get the direct child paragraph identified by the given key.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public StoryParagraph GetChunk(string id)
    {
        return _paragraphsIndex[id];
    }
}
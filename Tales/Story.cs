using System;
using System.Collections.Generic;

namespace Weaver.Tales;

/// <summary>
/// A Story narrative structure.
/// Stores and manages every paragraphs from the story.
/// The story is structured as a graph, and can be used to implement multiple-choice narratives.
/// The entry point depends on implementation, by it is generally the first paragraph from the list.
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
    /// The complete list of the paragraphs.
    /// </summary>
    public IEnumerable<StoryParagraph> Paragraphs { get { return _paragraphs; } }

    /// <summary>
    /// Add a paragraph to the story. Uses the paragraph's Label as a key.
    /// </summary>
    /// <param name="chunk"></param>
    public void AddChunk(StoryParagraph chunk)
    {
        _paragraphsIndex.Add(chunk.Label, chunk);
        _paragraphs.Add(chunk);
    }

    /// <summary>
    /// Checks if the given string is the key of an existing paragraph.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public bool HasChunk(string id)
    {
        return _paragraphsIndex.ContainsKey(id);
    }

    /// <summary>
    /// Get the paragraph identified by the given key.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public StoryParagraph GetChunk(string id)
    {
        return _paragraphsIndex[id];
    }
}
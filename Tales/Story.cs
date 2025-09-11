using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Weaver.Tales;

/// <summary>
<<<<<<< HEAD
/// A Story narrative structure.
/// Stores and manages every paragraphs from the story.
/// The story is structured as a graph, and can be used to implement multiple-choice narratives.
/// The entry point depends on implementation, by it is generally the first paragraph from the list.
=======
/// The entry point of a Story narrative structure.
/// The story is structured as a graph, and can be used to implement multiple-choice narratives.
>>>>>>> 43df54ed5a35f1b43c61d4f901108e1a8f0998ae
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
<<<<<<< HEAD
    /// The complete list of the paragraphs.
=======
    /// The complete list of the direct children paragraphs.
>>>>>>> 43df54ed5a35f1b43c61d4f901108e1a8f0998ae
    /// </summary>
    public IEnumerable<StoryParagraph> Paragraphs { get { return _paragraphs; } }

    /// <summary>
<<<<<<< HEAD
    /// Add a paragraph to the story. Uses the paragraph's Label as a key.
=======
    /// Add a paragraph child as a story entry point. Uses the paragraph's Label as a key.
>>>>>>> 43df54ed5a35f1b43c61d4f901108e1a8f0998ae
    /// </summary>
    /// <param name="chunk"></param>
    public void AddChunk(StoryParagraph chunk)
    {
        _paragraphsIndex.Add(chunk.Label, chunk);
        _paragraphs.Add(chunk);
    }

    /// <summary>
<<<<<<< HEAD
    /// Checks if the given string is the key of an existing paragraph.
=======
    /// Checks if the given string if the key of a direct paragraph child.
>>>>>>> 43df54ed5a35f1b43c61d4f901108e1a8f0998ae
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public bool HasChunk(string id)
    {
        return _paragraphsIndex.ContainsKey(id);
    }

    /// <summary>
<<<<<<< HEAD
    /// Get the paragraph identified by the given key.
=======
    /// Get the direct child paragraph identified by the given key.
>>>>>>> 43df54ed5a35f1b43c61d4f901108e1a8f0998ae
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public StoryParagraph GetChunk(string id)
    {
        return _paragraphsIndex[id];
    }

    [OnDeserialized]
    internal void OnDeserializedMethod(StreamingContext context)
    {
        foreach (StoryParagraph paragraph in _paragraphs)
        {
            _paragraphsIndex[paragraph.Label] = paragraph;
        }
        foreach (StoryParagraph paragraph in _paragraphs)
        {
            foreach(StoryChoice choice in paragraph.Choices)
            {
                choice.Next = _paragraphsIndex[choice.Label];
            }
        }
    }
}
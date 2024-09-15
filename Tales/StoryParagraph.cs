using System;
using System.Collections.Generic;

namespace Weaver.Tales;

public class StoryParagraph
{
    public string Label { get; set; }
    public string Text { get; set; }
    public string DevNotes { get; set; }
    public List<IStoryChoice> Choices { get; } = new List<IStoryChoice>();
    public List<IStoryEffect> Effects { get; } = new List<IStoryEffect>();
    public List<IStoryEffect> PostEffects { get; } = new List<IStoryEffect>();
}
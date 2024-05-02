using Assets.Scripts.ThoughtBubble;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thought : ScriptableObject
{
    public Sprite ThoughtIcon;
    public ThoughtEmotion Emotion;
    public float Duration;

    public static Thought FromThinkable(IThinkable thinkable)
    {
        return CreateInstance<Thought>()
            .SetThoughtIcon(thinkable.ThoughtIcon)
            .SetDuration(3)
            .SetEmotion(ThoughtEmotion.Neutral);
    }
}

public static class ThoughtExtensions
{
    public static Thought SetEmotion(this Thought thought, ThoughtEmotion emotion) { thought.Emotion = emotion; return thought; }
    public static Thought SetDuration(this Thought thought, float duration) { thought.Duration = duration; return thought; }
    public static Thought SetThoughtIcon(this Thought thought, Sprite thoughtIcon) { thought.ThoughtIcon = thoughtIcon; return thought; }
}

using Assets.Scripts.ThoughtBubble;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class Thought : ScriptableObject
{
    public Sprite ThoughtIcon;
    public ThoughtEmotion Emotion;
    public bool KeepUntilStopped;
    public float Duration;
    public float Scale;

    //private Func<> _onClickMethod;

    public static Thought FromThinkable(IThinkable thinkable)
    {
        return CreateInstance<Thought>()
            .SetThoughtIcon(thinkable.ThoughtIcon)
            .SetDuration(1)
            .SetDuration(true)
            .SetEmotion(ThoughtEmotion.Neutral)
            .SetScale(1);
    }
}

public static class ThoughtExtensions
{
    public static Thought SetEmotion(this Thought thought, ThoughtEmotion emotion) { thought.Emotion = emotion; return thought; }
    public static Thought SetDuration(this Thought thought, float duration) { thought.Duration = duration; return thought; }
    public static Thought SetDuration(this Thought thought, bool keepUntilStopped) { thought.KeepUntilStopped = keepUntilStopped; return thought; }
    public static Thought SetThoughtIcon(this Thought thought, Sprite thoughtIcon) { thought.ThoughtIcon = thoughtIcon; return thought; }
    public static Thought SetScale(this Thought thought, float scale) { thought.Scale = scale; return thought; }
}

using Assets.Scripts.ThoughtBubble;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class ThoughtManager : MonoBehaviour
{
    public ThoughtBubbleSortMode SortMode;

    [SerializeField]
    private ThoughtBubble _neutralBubble;

    private List<ThoughtBubble> _thoughtBubbles = new();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Sets up a ThoughtBubble for this Thought on this Manager
    // echos back the given Thought for one line assignments
    public Thought ThinkAbout(Thought thought)
    {
        ThoughtBubble bubble = InstantiateBubbleFromEmotion(thought.Emotion);
        bubble.SetThought(thought);

        AddBubble(bubble);

        return thought;
    }

    public void StopThinkingAbout(Thought thought)
    {

    }
    public void StopThinkingAbout(ThoughtBubble bubble)
    {
        _thoughtBubbles.Remove(bubble);
        Destroy(bubble);
    }

    public void StopThinking()
    {
        foreach (ThoughtBubble bubble in _thoughtBubbles)
        {
            Destroy(bubble);
        }

        _thoughtBubbles.Clear();
    }

    // this will work for now, but I should really set up some editor UI to have a key-value list of emotions and prefabs
    // so this can be done automatically
    private ThoughtBubble InstantiateBubbleFromEmotion(ThoughtEmotion emotion)
    {
        switch (emotion)
        {
            case ThoughtEmotion.Neutral:
                return Instantiate(_neutralBubble);
            default:
                Debug.LogError($"Thought Manager cannot handle Thought of Emotion {emotion}");
                return null;
        }
    }

    private void AddBubble(ThoughtBubble bubble)
    {
        switch (SortMode)
        {
            case ThoughtBubbleSortMode.FirstComeFirstDrawn:
                _thoughtBubbles.Add(bubble);
                break;
            case ThoughtBubbleSortMode.FirstComeLastDrawn:
                _thoughtBubbles.Prepend(bubble);
                break;
            case ThoughtBubbleSortMode.FirstToDisapearFirstDrawn:
                // haven't tested this yet. chances are its nonsence
                _thoughtBubbles.Insert(_thoughtBubbles.FindIndex(match => match.LifetimeRemaining > bubble.LifetimeRemaining), bubble);
                break;
            default:
                _thoughtBubbles.Add(bubble);
                break;
        }
        bubble.SetManager(this);
    }

    // now does nothing but should handle putting bubbles in the correct positions and/or showing/hiding them
    private void ResetBubbles()
    {

    }
}

public enum ThoughtBubbleSortMode
{
    FirstComeFirstDrawn,
    FirstComeLastDrawn,
    FirstToDisapearFirstDrawn,
}
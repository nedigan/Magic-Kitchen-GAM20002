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
    [SerializeField]
    private ThoughtBubble _infoBubble;

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
        ThoughtBubble foundBubble = null;
        foreach (ThoughtBubble bubble in _thoughtBubbles)
        {
            if (bubble.Thought == thought)
            {
                foundBubble = bubble;
                break;
            }
        }

        if (foundBubble != null) { StopThinkingAbout(foundBubble); }
    }
    public void StopThinkingAbout(ThoughtBubble bubble)
    {
        _thoughtBubbles.Remove(bubble);
        Destroy(bubble.gameObject);
    }

    public void StopThinking()
    {
        foreach (ThoughtBubble bubble in _thoughtBubbles)
        {
            Destroy(bubble.gameObject);
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
            case ThoughtEmotion.Info:
                return Instantiate(_infoBubble);
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
                // haven't tested this yet. chances are it's nonsence
                _thoughtBubbles.Insert(_thoughtBubbles.FindIndex(match => match.LifetimeRemaining > bubble.LifetimeRemaining), bubble);
                break;
            default:
                _thoughtBubbles.Add(bubble);
                break;
        }
        bubble.SetManager(this);
        bubble.transform.SetParent(this.transform, false);

        ResetBubblePositions();
    }

    private void ResetBubblePositions()
    {
        int i = 1;
        foreach (var bubble in _thoughtBubbles.Skip(1))
        {
            Vector3 offset = Vector3.back * 0.1f * i;

            if (i % 2 == 0)
            {
                offset += Vector3.left * 0.25f;
            }
            else
            {
                offset += Vector3.right * 0.25f;
            }

            bubble.transform.localPosition = offset;

            i++;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawIcon(transform.position, "Icon_ThoughtBubble");
    }
}

public enum ThoughtBubbleSortMode
{
    FirstComeFirstDrawn,
    FirstComeLastDrawn,
    FirstToDisapearFirstDrawn,
}
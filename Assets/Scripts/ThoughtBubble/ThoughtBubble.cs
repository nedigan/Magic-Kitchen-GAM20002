using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ThoughtEmotion
{
    Neutral,
}

public class ThoughtBubble : MonoBehaviour
{
    private float _lifetime = 0;
    private ThoughtManager _manager;
    private Thought _thought;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetManager(ThoughtManager manager) { _manager = manager; }
    public void SetThought(Thought thought) { _thought = thought; }
}

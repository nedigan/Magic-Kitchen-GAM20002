using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Unity.VisualScripting;

public enum ThoughtEmotion
{
    Neutral,
    Info,
}

public class ThoughtBubble : MonoBehaviour
{
    [SerializeField]
    private GameObject _iconCenter;
    [SerializeField]
    private Vector2 _iconSize;
    private Vector2 IconSize => _iconSize * transform.lossyScale;

    private float _lifetime = 0;
    private ThoughtManager _manager;
    private Thought _thought;

    private Animator _animator;

    public Thought Thought => _thought;
    public float LifetimeRemaining => _thought.Duration - _lifetime;

    // Start is called before the first frame update
    void Start()
    {
        if (TryGetComponent(out Animator animator))
        {
            _animator = animator;
            _animator.StartPlayback();
        }

        transform.rotation = Quaternion.Euler(0, -45, 0);
    }

    // Update is called once per frame
    void Update()
    {
        _lifetime += Time.deltaTime;

        if (!_thought.KeepUntilStopped && LifetimeRemaining <= 0)
        {
            _manager.StopThinkingAbout(this);
        }
    }

    public void SetManager(ThoughtManager manager) { _manager = manager; }
    public void SetThought(Thought thought) 
    { 
        _thought = thought;

        transform.localScale = transform.localScale * _thought.Scale;

        if (_thought.ThoughtIcon != null)
        {
            SpriteRenderer spriteRenderer = _iconCenter.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = _thought.ThoughtIcon;

            float smaller_cord;
            if (IconSize.x < IconSize.y)
            {
                smaller_cord = IconSize.x;
            }
            else
            {
                smaller_cord = IconSize.y;
            }

            spriteRenderer.transform.localScale = new Vector3(smaller_cord / spriteRenderer.bounds.size.x, smaller_cord / spriteRenderer.bounds.size.y, 1);
            spriteRenderer.sortingLayerName = "ThoughtBubble";
            spriteRenderer.sortingOrder = 1;
        }
    }

    private void OnMouseDown()
    {
        Debug.LogWarning("MOUSE DOWN! I REREAT, MOUSE DOWN!");

        if (_thought.OnClickMethod != null)
        {
            _thought.OnClickMethod();
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (_iconCenter != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(_iconCenter.transform.position, new Vector3(IconSize.x, IconSize.y, 0.01f));
        }
    }
}
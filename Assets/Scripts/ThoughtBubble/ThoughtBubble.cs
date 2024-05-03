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
        if (_thought.ThoughtIcon != null)
        {
            SpriteRenderer spriteRenderer = _iconCenter.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = _thought.ThoughtIcon;

            float smaller_cord;
            if (_iconSize.x < _iconSize.y)
            {
                smaller_cord = _iconSize.x;
            }
            else
            {
                smaller_cord = _iconSize.y;
            }

            //Vector3 newScale = spriteRenderer.transform.localScale;
            //newScale = new Vector3(newScale.x * transform.localScale.x, newScale.y * transform.localScale.y, newScale.z * transform.localScale.z) /** smaller_cord*/;
            //spriteRenderer.transform.localScale = newScale;

            spriteRenderer.transform.localScale = new Vector3(smaller_cord / spriteRenderer.bounds.size.x, smaller_cord / spriteRenderer.bounds.size.y, 1);
            spriteRenderer.sortingLayerName = "ThoughtBubble";
            spriteRenderer.sortingOrder = 1;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (_iconCenter != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(_iconCenter.transform.position, new Vector3(_iconSize.x, _iconSize.y, 0.01f));
        }
    }
}

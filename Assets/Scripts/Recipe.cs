using Assets.Scripts.ThoughtBubble;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class Recipe : ScriptableObject, IThinkable
{
    public List<ItemType> Ingredients;
    public Item Result;
    public float CookTime = 1;

    // IThinkable fields
    public Sprite ThoughtIcon => Result.ThoughtIcon;
}

using Assets.Scripts.ThoughtBubble;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Tiers{
    Tier1,
    Tier2,
    Tier3
}

[CreateAssetMenu()]
public class Recipe : ScriptableObject, IThinkable
{
    public List<ItemType> Ingredients;
    public Item Result;
    public float CookTime = 1;
    public Tiers Tier;

    // IThinkable fields
    public Sprite ThoughtIcon => Result.ThoughtIcon;
}

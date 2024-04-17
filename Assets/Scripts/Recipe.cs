using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class Recipe : ScriptableObject
{
    public List<ItemType> Ingredients;
    public Item Result;
    public float CookTime = 1;
}

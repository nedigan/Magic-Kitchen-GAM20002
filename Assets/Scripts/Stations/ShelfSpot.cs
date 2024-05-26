using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Station))]
public class ShelfSpot : MonoBehaviour
{
    private Station _station;
    public Station Station => _station;

    public bool StartStocked = true;

    private List<Item> _ownedItems = new();

    public int OwnedItems => _ownedItems.Count;
    public bool IsFull => _ownedItems.Count >= _station.ItemHolder.MaxItems;


    private void Awake()
    {
        _station = GetComponent<Station>();
    }

    public void AddOwnedItem(Item item)
    {
        _ownedItems.Add(item);
    }

    public void RemoveOwnedItem(Item item)
    {
        _ownedItems.Remove(item);
    }
}

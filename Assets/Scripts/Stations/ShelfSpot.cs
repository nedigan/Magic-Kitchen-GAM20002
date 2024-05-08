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

    private Item _owner;
    public Item Owner => _owner;
    public bool HasOwner => _owner != null;


    private void Start()
    {
        _station = GetComponent<Station>();
    }

    public void SetOwner(Item owner)
    {
        if (_owner != null) { RemoveCurrentOwner(); }

        _owner = owner;
    }

    public void RemoveCurrentOwner()
    {
        _owner = null;
    }
}

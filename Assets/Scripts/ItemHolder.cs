using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemHolder : MonoBehaviour
{
    [Tooltip("Where the held item will be placed. If left empty will default to this component's parent")]
    public GameObject HoldLocation;
    [SerializeField]
    //private Item _heldItem;
    private List<Item> _items = new List<Item>();

    [Range(1, 100)]
    public int MaxItems = 1;

    public bool ClaimHeldItems = true;

    [Header("Stack Visuals")]
    public float WidthSeparation = 1.0f;
    public float HeightSeparation = 1.0f;
    [Range(1, 50)]
    public int Columns = 1;
    public bool FillRowsFirst = false;

    public List<Item> HeldItems => _items;
    public bool Full => _items.Count == MaxItems;
    public bool Empty => _items.Count == 0;

    // Start is called before the first frame update
    void Awake()
    {
        if (HoldLocation == null) { HoldLocation = gameObject; }
        //if (_heldItem != null) { PickUpItem(_heldItem); }
    }

    public void PickUpItem(Item item)
    {
        if (Full) { DropCurrentItemOnGround(); }

        _items.Add(item);

        item.OnPickedUp(this);
        item.transform.SetParent(HoldLocation.transform);
        UpdateItemPositions();

        if (ClaimHeldItems) { item.Claimed = true; }
    }

    public void DropCurrentItemOnGround()
    {
        if (!Empty) 
        {
            _items.Last().transform.SetParent(null, true);

            RemoveCurrentItem();
        }
    }

    public void RemoveCurrentItem()
    {
        if (!Empty) 
        {
            Item item = _items.Last();
            _items.RemoveAt(_items.Count - 1);

            item.OnPutDown();

            UpdateItemPositions();

            if (ClaimHeldItems) { item.Claimed = false; }
        }        
    }

    // RemoveCurrentItem used to do what this does now
    // some old code may still be relying on that old version
    // if that breaks anything switch it to use this instead
    public void RemoveSpecificItem(Item item)
    {
        if (_items.Contains(item))
        {
            _items.Remove(item);
            item.OnPutDown();

            UpdateItemPositions();

            if (ClaimHeldItems) { item.Claimed = false; }
        }
    }

    private void UpdateItemPositions()
    {
        int maxHeight = (int)Math.Ceiling((float)MaxItems / (float)Columns);

        for (int i = 0; i < _items.Count; i++)
        {
            Vector3 position;
            if (FillRowsFirst)
            {
                position = new Vector3((i / maxHeight) * HeightSeparation, (i % maxHeight) * WidthSeparation);
            }
            else
            {
                position = new Vector3((i % maxHeight) * HeightSeparation, (i / maxHeight) * WidthSeparation);
            }

            _items[i].transform.localPosition = position;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        if (HoldLocation != null)
        {
            //Gizmos.DrawSphere(HoldLocation.transform.position, 0.25f);
            Gizmos.DrawIcon(HoldLocation.transform.position, "Icon_ItemHolder");
        }
        else
        {
            //Gizmos.DrawSphere(transform.position, 0.25f);
            Gizmos.DrawIcon(transform.position, "Icon_ItemHolder");
        }
    }

    private void OnDrawGizmosSelected()
    {
        int maxHeight = (int)Math.Ceiling((float)MaxItems / (float)Columns);
        //int maxHeight = MaxItems / Columns;

        for (int i = 0; i < MaxItems; i++)
        {
            Vector3 position;
            if (FillRowsFirst)
            {
                position = new Vector3((i / maxHeight) * HeightSeparation, (i % maxHeight) * WidthSeparation);
            }
            else
            {
                position = new Vector3((i % maxHeight) * HeightSeparation, (i / maxHeight) * WidthSeparation);
            }
            position = transform.TransformPoint(position);
            Gizmos.DrawWireSphere(position, 0.25f);
        }
    }
}

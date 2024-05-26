using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Room))]
public class StoreRoom : MonoBehaviour
{
    public List<StoreRoomTarget> TargetStock = new();

    private List<Item> _currentStock = new();
    public List<Item> CurrentStock => _currentStock;

    public List<ShelfSpot> ShelfSpots = new();

    private Room _room;

    // Start is called before the first frame update
    void Start()
    {
        _room = GetComponent<Room>();

        InstaRestock();
    }

    /// <summary>
    /// Returns prefabs of all the missing stock
    /// </summary>
    /// <returns></returns>
    public List<Item> GetMissingStockPrefabs()
    {
        List<Item> currentStock = _currentStock;
        List<Item> missingStock = new();
        //Item toRemove = null;

        //foreach (StoreRoomTarget target in TargetStock)
        //{
        //    for (int i = 0; i < target.Number; i++)
        //    {
        //        if (toRemove != null) { currentStock.Remove(toRemove); }

        //        foreach (Item current in currentStock)
        //        {
        //            if (target.Item.Type == current.Type)
        //            {
        //                toRemove = current;
        //                break;
        //            }
        //        }

        //        missingStock.Add(target.Item);
        //    }
        //}

        foreach (StoreRoomTarget target in TargetStock)
        {
            int currentNumber = 0;
            foreach (Item current in currentStock)
            {
                if (current.Type == target.Item.Type) { currentNumber++; }
            }

            for (int i = 0; i < target.Number - currentNumber; i++)
            {
                missingStock.Add(target.Item);
            }
        }

        return missingStock;
    }

    public List<Item> GenerateMissingStockItems()
    {
        List<Item> missingsPrefabs = GetMissingStockPrefabs();
        List<Item> missingItems = new();

        foreach (Item item in missingsPrefabs)
        {
            foreach (ShelfSpot shelfSpot in ShelfSpots)
            {
                if (shelfSpot.IsFull == false)
                {
                    Item newStock = Instantiate(item);
                    newStock.AddToStoreRoom(this, shelfSpot);
                    missingItems.Add(newStock);
                    break;
                }
            }
        }

        return missingItems;
    }

    public void InstaRestock()
    {
        List<Item> missings = GetMissingStockPrefabs();

        foreach (Item item in missings)
        {
            foreach (ShelfSpot shelfSpot in ShelfSpots)
            {
                if (shelfSpot.StartStocked && shelfSpot.IsFull == false)
                {
                    Item newStock = Instantiate(item);
                    newStock.SetCurrentRoom(_room);
                    newStock.AddToStoreRoom(this, shelfSpot);
                    shelfSpot.Station.ItemHolder.PickUpItem(newStock);
                    break;
                }
            }
        }
    }

    private void OnValidate()
    {
        //int totalTarget = 0;
        //foreach (StoreRoomTarget target in TargetStock) { totalTarget += target.Number; }
        //int totalSpots = 0;
        //foreach (ShelfSpot shelfSpot in ShelfSpots) { totalSpots += shelfSpot.Station.ItemHolder.MaxItems; }

        //if (totalTarget > totalSpots)
        //{
        //    Debug.LogWarning($"StoreRoom {this} has more Target Stock than Shelf Spots. Remove some Target Stock or add more SHelf Spots");
        //}
    }
}

[System.Serializable]
public struct StoreRoomTarget
{
    public Item Item;
    public int Number;
}

using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

[RequireComponent(typeof(Room))]
public class StoreRoom : MonoBehaviour
{
    public List<Item> TargetStock = new();

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
        Item toRemove = null;

        foreach (Item target in TargetStock)
        {
            if (toRemove != null) { currentStock.Remove(toRemove); }

            foreach (Item current in currentStock)
            {
                if (target.Type == current.Type)
                {
                    //currentStock.Remove(current);
                    toRemove = current;
                    break;
                }
            }

            missingStock.Add(target);
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
                if (shelfSpot.StartStocked && shelfSpot.HasOwner == false)
                {
                    Item newStock = Instantiate(item);
                    //newStock.SetCurrentRoom(_room);
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
                if (shelfSpot.StartStocked && shelfSpot.HasOwner == false)
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
        if (TargetStock.Count > ShelfSpots.Count)
        {
            Debug.LogWarning($"StoreRoom {this} has more Target Stock than Shelf Spots. Remove some Target Stock or add more SHelf Spots");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
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

        List<Item> missings = GetMissingStock();

        foreach (Item item in missings)
        {
            foreach (ShelfSpot shelfSpot in ShelfSpots)
            {
                if (shelfSpot.StartStocked && shelfSpot.ItemHolder.Empty)
                {
                    Item newStock = Instantiate(item);
                    newStock.SetCurrentRoom(_room);
                    shelfSpot.ItemHolder.PickUpItem(newStock);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //public int TargetStockOfType(ItemType type)
    //{

    //}

    //public int CurrentStockOfType(ItemType type)
    //{

    //}

    /// <summary>
    /// Returns prefabs of all the missing stock
    /// </summary>
    /// <returns></returns>
    public List<Item> GetMissingStock()
    {
        List<Item> currentStock = _currentStock;
        List<Item> missingStock = new();

        foreach (Item target in TargetStock)
        {
            foreach (Item current in currentStock)
            {
                if (target.Type == current.Type)
                {
                    currentStock.Remove(current);
                    continue;
                }
            }

            missingStock.Add(target);
        }

        //foreach (Item missing in missingStock)
        //{
        //    Debug.Log(missing);
        //}

        return missingStock;
    }

    public void InstaRestock()
    {

    }

    private void OnValidate()
    {
        if (TargetStock.Count > ShelfSpots.Count)
        {
            Debug.LogWarning($"StoreRoom {this} has more Target Stock than Shelf Spots. Remove some Target Stock or add more SHelf Spots");
        }
    }
}

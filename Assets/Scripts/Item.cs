using Assets.Scripts;
using Assets.Scripts.ThoughtBubble;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Sugar,
    Flour,
    Egg,
    Meal,
}

// Base class for any object in a room that Animals can pick up
public class Item : MonoBehaviour, IRoomObject, IThinkable
{
    [SerializeField]
    private ItemType _type;
    public ItemType Type => _type;

    [Tooltip("Has an Animal claimed this Item to be used")]
    public bool Claimed = false;

    [SerializeField]
    private Room _currentRoom;

    [SerializeField]
    SpriteRenderer _sprite;

    private ItemHolder _holder;

    private StoreRoom _storeRoom;
    private ShelfSpot _shelfSpot;
    public ShelfSpot ShelfSpot => _shelfSpot;

    // IRoomObject fields
    public Room CurrentRoom { get => _currentRoom; set => _currentRoom = value; }

    public Vector3 Destination => transform.position;

    // IThinkable fields
    public Sprite ThoughtIcon => _sprite.sprite;

    public void SetCurrentRoom(Room room)
    {
        _currentRoom = room;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (_currentRoom == null && RoomFinder.TryFindRoomAbove(gameObject, out Room foundRoom))
        {
            SetCurrentRoom(foundRoom);
        }

        TaskManager manager = FindFirstObjectByType<TaskManager>();
        if (manager != null) { manager.Items.Add(this); }
    }

    public void OnPickedUp(ItemHolder holder)
    {
        if (_holder != null) { _holder.RemoveCurrentItem(); }

        _holder = holder;
    }

    public void OnPutDown()
    {
        _holder = null;
    }

    public void AddToStoreRoom(StoreRoom storeRoom, ShelfSpot shelfSpot)
    {
        _shelfSpot = shelfSpot;
        _storeRoom = storeRoom;

        _storeRoom.CurrentStock.Add(this);
        _shelfSpot.SetOwner(this);
    }

    public void RemoveFromCurrentStoreRoom()
    {
        _storeRoom?.CurrentStock.Remove(this);
        _shelfSpot?.RemoveCurrentOwner();

        _storeRoom = null;
        _shelfSpot = null;
    }

    private void OnDestroy()
    {
        RemoveFromCurrentStoreRoom();
    }
}
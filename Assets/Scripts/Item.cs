using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Sugar,
    Flour,
    Egg,
}

// Base class for any object in a room that Animals can pick up / interact with

public class Item : MonoBehaviour, IRoomObject
{
    [SerializeField]
    private ItemType type;

    [Tooltip("Has an Animal claimed this Item to be used")]
    public bool Claimed = false;

    [SerializeField]
    private Room _currentRoom;

    // IRoomObject fields
    public Room CurrentRoom { get => _currentRoom; set => _currentRoom = value; }

    public Vector3 Destination => transform.position;

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

    // Update is called once per frame
    void Update()
    {
        
    }
}
using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StationType
{
    Stove, //for cooking
    Shelf, //for grabbing ingredients
    Table, //for serving food
    Window, // for Turtles to give and take orders
}

[RequireComponent(typeof(TaskHolder))]
public class Station : MonoBehaviour, IRoomObject
{
    public StationType Type;
    public bool Occupied = false;
    [SerializeField]
    private Room _currentRoom;
    public GameObject StandLocation;
    public TaskHolder TaskHolder;

    // IRoomObject fields
    public Room CurrentRoom { get => _currentRoom; set => _currentRoom = value; }

    public Vector3 Destination => StandLocation.transform.position;

    private void Start()
    {
        if (_currentRoom == null)
        {
            _currentRoom = RoomFinder.FindRoomAbove(gameObject);
        }

        TaskManager manager = FindFirstObjectByType<TaskManager>();
        if (manager != null) { manager.Stations.Add(this); }
    }
}

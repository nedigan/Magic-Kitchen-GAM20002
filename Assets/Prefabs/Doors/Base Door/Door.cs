using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Door ConnectingDoor = null;
    [SerializeField]
    protected Room _room;
    [SerializeField]
    protected SceneDoor _sceneDoor;
    [SerializeField]
    protected TableDoor _tableDoor;

    public Room Room => _room;
    public SceneDoor SceneDoor => _sceneDoor;
    public TableDoor TableDoor => _tableDoor;

    public event EventHandler DoorConnected;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnConnectedTo(Door door)
    {
        ConnectingDoor = door;
        DoorConnected?.Invoke(this, EventArgs.Empty);

        Debug.Log($"A door in the {Room.name} has connected to a door in the {door.Room.name}");
        SceneDoor.OnConnectedTo(door);
        TableDoor.OnConnectedTo(door);
    }

    public void OnDisconnectFrom(Door door)
    {
        ConnectingDoor = null;

        Debug.Log($"A door in the {Room.name}has disconnected to a door in the {door.Room.name}");
        SceneDoor.OnDisconnectFrom(door);
        TableDoor.OnDisconnectFrom(door);
    }
}

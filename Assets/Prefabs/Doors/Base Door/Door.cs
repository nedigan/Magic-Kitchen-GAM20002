using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Room ConnectingRoom = null;
    [SerializeField]
    protected Room _room;
    [SerializeField]
    protected SceneDoor _sceneDoor;
    [SerializeField]
    protected TableDoor _tableDoor;

    public Room Room => _room;
    public SceneDoor SceneDoor => _sceneDoor;
    public TableDoor TableDoor => _tableDoor;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnConnectedTo(Room room)
    {
        ConnectingRoom = room;

        SceneDoor.OnConnectedTo(room);
        TableDoor.OnConnectedTo(room);
    }

    public void OnDisconnectFrom(Room room)
    {
        ConnectingRoom = null;

        SceneDoor.OnDisconnectFrom(room);
        TableDoor.OnDisconnectFrom(room);
    }
}

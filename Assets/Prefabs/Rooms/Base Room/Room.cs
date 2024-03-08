using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] 
    protected Door _door;
    [SerializeField]
    protected TableRoom _tableRoom; 

    public List<Animal> Animals = new List<Animal>();
    public List<Item> Items = new List<Item>();
    public List<Station> Stations = new List<Station>();

    public Door Door => _door;
    public TableRoom TableRoom => _tableRoom;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // this is called only on the door that initiates the connection
    // use it to call functions you want called on one of the connected rooms but not both
    public void ConnectToRoom(Room room)
    {
        this.OnConnectedTo(room);
        room.OnConnectedTo(this);
    }

    public void OnConnectedTo(Room room)
    {
        Debug.Log($"{this} Connected to {room}");
        Door.OnConnectedTo(room);
    }

    // this is called only on the door that initiates the disconnection
    // use it to call functions you want called on one of the disconnected rooms but not both
    public void DisconnectFromRoom(Room room)
    {
        this.OnDisconnectFrom(room);
        room.OnDisconnectFrom(this);
    }

    public void OnDisconnectFrom(Room room)
    {
        Debug.Log($"Disconnected from {room}");
        Door.OnDisconnectFrom(room);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class TableDoor : MonoBehaviour
{
    public Collider ProxcimityCollider;
    [SerializeField]
    private Door _door;

    public Door Door => _door;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log($"Trigger!");
        TableDoor otherDoor = other.GetComponent<TableDoor>();
        if (otherDoor != null && Door.ConnectingRoom == null && otherDoor.Door.ConnectingRoom == null)
        {
            Door.Room.ConnectToRoom(otherDoor.Door.Room);
        }
    }

    //private void OnTriggerEnter(Collision collision)
    //{
    //    Debug.Log($"Collision!");
    //    TableDoor otherDoor = collision.collider.GetComponent<TableDoor>();
    //    if (otherDoor != null && Door.ConnectingRoom == null && otherDoor.Door.ConnectingRoom == null) 
    //    {
    //        Door.Room.ConnectToRoom(otherDoor.Door.Room);
    //    }
    //}

    private void OnTriggerExit(Collider other)
    {
        //Debug.Log($"Trigger Exit!");
        TableDoor otherDoor = other.GetComponent<TableDoor>();
        if (otherDoor != null && Door.ConnectingRoom == otherDoor.Door.Room)
        {
            Door.Room.DisconnectFromRoom(otherDoor.Door.Room);
        }
    }

    //private void OnCollisionExit(Collision collision)
    //{
    //    Debug.Log($"Collision Exit!");
    //    TableDoor otherDoor = collision.collider.GetComponent<TableDoor>();
    //    if (otherDoor != null && Door.ConnectingRoom == otherDoor.Door.Room)
    //    {
    //        Door.Room.DisconnectFromRoom(otherDoor.Door.Room);
    //    }
    //}

    public void OnConnectedTo(Room room)
    {

    }

    public void OnDisconnectFrom(Room room)
    {

    }
}

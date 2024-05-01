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

    private TunnelManager _tunnelManager;

    public void Start()
    {
        _tunnelManager = FindFirstObjectByType<TunnelManager>(); // Slow but works?
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Door"))
            return;
        TableDoor otherDoor = other.GetComponent<TableDoor>();
        if (otherDoor != null && Door.ConnectingDoor == null && otherDoor.Door.ConnectingDoor == null)
        {
            Door.OnConnectedTo(otherDoor.Door);
            otherDoor.Door.OnConnectedTo(Door); // Trigger on other door will not call so will call from here

            _tunnelManager.DoorsHaveConnected(otherDoor.Door, Door);
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
        if (!other.CompareTag("Door"))
            return;
        TableDoor otherDoor = other.GetComponent<TableDoor>();
        if (otherDoor != null && Door.ConnectingDoor == otherDoor.Door)
        {
            Door.OnDisconnectFrom(otherDoor.Door);
            otherDoor.Door.OnDisconnectFrom(Door);// Trigger on other door will not call so will call from here

            _tunnelManager.DoorsHaveDisconnected(otherDoor.Door, Door);
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

    public void OnConnectedTo(Door door)
    {

    }

    public void OnDisconnectFrom(Door door)
    {

    }
}

using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using UnityEngine.AI;
using static System.Collections.Specialized.BitVector32;

public enum AnimalType
{
    Fox,
    Chicken,
    Turtle
}

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(TaskHolder))]
public class Animal : MonoBehaviour, IRoomObject
{
    public NavMeshAgent Agent;
    public TaskHolder TaskHolder;
    [SerializeField]
    private Room _currentRoom;

    [SerializeField] 
    private SpriteRenderer _sprite;
    private Vector3 _prevPos;

    private bool _moving = false;

    public AnimalType Type;

    [SerializeField]
    private GameObject _itemHoldLocation;
    private Item _heldItem;

    public Item HeldItem { get => _heldItem; }

    // IRoomObject fields
    public Room CurrentRoom { get => _currentRoom; set => _currentRoom = value; }

    public Vector3 Destination => transform.position;

    // Start is called before the first frame update
    void Start()
    {
        // find the first room above this in the Hierarchy
        if (_currentRoom == null) 
        {
            _currentRoom = RoomFinder.FindRoomAbove(gameObject);
        }
        
        TaskManager manager = FindFirstObjectByType<TaskManager>();
        if (manager != null ) { manager.Animals.Add(this); }

        _prevPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Sprite stuff
        if (_sprite != null)
        {
            _sprite.transform.position = transform.position;
            _sprite.flipX = _prevPos.x < transform.position.x;
            _prevPos = transform.position;
        }

        if (_moving && AtDestination())
        {
            _moving = false;
            OnReachedDestination();

        }
    }

    // Sets a destination for the Animal to go to
    // TODO: Should handle pathfinding between rooms
    public bool SetDestination(IRoomObject roomObject)
    {
        bool isInCurrentRoom = false;
        if (roomObject.CurrentRoom == CurrentRoom)
        {
            Agent.SetDestination(roomObject.Destination);
            isInCurrentRoom = true;
        }
        else
        {
            foreach (Door door in CurrentRoom.Doors)
            {
                if (door.ConnectingDoor != null && door.ConnectingDoor.Room == roomObject.CurrentRoom)
                {
                    SetDestination(door);
                    door.ConnectingDoor.DoorConnected -= TaskHolder.ResetTask; // unsubcribes

                }
                else
                {
                    // listen for door connect event
                    door.DoorConnected += TaskHolder.ResetTask;
                }

                isInCurrentRoom = false;
            }
        }
        Agent.stoppingDistance = 1;
        _moving = true;
        Agent.isStopped = false;
        return isInCurrentRoom;
    }
    // having three of these is very hacky but I don't have the time to do it properly
    //public bool SetDestination(Station station) // returns true if target is in room
    //{
    //    bool isInCurrentRoom = false;
    //    if (station.CurrentRoom == CurrentRoom)
    //    {
    //        Agent.SetDestination(station.StandLocation.transform.position);
    //        isInCurrentRoom = true;
    //    }
    //    else
    //    {
    //        foreach (Door door in CurrentRoom.Doors)
    //        {
    //            if (door.ConnectingDoor != null && door.ConnectingDoor.Room == station.CurrentRoom)
    //            {
    //                SetDestination(door);
    //                door.ConnectingDoor.DoorConnected -= TaskHolder.ResetTask; // unsubcribes
                    
    //            }
    //            else 
    //            {
    //                // listen for door connect event
    //                door.DoorConnected += TaskHolder.ResetTask;
    //            }

    //            isInCurrentRoom = false;
    //        }
    //    }
    //    Agent.stoppingDistance = 1;
    //    _moving = true;
    //    Agent.isStopped = false;
    //    return isInCurrentRoom;
    //}
    //// TODO: account for the fact that an animal could be moving
    //public bool SetDestination(Animal animal) 
    //{
    //    bool isInCurrentRoom = false;
    //    if (animal.CurrentRoom == CurrentRoom)
    //    {
    //        Agent.SetDestination(animal.transform.position);
    //        isInCurrentRoom = true;
    //    }
    //    else
    //    {
    //        foreach (Door door in CurrentRoom.Doors)
    //        {
    //            if (door.ConnectingDoor != null && door.ConnectingDoor.Room == animal.CurrentRoom)
    //            {
    //                SetDestination(door);
    //                door.ConnectingDoor.DoorConnected -= TaskHolder.ResetTask; // unsubcribes

    //            }
    //            else
    //            {
    //                // listen for door connect event
    //                door.DoorConnected += TaskHolder.ResetTask;
    //            }

    //            isInCurrentRoom = false;
    //        }
    //    }

    //    Agent.stoppingDistance = 1;
    //    _moving = true;
    //    Agent.isStopped = false;
    //    return isInCurrentRoom;
    //}
    //public bool SetDestination(Item item)
    //{
    //    bool isInCurrentRoom = false;
    //    if (item.CurrentRoom == CurrentRoom)
    //    {
    //        Agent.SetDestination(item.transform.position);
    //        isInCurrentRoom = true;
    //    }
    //    else
    //    {
    //        foreach (Door door in CurrentRoom.Doors)
    //        {
    //            if (door.ConnectingDoor != null && door.ConnectingDoor.Room == item.CurrentRoom)
    //            {
    //                SetDestination(door);
    //                door.ConnectingDoor.DoorConnected -= TaskHolder.ResetTask; // unsubcribes

    //            }
    //            else
    //            {
    //                // listen for door connect event
    //                door.DoorConnected += TaskHolder.ResetTask;
    //            }

    //            isInCurrentRoom = false;
    //        }
    //    }

    //    Agent.stoppingDistance = 1;
    //    _moving = true;
    //    Agent.isStopped = false;
    //    return isInCurrentRoom;

    //    //Agent.stoppingDistance = 1;
    //    //Agent.SetDestination(item.transform.position);
    //    //_moving = true;
    //    //Agent.isStopped = false;
    //}

    public void SetDestination(Door door)
    {
        Agent.stoppingDistance = 1;
        Agent.SetDestination(door.SceneDoor.transform.position);
        _moving = true;
        Agent.isStopped = false;
    }
    private bool AtDestination()
    {
        // got this from here:
        // https://discussions.unity.com/t/how-can-i-tell-when-a-navmeshagent-has-reached-its-destination/52403/5

        // Check if we've reached the destination
        if (!Agent.pathPending)
        {
            if (Agent.remainingDistance <= Agent.stoppingDistance)
            {
                if (!Agent.hasPath || Agent.velocity.sqrMagnitude == 0f)
                {
                    return true;
                }
            }
        }

        return false;
    }

    public event EventHandler ReachedDestination;
    private void OnReachedDestination()
    {
        ReachedDestination?.Invoke(this, EventArgs.Empty);
    }

    // Item stuff

    public void PickUpItem(Item item)
    {

    }

    public void DropCurrentItem()
    {

    }
}
using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using TMPro.EditorUtilities;
using Unity.VisualScripting;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using UnityEngine.AI;

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
        if (_currentRoom == null && RoomFinder.TryFindRoomAbove(gameObject, out Room foundRoom))
        {
            SetCurrentRoom(foundRoom);
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
            bool shouldFlip = _prevPos.x < transform.position.x;

            _sprite.transform.position = transform.position;
            _sprite.flipX = shouldFlip;
            _prevPos = transform.position;

            if (_heldItem != null)
            {
                _heldItem.transform.position = _itemHoldLocation.transform.position;

                if (shouldFlip)
                {
                    _heldItem.transform.position -= new Vector3(0, _itemHoldLocation.transform.position.y * 2, 0);
                }
            }
        }

        if (_moving && AtDestination())
        {
            _moving = false;
            OnReachedDestination();
        }
    }

    // Sets a destination for the Animal to go to
    // TODO: Should handle pathfinding between rooms
    public bool SetDestination(IRoomObject destination)
    {
        bool isInCurrentRoom = false;
        if (destination.CurrentRoom == CurrentRoom)
        {
            Agent.SetDestination(destination.Destination);
            isInCurrentRoom = true;
        }
        else
        {
            foreach (Door door in CurrentRoom.Doors)
            {
                if (door.ConnectingDoor != null && door.ConnectingDoor.Room == destination.CurrentRoom)
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
    //public bool SetDestinationAnd<Delaga>(IRoomObject destination)
    //{
    //    if (SetDestination(destination)) { }
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

    public void MoveToRoom(Door exitDoor)
    {
        //Debug.Log("Transporting...");
        Agent.enabled = false;
        transform.position = exitDoor.SceneDoor.ExitPosition.transform.position;
        Agent.enabled = true;

        // Try task again once in the other room
        //CurrentRoom = exitDoor.Room;
        SetCurrentRoom(exitDoor.Room);
        TaskHolder.ResetTask();
        // CHANGE PARENT if you want
    }

    // Item stuff

    public void PickUpItem(Item item)
    {
        if (_heldItem != null) { DropCurrentItemOnGround(); }

        _heldItem = item;

        _heldItem.Claimed = true;
    }

    public void DropCurrentItemOnGround()
    {
        _heldItem.transform.position = new Vector3(_itemHoldLocation.transform.position.x, 0.1f, _itemHoldLocation.transform.position.z);

        RemoveCurrentItem();
    }

    public void RemoveCurrentItem()
    {
        _heldItem.Claimed = false;

        _heldItem = null;
    }

    public void SetCurrentRoom(Room room)
    {
        _currentRoom = room;

        if (_heldItem != null) { _heldItem.SetCurrentRoom(room); }
    }
}
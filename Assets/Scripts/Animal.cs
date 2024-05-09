using Assets.Scripts;
using Assets.Scripts.ThoughtBubble;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public enum AnimalType
{
    Fox,
    Chicken,
    Turtle,
    Deliverer
}

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(TaskHolder))]
[RequireComponent(typeof(ItemHolder))]
public class Animal : MonoBehaviour, IRoomObject, IThinkable
{
    public NavMeshAgent Agent;
    public TaskHolder TaskHolder;
    [SerializeField]
    private Room _currentRoom;

    [SerializeField] 
    private SpriteRenderer _sprite;
    private Vector3 _prevPos;

    private bool _moving = false;
    private GameObject _destination;

    public AnimalType Type;

    [SerializeField]
    private GameObject _itemHoldLocation;
    private GameObject _secretThirdItemHoldLocation;

    [SerializeField]
    private ThoughtManager _thoughtManager;

    public ThoughtManager ThoughtManager => _thoughtManager;

    private ItemHolder _itemHolder;
    public ItemHolder ItemHolder => _itemHolder;

    // IRoomObject fields
    public Room CurrentRoom { get => _currentRoom; set => _currentRoom = value; }

    public Vector3 Destination => transform.position;

    // IThinkable fields
    public Sprite ThoughtIcon => _sprite.sprite;

    private void Awake()
    {
        _prevPos = transform.position;

        _itemHolder = GetComponent<ItemHolder>();

        _secretThirdItemHoldLocation = new GameObject();
        _secretThirdItemHoldLocation.transform.SetParent(_itemHoldLocation.transform);

        _itemHolder.HoldLocation = _secretThirdItemHoldLocation;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (_currentRoom == null && RoomFinder.TryFindRoomAbove(gameObject, out Room foundRoom))
        {
            SetCurrentRoom(foundRoom);
        }
        
        TaskManager manager = FindFirstObjectByType<TaskManager>();
        if (manager != null ) { manager.Animals.Add(this); }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSprite();

        if (_moving && AtDestination())
        {
            OnReachedDestination();
        }
    }

    private void UpdateSprite()
    {
        // Sprite stuff
        if (_sprite != null)
        {
            bool shouldFlip = _prevPos.x < transform.position.x;

            _sprite.transform.position = transform.position;
            _sprite.flipX = shouldFlip;
            _prevPos = transform.position;

            if (shouldFlip)
            {
                _secretThirdItemHoldLocation.transform.localPosition = new Vector3(-_itemHoldLocation.transform.localPosition.x, 0, 0);
            }
            else
            {
                _secretThirdItemHoldLocation.transform.localPosition = Vector3.zero;
            }
        }
    }

    // Sets a destination for the Animal to go to
    // TODO: Should handle pathfinding between rooms
    private bool _subscribedToDoorConnectEvents = false;
    public bool SetDestination(IRoomObject destination, Room currentRoom = null, int depth = 0, List<Room> checkedRooms = null)
    {
        if (currentRoom == null)
            currentRoom = CurrentRoom;

        if (checkedRooms == null)
            checkedRooms = new List<Room>();

        if (checkedRooms.Contains(currentRoom))
            return false;

        checkedRooms.Add(currentRoom);

        bool isInCurrentRoom = false;
        if (destination.CurrentRoom == currentRoom)
        {
            _destination = null;
            if (depth == 0) // if the animal is actually in the room
                Agent.SetDestination(destination.Destination);
            isInCurrentRoom = true;
        }
        else
        {
            foreach (Door door in currentRoom.Doors)
            {
                // listen for door connect event
                if (!_subscribedToDoorConnectEvents)
                {
                    //door.DoorConnected += TaskHolder.ResetTask;
                    // remove any old disconnect subscriptions
                    //door.DoorDisconnected -= TaskHolder.ResetTask;
                }

                if (door.ConnectingDoor != null && SetDestination(destination, door.ConnectingDoor.Room, depth + 1, checkedRooms))
                {
                    SetDestination(door);
                    //door.ConnectingDoor.DoorDisconnected += TaskHolder.ResetTask;
                    //door.ConnectingDoor.DoorConnected -= TaskHolder.ResetTask; // unsubcribes
                }

                isInCurrentRoom = false;
            }
            _subscribedToDoorConnectEvents = true;
        }
        Agent.stoppingDistance = 1;
        _moving = true;
        Agent.isStopped = false;
        return isInCurrentRoom;
    }

    public bool SetDestination(RoomType roomType, bool waitForValidConnection, Room currentRoom = null, int depth = 0, List<Room> checkedRooms = null)
    {
        if (currentRoom == null)
            currentRoom = CurrentRoom;

        if (checkedRooms == null)
            checkedRooms = new List<Room>();

        if (checkedRooms.Contains(currentRoom))
            return false;

        checkedRooms.Add(currentRoom);

        bool isInCurrentRoom = false;
        if (roomType == currentRoom.RoomType)
        {
            _destination = null;
            isInCurrentRoom = true;
        }
        else
        {
            foreach (Door door in currentRoom.Doors)
            {
                // listen for door connect event
                if (!_subscribedToDoorConnectEvents)
                {
                    //door.DoorConnected += TaskHolder.ResetTask;
                    // remove any old disconnect subscriptions
                    door.DoorDisconnected -= TaskHolder.ResetTask;
                }

                if (waitForValidConnection)
                {
                    if (door.ConnectingDoor != null && SetDestination(roomType, waitForValidConnection, door.ConnectingDoor.Room, depth + 1, checkedRooms))
                    {
                        SetDestination(door);
                        //door.ConnectingDoor.DoorDisconnected += TaskHolder.ResetTask;
                        //door.ConnectingDoor.DoorConnected -= TaskHolder.ResetTask; // unsubcribes
                    }
                }
                else // go to first door even with no connection - for the fox queue
                {
                    SetDestination(door);
                    //door.ConnectingDoor.DoorDisconnected += TaskHolder.ResetTask;
                    break;
                }
            

                isInCurrentRoom = false;
            }
            _subscribedToDoorConnectEvents = true;
        }
        Agent.stoppingDistance = 1;
        _moving = true;
        Agent.isStopped = false;
        return isInCurrentRoom;
    }

    // TODO: Test if this works or not
    public bool SetDestinationAndSubcribe(IRoomObject destination, EventHandler handler)
    {
        if (SetDestination(destination))
        {
            //ReachedDestination += handler;
            return true;
        }
        
        return false;
    }

    public void SetDestination(Door door)
    {
        Agent.stoppingDistance = 1;
        Agent.SetDestination(door.SceneDoor.transform.position);
        _destination = door.gameObject;
        _moving = true;
        Agent.isStopped = false;
    }

    public void SetDestination(Vector3 position)
    {
        Agent.stoppingDistance = 1;
        Agent.SetDestination(position);
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
        _moving = false;

        if (_destination != null && _destination.TryGetComponent(out Door door))
        {
            if (door.ConnectingDoor != null)
            {
                MoveToRoom(door.ConnectingDoor);
            }
            else
            {
                Debug.LogWarning($"{this} arrived at a door without a connection! This should be prevented from happening");
            }
        }
        else
        {
            ReachedDestination?.Invoke(this, EventArgs.Empty);
        }
    }

    public event EventHandler MovedRoom;
    public void MoveToRoom(Door exitDoor)
    {
        //Debug.Log("Transporting...");
        Agent.enabled = false;
        transform.position = exitDoor.SceneDoor.ExitPosition.transform.position;
        Agent.enabled = true;

        // Try task again once in the other room
        //CurrentRoom = exitDoor.Room;
        ClearAnimalFromDoorConnectEvents();
        MovedRoom?.Invoke(this, EventArgs.Empty);
        SetCurrentRoom(exitDoor.Room);
        TaskHolder.ResetTask();
        // CHANGE PARENT if you want
    }

    private void ClearAnimalFromDoorConnectEvents()
    {
        foreach (Door door in CurrentRoom.Doors)
        {
            door.DoorConnected -= TaskHolder.ResetTask;
        }
        _subscribedToDoorConnectEvents = false;
    }

    // Item stuff

    public void PickUpItem(Item item)
    {
        _itemHolder.PickUpItem(item);

        UpdateSprite();
    }

    public void DropCurrentItemOnGround()
    {
        _itemHolder.DropCurrentItemOnGround();
    }

    public void RemoveCurrentItem()
    {    
        _itemHolder.RemoveCurrentItem();
    }

    public void SetCurrentRoom(Room room)
    {
        if (_currentRoom != null) { _currentRoom.Animals.Remove(this); }

        _currentRoom = room;

        _currentRoom.Animals.Add(this);

        if (_itemHolder.Empty == false) { _itemHolder.HeldItem.SetCurrentRoom(room); }
    }

    public void OnDoorConnected(Door door)
    {
        if (_subscribedToDoorConnectEvents) { TaskHolder.ResetTask(); }
    }

    public void OnDoorDisconected(Door door)
    {
        if (_destination == door) { TaskHolder.ResetTask(); }
    }
}
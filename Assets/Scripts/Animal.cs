using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using UnityEngine.AI;
using static System.Collections.Specialized.BitVector32;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(TaskHolder))]
public class Animal : MonoBehaviour
{
    public NavMeshAgent Agent;
    public TaskHolder TaskHolder;
    public Room CurrentRoom;

    public AnimalType Type;

    private bool _moving = false;

    // Start is called before the first frame update
    void Start()
    {
        TaskManager manager = FindFirstObjectByType<TaskManager>();
        if (manager != null ) { manager.Animals.Add(this); }
    }

    // Update is called once per frame
    void Update()
    {
        if (_moving && AtDestination())
        {
            _moving = false;
            OnReachedDestination();

        }
    }

    // Sets a destination for the Animal to go to
    // TODO: Should handle pathfinding between rooms
    // having three of these is very hacky but I don't have the time to do it properly
    public bool SetDestination(Station station) // returns true if target is in room
    {
        bool isInCurrentRoom = false;
        if (station.CurrentRoom == CurrentRoom)
        {
            Agent.SetDestination(station.StandLocation.transform.position);
            isInCurrentRoom = true;
        }
        else
        {
            foreach (Door door in CurrentRoom.Doors)
            {
                if (door.ConnectingDoor != null && door.ConnectingDoor.Room == station.CurrentRoom)
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
        Agent.stoppingDistance = 0;
        _moving = true;
        Agent.isStopped = false;
        return isInCurrentRoom;
    }
    // TODO: account for the fact that an animal could be moving
    public bool SetDestination(Animal animal) 
    {
        bool isInCurrentRoom = false;
        if (animal.CurrentRoom == CurrentRoom)
        {
            Agent.SetDestination(animal.transform.position);
            isInCurrentRoom = true;
        }
        else
        {
            foreach (Door door in CurrentRoom.Doors)
            {
                if (door.ConnectingDoor != null && door.ConnectingDoor.Room == animal.CurrentRoom)
                {
                    SetDestination(door);
                    isInCurrentRoom = false;
                }
                
            }
        }

        Agent.stoppingDistance = 1;
        _moving = true;
        Agent.isStopped = false;
        return isInCurrentRoom;
    }
    public void SetDestination(Item item)
    {
        Agent.stoppingDistance = 1;
        Agent.SetDestination(item.transform.position);
        _moving = true;
        Agent.isStopped = false;
    }

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
}

public enum AnimalType
{
    Fox,
    Chicken,
    Turtle
}
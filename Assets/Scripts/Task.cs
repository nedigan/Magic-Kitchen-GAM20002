using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Task : ScriptableObject
{
    public TaskManager Manager;

    // an IdleTask will set TaskHolder.PerformingTask to false so it can still recive a new task from the TaskManager
    // use for things like a task to send an animal back to a wating area
    public bool IsIdleTask = false;

    private Thought _taskThought;

    public abstract TaskHolder FindTaskHolder();
    public abstract void PerformTask();
    public abstract void StartTask();
    public abstract void FinishTask();
    //public abstract void OnCancelTask();
    public void CancelTask()
    {
        Debug.LogWarning("hi :)");
    }
    // allows this to subscribe to events
    // using it to subscribe to an Animal's ReachedDestination Event
    public void FinishTask(object sender, EventArgs e)
    {
        this.FinishTask();
    }

    public void StartTask(object sender, EventArgs e)
    {
        this.StartTask();
    }

    private void Awake()
    {
        object foundObject = FindFirstObjectByType(typeof(TaskManager));
        if (foundObject != null)
        {
            Manager = (TaskManager)foundObject;
        }
        else
        {
            Debug.LogError("Task could not find TaskManager in scene!");
        }
    }

    protected Animal FindIdleAnimalOfType(AnimalType type)
    {
        foreach (Animal animal in Manager.Animals)
        {
            if (animal.Type == type && animal.TaskHolder.PerformingTask == false)
            {
                return animal;
            }
        }

        return null;
    }
    protected bool TryFindIdleAnimalOfType(AnimalType type, out Animal foundAnimal)
    {
        foundAnimal = FindIdleAnimalOfType(type);
        return foundAnimal != null;
    }

    protected Station FindEmptyStationOfType(StationType type)
    {
        foreach (Station station in Manager.Stations)
        {
            if (station.Type == type && station.Occupied == false)
            {
                return station;
            }
        }

        return null;
    }
    protected bool TryFindEmptyStationOfType(StationType type, out Station foundStation)
    {
        foundStation = FindEmptyStationOfType(type);
        return foundStation != null;
    }

    protected Item FindUnclaimedItemOfType(ItemType type)
    {
        foreach (Item item in Manager.Items)
        {
            if (item.Type == type && item.Claimed == false)
            {
                return item;
            }
        }

        return null;
    }
    protected bool TryFindUnclaimedItemOfType(ItemType type, out Item foundItem)
    {
        foundItem = FindUnclaimedItemOfType(type);
        return foundItem != null;
    }

    // Task Thought

    public void SetTaskThought(ThoughtManager manager, Thought thought, BubbleClickMethod onClickMethod = null)
    {
        // pathfinding sometimes needs to spam StartTask so this failsafe is meant to
        // stop multiple thoughts from popping up when that happens
        if (_taskThought != null) { UnsetTaskThought(manager); }

        _taskThought = manager.ThinkAbout(thought);
        thought.OnClickMethod = onClickMethod ?? CancelTask;
    }

    public void UnsetTaskThought(ThoughtManager manager)
    {
        manager.StopThinkingAbout(_taskThought);
    }
}

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

    public abstract TaskHolder FindTaskHolder();
    public abstract void PerformTask();
    public abstract void StartTask();
    public abstract void FinishTask();
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
}

public class CollectIngredient: Task
{

    public override TaskHolder FindTaskHolder()
    {
        // Find a chicken task holder
        return null;
    }

    public override void StartTask()
    {
        throw new System.NotImplementedException();
    }

    public override void PerformTask()
    {
        throw new System.NotImplementedException();
    }

    public override void FinishTask()
    {
        throw new System.NotImplementedException();
    }
}
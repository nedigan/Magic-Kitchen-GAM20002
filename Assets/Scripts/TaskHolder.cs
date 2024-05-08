using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskHolder : MonoBehaviour
{
    public Task Task;

    [Tooltip("If the holder isn't given a task it will do this instead")]
    public Task DefaultTask;

    public bool PerformingTask = false;
    public bool HasTask => Task != null;

    public void SetTask(Task task, bool cancelCurrentTask = false)
    {
        if (cancelCurrentTask && Task != null)
        {
            Task.CancelTask(false);
        }

        Task = task;
        Task.Holder = this;
        Task.StartTask();
        PerformingTask = !task.IsIdleTask;
    }

    // Called from scene door to redo task when in the other room
    public void ResetTask()
    {
        if (!HasTask)
        {
            Debug.Log("There was no task being performed by this animal");
            return;
        }

        Task.StartTask();
    }

    public void ResetTask(object sender, EventArgs e)
    {
        this.ResetTask();
    }

    public void RemoveCurrentTask(bool setDefault = true)
    {
        if (setDefault && DefaultTask != null) 
        { 
            SetTask(DefaultTask); 
        }
        else 
        { 
            Task = null; 
            PerformingTask = false;
        }
    }

    private void Update()
    {
        if (Task != null)
        {
            Task.PerformTask();
        }
    }
}

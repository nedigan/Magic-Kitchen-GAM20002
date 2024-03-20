using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskHolder : MonoBehaviour
{
    public Task Task;

    public bool PerformingTask = false;

    public void SetTask(Task task)
    {
        Task = task;
        PerformingTask = !task.IsIdleTask;
        Task.StartTask();
    }

    // Called from scene door to redo task when in the other room
    public void ResetTask()
    {
        if (!PerformingTask)
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

    public void RemoveCurrentTask()
    {
        Task = null;
        PerformingTask = false;
    }

    private void Update()
    {
        if (Task != null)
        {
            Task.PerformTask();
        }
    }
}

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
        task.StartTask();
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

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
        task.StartTask();
    }

    private void Update()
    {
        if (Task != null)
        {
            Task.PerformTask();
        }
    }
}

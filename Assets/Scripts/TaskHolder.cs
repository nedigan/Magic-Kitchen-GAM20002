using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskHolder : MonoBehaviour
{
    private Queue<Task> _tasks = new Queue<Task>();

    private bool _performingTask = false;

    public void AddTask(Task task)
    {
        _tasks.Enqueue(task);
    }

    public void StartTask()
    {
        
    }
}

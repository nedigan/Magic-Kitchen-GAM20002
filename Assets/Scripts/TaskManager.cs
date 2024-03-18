using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    private List<Task> _tasks = new List<Task>();

    private List<Animal> _animals = new List<Animal>();
    private List<Station> _stations = new List<Station>();
    private List<Item> _items = new List<Item>();

    public List<Animal> Animals => _animals;
    public List<Station> Stations => _stations;
    public List<Item> Items => _items;

    // Give a task to a task holder
    private void SendTask(Task task, TaskHolder holder)
    {
        holder.SetTask(task);
        _tasksToRemove.Enqueue(task);
    }

    // Add task for the TaskManager to manage
    public void ManageTask(Task task)
    {
        _tasks.Add(task);
    }

    private Queue<Task> _tasksToRemove = new Queue<Task>();

    private void Update()
    {
        foreach (Task task in _tasks)
        { 
            TaskHolder holder = task.FindTaskHolder();

            if (holder != null)
            {
                SendTask(task, holder);
            }
        }

        while (_tasksToRemove.Count > 0)
        {
            Task task = _tasksToRemove.Dequeue();
            _tasks.Remove(task);
        }
    }
}

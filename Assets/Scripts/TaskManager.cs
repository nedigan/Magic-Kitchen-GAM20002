using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    private List<Task> _tasks = new List<Task>();
    private List<TaskHolder> _taskHolders = new List<TaskHolder>();

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
        _tasks.Remove(task);
    }

    // Add task for the TaskManager to manage
    public void ManageTask(Task task)
    {
        _tasks.Add(task);
    }

    private void Update()
    {
        foreach (var task in _tasks)
        { 
            TaskHolder holder = task.FindTaskHolder();

            if (holder != null)
            {
                SendTask(task, holder);
            }
        }
    }
}
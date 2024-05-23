using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System.Linq;

public class TaskManager : MonoBehaviour
{
    public List<Task> _tasks = new List<Task>();

    private List<Animal> _animals = new List<Animal>();
    private List<Station> _stations = new List<Station>();
    public List<Item> _items = new List<Item>();

    private OrderManager _orderManager;

    public List<Animal> Animals => _animals;
    public List<Station> Stations => _stations;
    public List<Item> Items => _items;

    public OrderManager OrderManager => _orderManager;

    private void Awake()
    {
        _orderManager = GetComponent<OrderManager>();
    }

    // Give a task to a task holder
    private void SendTask(Task task, TaskHolder holder)
    {
        holder.SetTask(task, true);
        _tasksToRemove.Enqueue(task);
    }

    public int NumFoxesInRestaurant()
    {
        return _animals.Count(a => a.Type == AnimalType.Fox && a.CurrentRoom.RoomType == RoomType.Dining);
    }

    // Add task for the TaskManager to manage
    public void ManageTask(Task task)
    {
        _tasksToAdd.Enqueue(task);
    }

    private Queue<Task> _tasksToRemove = new Queue<Task>();

    private Queue<Task> _tasksToAdd = new Queue<Task>();
    private void Update()
    {

        while (_tasksToAdd.Count > 0)
        {
            Task task = _tasksToAdd.Dequeue();
            _tasks.Add(task);
        }

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

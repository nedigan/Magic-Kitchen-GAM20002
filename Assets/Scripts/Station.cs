using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StationType
{
    Stove, //for cooking
    Shelf, //for grabbing ingredients
    Table //for serving food
}

[RequireComponent(typeof(TaskHolder))]
public class Station : MonoBehaviour
{
    public StationType Type;
    public bool Occupied = false;
    public GameObject StandLocation;
    public TaskHolder TaskHolder;

    private void Start()
    {
        TaskManager manager = FindFirstObjectByType<TaskManager>();
        if (manager != null) { manager.Stations.Add(this); }
    }
}

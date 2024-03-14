using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Animal : MonoBehaviour
{
    [SerializeField] protected Task[] _taskTypes;
    protected Queue<Task> tasks = new Queue<Task>();
    public abstract void PerformNextTask();
    
}

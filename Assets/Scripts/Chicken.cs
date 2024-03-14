using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chicken : Animal
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Hello");
        tasks.Enqueue(_taskTypes[0]);
        PerformNextTask();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void PerformNextTask()
    {
        Task task = tasks.Dequeue();
        Debug.Log($"{gameObject.name} is completing the {task.name} task. \nIt will take {task.Duration} seconds long.");
    }
}

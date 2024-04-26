using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DeliverMeal : Task
{
    private Animal _turtle;
    private Animal _fox;

    public void SetUp(Animal turtle, Animal fox)
    {
        _turtle = turtle;
        _fox = fox; 
    }
    public override TaskHolder FindTaskHolder()
    {
        //throw new System.NotImplementedException();
        return null;
    }

    public override void FinishTask()
    {
        //throw new System.NotImplementedException();
        _turtle.TaskHolder.RemoveCurrentTask();
        _turtle.ReachedDestination -= FinishTask;
        MoneyHandler.AddMoney(20); // TODO: Change price based on meal
        Debug.Log("Delivered meal to fox");
    }

    public override void PerformTask()
    {
        //throw new System.NotImplementedException();
    }

    public override void StartTask()
    {
        if (_turtle.SetDestination(_fox))
        {
            _turtle.ReachedDestination += FinishTask;
        }

    }
}

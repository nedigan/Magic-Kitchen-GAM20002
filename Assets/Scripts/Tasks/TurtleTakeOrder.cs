using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleTakeOrder : Task
{
    private Animal _turtle;
    private Animal _fox;

    public void Setup(Animal fox)
    {
        _fox = fox;
    }

    public override TaskHolder FindTaskHolder()
    {
        Animal turtle = FindIdleAnimalOfType(AnimalType.Turtle);

        if (turtle != null) 
        { 
            _turtle = turtle;
            return _turtle.TaskHolder;
        }

        return null;
    }

    public override void FinishTask()
    {
        TurtleReturnOrder returnOrder = ScriptableObject.CreateInstance<TurtleReturnOrder>();
        _turtle.ReachedDestination -= this.FinishTask;
        Debug.Log("Create instance of new task...");
        returnOrder.Setup(_turtle);
        _turtle.TaskHolder.SetTask(returnOrder);
    }

    public override void PerformTask()
    {
        //throw new System.NotImplementedException();
    }

    public override void StartTask()
    {
        Debug.Log("Going to take fox order");
        if (_turtle.SetDestination(_fox))
        {
            _turtle.ReachedDestination += FinishTask;
        }
    }
}

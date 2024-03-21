using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleGrabMeal : Task
{
    private Animal _turtle;
    private Animal _fox;
    private Station _stove;

    public void SetUp(Station stove, Animal fox)
    {
        _fox = fox;
        _stove = stove;
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
        //throw new System.NotImplementedException();
        _turtle.ReachedDestination -= this.FinishTask;
        _turtle.TaskHolder.RemoveCurrentTask();

        DeliverMeal deliverMeal = ScriptableObject.CreateInstance<DeliverMeal>();
        deliverMeal.SetUp(_turtle, _fox);
        _turtle.TaskHolder.SetTask(deliverMeal);
    }

    public override void PerformTask()
    {
        //throw new System.NotImplementedException();
    }

    public override void StartTask()
    {
        if (_turtle.SetDestination(_stove))
        {
            _turtle.ReachedDestination += FinishTask;
        }
    }
}

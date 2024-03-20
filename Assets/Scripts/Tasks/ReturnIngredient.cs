using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnIngredient : Task
{
    private Animal _chicken;
    private Station _stove;
    public void SetUp(Animal chicken, Station station)
    {
        _chicken = chicken;
        _stove = station;
    }
    public override TaskHolder FindTaskHolder()
    {
        return null;
    }

    public override void FinishTask()
    {
        _chicken.ReachedDestination -= this.FinishTask;
        _chicken.Agent.isStopped = true;

        // SUPER HACKY but trying to get it to work
        Task task = _stove.GetComponent<TaskHolder>().Task;
        if (task != null)
        {
            if (task is RequestIngredients)
            {
                RequestIngredients request = task as RequestIngredients;
                request.DeliverIngredient();
            }
        }
    }

    public override void PerformTask()
    {
       // throw new System.NotImplementedException();
    }

    public override void StartTask()
    {
        Debug.Log("Trying to return to kicthen");
        if (_chicken.SetDestination(_stove))
        {
            _chicken.ReachedDestination += FinishTask;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnIngredient : Task
{
    private Animal _chicken;
    private RequestIngredients _requester;
    private Item _item;

    public void SetUp(Animal chicken, RequestIngredients requester, Item item)
    {
        _chicken = chicken;
        _requester = requester;
        _item = item;
    }

    public override TaskHolder FindTaskHolder()
    {
        return null;
    }

    public override void FinishTask()
    {
        _chicken.ReachedDestination -= this.FinishTask;
        _chicken.Agent.isStopped = true;
        _chicken.TaskHolder.RemoveCurrentTask();
        
        _chicken.RemoveCurrentItem();
        
        _requester.DeliverIngredient(_item);

        // SUPER HACKY but trying to get it to work
        //Task task = _stove.GetComponent<TaskHolder>().Task;
        //if (task != null)
        //{
        //    if (task is RequestIngredients)
        //    {
        //        Debug.Log("Delivered ingredient");
        //        RequestIngredients request = task as RequestIngredients;
        //        request.DeliverIngredient();
        //    }
        //}

        Debug.Log("Delivered ingredient");
    }

    public override void PerformTask()
    {
       // throw new System.NotImplementedException();
    }

    public override void StartTask()
    {
        if (_chicken.SetDestination(_requester.Stove))
        {
            _chicken.ReachedDestination += FinishTask;
        }
    }
}

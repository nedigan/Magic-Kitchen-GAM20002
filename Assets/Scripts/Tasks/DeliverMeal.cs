using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class DeliverMeal : Task
{
    private Animal _turtle;

    private OrderTicket _ticket;

    public void SetUp(Animal turtle, OrderTicket ticket)
    {
        _turtle = turtle;
        _ticket = ticket;
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
        Debug.Log($"Delivered {_ticket.Meal} to fox");

        // Turtle drop Meal Item
        // this should eventually hand it off to the Fox Recipient
        _turtle.DropCurrentItemOnGround();
        Destroy(_ticket.Meal.gameObject);
        
        MoneyHandler.AddMoney(20); // TODO: Change price based on meal

        FoxExit task = ScriptableObject.CreateInstance<FoxExit>();
        task.Setup(_ticket.Recipient);
        _ticket.Recipient.TaskHolder.SetTask(task);
    }

    public override void PerformTask()
    {
        //throw new System.NotImplementedException();
    }

    public override void StartTask()
    {
        if (_turtle.SetDestination(_ticket.Recipient))
        {
            _turtle.ReachedDestination += FinishTask;
            Debug.LogWarning("Subscribing deliver meal");
        }
    }
}

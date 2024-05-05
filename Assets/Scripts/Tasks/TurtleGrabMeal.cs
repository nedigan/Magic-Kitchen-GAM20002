using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleGrabMeal : Task
{
    private Animal _turtle;
    private OrderTicket _ticket;

    public void SetUp(OrderTicket ticket)
    {
        _ticket = ticket;
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

        // Turtle pick up Meal Item
        _turtle.PickUpItem(_ticket.Meal);

        DeliverMeal deliverMeal = ScriptableObject.CreateInstance<DeliverMeal>();
        deliverMeal.SetUp(_turtle, _ticket);
        _turtle.TaskHolder.SetTask(deliverMeal);

        UnsetTaskThought(_turtle.ThoughtManager);
    }

    public override void PerformTask()
    {
        //throw new System.NotImplementedException();
    }

    public override void StartTask()
    {
        if (_turtle.SetDestination(_ticket.Meal))
        {
            _turtle.ReachedDestination += FinishTask;
        }

        _ticket.Meal.Claimed = true;

        SetTaskThought(_turtle.ThoughtManager, Thought.FromThinkable(_ticket.Meal).SetEmotion(ThoughtEmotion.Neutral));
    }
}

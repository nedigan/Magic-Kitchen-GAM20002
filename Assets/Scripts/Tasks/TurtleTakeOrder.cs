using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleTakeOrder : Task
{
    private Animal _turtle;
    private OrderTicket _ticket;

    private Thought _thought;

    public void Setup(OrderTicket ticket)
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
        TurtleReturnOrder returnOrder = ScriptableObject.CreateInstance<TurtleReturnOrder>();
        _turtle.ReachedDestination -= this.FinishTask;
        //Debug.Log("Create instance of new task...");
        returnOrder.Setup(_turtle, _ticket);
        _turtle.TaskHolder.SetTask(returnOrder);

        _turtle.ThoughtManager.StopThinkingAbout(_thought);
    }

    public override void PerformTask()
    {
        //throw new System.NotImplementedException();
    }

    public override void StartTask()
    {
        Debug.Log("Going to take fox order");
        if (_turtle.SetDestination(_ticket.Recipient))
        {
            _turtle.ReachedDestination += FinishTask;
        }

        _thought = _turtle.ThoughtManager.ThinkAbout(Thought.FromThinkable(_ticket.Recipient).SetEmotion(ThoughtEmotion.Neutral));
    }
}

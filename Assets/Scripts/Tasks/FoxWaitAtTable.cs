using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Intermediary task for a fox to wait for its order to be taken and for its meal to arrive
// TODO: set the WaitTimes and visuals for them in the editor,
// move to Task to eat a Chicken when this Task is finished
public class FoxWaitAtTable : Task
{
    private Animal _fox;
    private Station _table;

    //private float _timeWaited = 0;
    
    //Wait times, in s
    //public List<float> WaitTimes = new List<float>();
    //public int WaitStage = 0;

    public void Setup(Animal fox, Station table)
    {
        _fox = fox;
        _table = table;
    }

    public override TaskHolder FindTaskHolder()
    {
        return null;
    }

    // move onto eating a Chicken
    public override void FinishTask()
    {
        _fox.ThoughtManager.StopThinking();
        _fox.TaskHolder.RemoveCurrentTask();

        FoxExit task = ScriptableObject.CreateInstance<FoxExit>();
        task.Setup(_fox);
        _fox.TaskHolder.SetTask(task);
    }

    public override void PerformTask()
    {
        // Uncomment this when you are implimenting waiting

        //_timeWaited += Time.deltaTime;
        //if (_timeWaited > WaitTimes[WaitStage])
        //{
        //    NextWaitStage();
        //}
    }

    public override void StartTask()
    {
        OrderTicket ticket = Manager.OrderManager.GenerateRandomOrderTicket(_fox);
        ticket.Task = this;
        Debug.Log($"Fox ordered {ticket.Recipe.Result}");

        TurtleTakeOrder turtleTakeOrder = ScriptableObject.CreateInstance<TurtleTakeOrder>();
        turtleTakeOrder.Setup(ticket);
        Manager.ManageTask(turtleTakeOrder);

        _fox.ThoughtManager.ThinkAbout(Thought.FromThinkable(ticket.Recipe).SetEmotion(ThoughtEmotion.Neutral));
    }

    //private void NextWaitStage()
    //{

    //    WaitStage++;

    //    if (WaitStage > WaitTimes.Count)
    //    {
    //        FinishTask();
    //    }

    //    _timeWaited = 0;
    //}

    public void DeliverMeal(OrderTicket ticket)
    {
        Destroy(ticket.Meal.gameObject);

        MoneyHandler.AddMoney(20); // TODO: Change price based on meal
        _table.Occupied = false;

        FinishTask();
    }
}

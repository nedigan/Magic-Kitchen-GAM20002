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
        //throw new System.NotImplementedException();
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
        Debug.Log($"Fox ordered {ticket.Recipe.Result}");

        TurtleTakeOrder turtleTakeOrder = ScriptableObject.CreateInstance<TurtleTakeOrder>();
        turtleTakeOrder.Setup(ticket);
        Manager.ManageTask(turtleTakeOrder);
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
}

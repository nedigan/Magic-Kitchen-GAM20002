using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxFindTable : Task
{
    private Animal _fox;
    private Station _table;

    public override TaskHolder FindTaskHolder()
    {
        Animal fox = FindIdleAnimalOfType(AnimalType.Fox);

        if (fox == null) { return null; }

        Station table = FindEmptyStationOfType(StationType.Table);

        if (table == null) { return null; }

        _fox = fox;
        _table = table;
        return _fox.TaskHolder;
    }

    public override void StartTask()
    {
        // send the fox to the table
        _fox.SetDestination(_table);
        // set to finish the task when the Fox has reached the table
        _fox.ReachedDestination += FinishTask;
        // claim the table
        _table.Occupied = true;
    }

    public override void PerformTask()
    {
        //throw new System.NotImplementedException();
    }

    public override void FinishTask()
    {
        _fox.ReachedDestination -= FinishTask;
        FoxWaitAtTable task = ScriptableObject.CreateInstance<FoxWaitAtTable>();
        task.Setup(_fox, _table);

        _table.TaskHolder.SetTask(task);
    }
}

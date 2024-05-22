using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxExit : Task
{
    private Animal _fox;

    public void Setup(Animal fox)
    {
        _fox = fox;
    }
    public override TaskHolder FindTaskHolder()
    {
        return null;
    }

    public override void FinishTask()
    {
        _fox.TaskHolder.RemoveCurrentTask();
        _fox.ReachedDestination -= FinishTask;

        Destroy(_fox.transform.parent.gameObject);
    }

    public override void PerformTask()
    {
    }

    public override void StartTask()
    {
        // BEWARE: assumes only one exit exists in the scene, any others will never be seen by this
        Station exit = FindEmptyStationOfType(StationType.FoxExit);
        if (exit != null)
        {
            // purposely not calling window.Occupied = true so that more than one Turtle can use the Window at a time
            if (_fox.SetDestination(exit)) // returns true if in current room
            {
                _fox.ReachedDestination += FinishTask;
            }
        }
        else
        {
            Debug.LogErrorFormat("FoxExit could not find a FoxExit Station! The entire game is now broken. You should really fix this");
        }
    }
}

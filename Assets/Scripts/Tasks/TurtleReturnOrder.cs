using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleReturnOrder : Task
{
    private Animal _turtle;
    private Station _window;

    public void Setup(Animal turtle)
    {
        _turtle = turtle;
    }

    public override TaskHolder FindTaskHolder()
    {
        //throw new System.NotImplementedException();
        return null;
    }

    public override void FinishTask()
    {
        _turtle.TaskHolder.RemoveCurrentTask();
    }

    public override void PerformTask()
    {
        //throw new System.NotImplementedException();
    }

    public override void StartTask()
    {
        // BEWARE: assumes only one window exists in the scene, any others will never be seen by this
        Station window = FindEmptyStationOfType(StationType.Window);
        if (window != null)
        {
            _window = window;
            // purposely not calling window.Occupied = true so that more than one Turtle can use the Window at a time

            _turtle.SetDestination(window);
            _turtle.ReachedDestination += FinishTask;
        }
        else
        {
            Debug.LogErrorFormat("TurtleReturnOrder could not find a Window Station! The entire game is now broken. You should really fix this");
        }
    }
}

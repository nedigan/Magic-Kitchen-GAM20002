using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wait : Task
{
    private float _waitTime = 1;
    private float _waitCounter = 0;

    public override TaskHolder FindTaskHolder()
    {
        return null;
    }

    public override void FinishTask()
    {
       Holder.RemoveCurrentTask();
    }

    public override void PerformTask()
    {
        _waitCounter += Time.deltaTime;

        if (_waitCounter >= _waitTime) { FinishTask(); }
    }

    public override void StartTask()
    {
        
    }

    public void Setup(float waitTime)
    {
        _waitTime = waitTime;
    }
}

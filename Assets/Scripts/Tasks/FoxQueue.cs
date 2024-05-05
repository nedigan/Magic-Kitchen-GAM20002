using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxQueue : Task
{
    private Animal _fox;
    public static List<Animal> AnimalsInQueue = new List<Animal>();

    public void Setup(Animal fox) { _fox = fox; }
    public override TaskHolder FindTaskHolder()
    {
        return null;
    }

    public override void FinishTask()
    {

    }

    public override void PerformTask()
    {
        
    }

    public override void StartTask()
    {
        AnimalsInQueue.Add(_fox);

        _fox.SetDestination(RoomType.Dining, false);
        // The false here indicates that the fox will not wait for a connection to the door first,
        // the fox will just go to the first door regardless. Which for the sidewalk there is only one anyway
    }
}

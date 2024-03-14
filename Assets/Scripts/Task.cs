using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Task : MonoBehaviour
{
    public abstract TaskHolder FindTaskHolder();
    public abstract void PerformTask();
}

public class CollectIngredient: Task
{

    public override TaskHolder FindTaskHolder()
    {
        // Find a chicken task holder
        return null;
    }

    public override void PerformTask()
    {
        // 
        throw new System.NotImplementedException();
    }
}



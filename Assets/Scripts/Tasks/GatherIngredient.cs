using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatherIngredient : Task
{
    private Animal _chicken;
    private Station _stove;

    public void SetUp(Station stove) { _stove = stove; }   
    public override TaskHolder FindTaskHolder()
    {
        Animal chicken = FindIdleAnimalOfType(AnimalType.Chicken);

        if (chicken != null)
        {
            _chicken = chicken;
            return _chicken.TaskHolder;
        }
        return null;
    }

    public override void FinishTask()
    {
        ReturnIngredient returnIngredientToStove = ScriptableObject.CreateInstance<ReturnIngredient>();
        _chicken.ReachedDestination -= this.FinishTask;
        returnIngredientToStove.SetUp(_chicken, _stove);
        _chicken.TaskHolder.SetTask(returnIngredientToStove);
        Debug.Log("Got ingredient");
    }
    public override void PerformTask()
    {
       // throw new System.NotImplementedException();
    }

    public override void StartTask()
    {
        Station shelf = FindEmptyStationOfType(StationType.Shelf);
        if (shelf != null)
        {
            Debug.LogWarning("Chicken going to shelf");
            if (_chicken.SetDestination(shelf))
            {
                _chicken.ReachedDestination += FinishTask;
            }
        }
        else
            Debug.LogError("Couldnt find a shelf");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatherIngredient : Task
{
    private Animal _chicken;
    private RequestIngredients _requester;
    private ItemType _itemType;
    private Item _foundItem;

    public void SetUp(RequestIngredients requester, ItemType itemType) 
    { 
        _requester = requester;
        _itemType = itemType;
    }   
    public override TaskHolder FindTaskHolder()
    {
        Animal chicken = FindIdleAnimalOfType(AnimalType.Chicken);
        Item foundItem = FindUnclaimedItemOfType(_itemType);

        if (chicken != null && foundItem != null)
        {
            _chicken = chicken;

            _foundItem = foundItem;
            _foundItem.Claimed = true;

            return _chicken.TaskHolder;
        }

        return null;
    }

    public override void FinishTask()
    {
        _chicken.PickUpItem(_foundItem);
        
        _chicken.ReachedDestination -= this.FinishTask;

        ReturnIngredient returnIngredientToStove = ScriptableObject.CreateInstance<ReturnIngredient>();
        returnIngredientToStove.SetUp(_chicken, _requester, _foundItem);
        _chicken.TaskHolder.SetTask(returnIngredientToStove);
        
        Debug.Log("Got ingredient");
    }
    public override void PerformTask()
    {
       // throw new System.NotImplementedException();
    }

    public override void StartTask()
    {
        if (_chicken.SetDestination(_foundItem))
        {
            _chicken.ReachedDestination += this.FinishTask;
        }

        //Station shelf = FindEmptyStationOfType(StationType.Shelf);
        //if (shelf != null)
        //{
        //    Debug.Log("Chicken going to shelf");
        //    if (_chicken.SetDestination(shelf))
        //    {
        //        _chicken.ReachedDestination += FinishTask;
        //    }
        //}
        //else
        //    Debug.LogError("Couldnt find a shelf");
    }
}

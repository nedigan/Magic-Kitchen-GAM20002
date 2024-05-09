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
        if (chicken == null) { Debug.LogError("Couldn't find Chicken"); return null; }

        Item foundItem = FindUnclaimedItemOfType(_itemType);
        if (foundItem == null) { Debug.LogError($"Couldn't find Item of Type {_itemType}"); return null; }

        _chicken = chicken;

        _foundItem = foundItem;
        _foundItem.Claimed = true;

        return _chicken.TaskHolder;
    }

    public override void FinishTask()
    {
        _chicken.PickUpItem(_foundItem);
        
        _chicken.ReachedDestination -= this.FinishTask;

        ReturnIngredient returnIngredientToStove = ScriptableObject.CreateInstance<ReturnIngredient>();
        returnIngredientToStove.SetUp(_chicken, _requester, _foundItem);
        _chicken.TaskHolder.SetTask(returnIngredientToStove);
        
        Debug.Log("Got ingredient");

        UnsetTaskThought();
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

        SetTaskThought(_chicken.ThoughtManager, Thought.FromThinkable(_foundItem).SetEmotion(ThoughtEmotion.Neutral));
    }

    protected override void OnCancelTask()
    {
        base.OnCancelTask();

        _foundItem.Claimed = false;

        _chicken.ReachedDestination -= FinishTask;
    }
}

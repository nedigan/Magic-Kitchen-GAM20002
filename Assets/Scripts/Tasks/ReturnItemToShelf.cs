using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnItemToShelf : Task
{
    private Item _item;
    private Animal _animal;

    public void SetUp(Item item, Animal animal)
    {
        _item = item;
        _animal = animal;
    }

    public override TaskHolder FindTaskHolder()
    {
        return null;
    }

    public override void FinishTask()
    {
        _item.Claimed = false;

        _animal.ItemHolder.RemoveCurrentItem();
        _item.ShelfSpot.Station.ItemHolder.PickUpItem(_item);

        _animal.ReachedDestination -= FinishTask;

        _animal.TaskHolder.RemoveCurrentTask();
    }

    public override void PerformTask()
    {
    }

    public override void StartTask()
    {
        if (_animal.SetDestination(_item.ShelfSpot.Station))
        {
            _animal.ReachedDestination += FinishTask;
        }
    }
}

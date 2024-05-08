using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelivererCollect : Task
{
    private DeliveryTruck _deliveryTruck;
    private Item _delivery;

    private Animal _deliverer;

    public void SetUp(DeliveryTruck deliveryTruck, Item delivery)
    {
        _deliveryTruck = deliveryTruck;
        _delivery = delivery;
    }

    public override TaskHolder FindTaskHolder()
    {
        // first idle animal under StoreRoom's Deliverers
        foreach (Animal animal in _deliveryTruck.Deliverers)
        {
            if (animal.TaskHolder.PerformingTask == false)
            {
                _deliverer = animal;
                return animal.TaskHolder;
            }
        }

        return null;
    }

    public override void FinishTask()
    {
        _deliverer.PickUpItem(_delivery);

        _deliverer.ReachedDestination -= FinishTask;

        // set new task
        ReturnItemToShelf returnItemToShelf = CreateInstance<ReturnItemToShelf>();
        returnItemToShelf.SetUp(_delivery, _deliverer);
        _deliverer.TaskHolder.SetTask(returnItemToShelf);

        UnsetTaskThought();
    }

    public override void PerformTask()
    {
        
    }

    public override void StartTask()
    {
        if (_deliverer.SetDestination(_delivery))
        {
            _deliverer.ReachedDestination += FinishTask;
        }

        SetTaskThought(_deliverer.ThoughtManager, Thought.FromThinkable(_delivery).SetEmotion(ThoughtEmotion.Neutral));
    }
}

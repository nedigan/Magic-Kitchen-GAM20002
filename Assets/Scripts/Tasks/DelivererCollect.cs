using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelivererCollect : Task
{
    private DeliveryTruck _deliveryTruck;
    private Item _delivery;
    private Stack<Item> _itemStack;

    private Animal _deliverer;

    public void SetUp(DeliveryTruck deliveryTruck, Item delivery, Stack<Item> itemStack, Animal deliverer = null)
    {
        _deliveryTruck = deliveryTruck;
        _delivery = delivery;
        _itemStack = itemStack;
        if (deliverer != null) { _deliverer = deliverer; }
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
        if (_itemStack.Count > 0 )
        {
            Item item = _itemStack.Pop();

            DelivererCollect delivererCollect = CreateInstance<DelivererCollect>();
            delivererCollect.SetUp(_deliveryTruck, item, _itemStack, _deliverer);
            _deliverer.TaskHolder.SetTask(delivererCollect);
        }
        else
        {
            ReturnItemToShelf returnItemToShelf = CreateInstance<ReturnItemToShelf>();
            returnItemToShelf.SetUp(_delivery, _deliverer);
            _deliverer.TaskHolder.SetTask(returnItemToShelf);
        }

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

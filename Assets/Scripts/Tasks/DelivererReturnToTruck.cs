using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DelivererReturnToTruck : Task
{
    private Animal _deliverer;
    private DeliveryTruck _deliveryTruck;
    private Station _exitSpot;

    public void Setup(Animal deliverer, DeliveryTruck deliveryTruck)
    {
        _deliverer = deliverer;
        _deliveryTruck = deliveryTruck;
    }

    public override TaskHolder FindTaskHolder()
    {
        return null;
    }

    public override void FinishTask()
    {
        _deliverer.ReachedDestination -= FinishTask;

        _deliverer.TaskHolder.DefaultTask = null;
        _deliverer.TaskHolder.RemoveCurrentTask();

        _deliveryTruck.DelivererReadyToLeave();
    }

    public override void PerformTask()
    {
    }

    public override void StartTask()
    {
        IsIdleTask = true;

        if (_exitSpot == null)
        {
            foreach (Station exitSpot in _deliveryTruck.ExitSpots)
            {
                if (exitSpot.Occupied == false)
                {
                    _exitSpot = exitSpot;
                    _exitSpot.Occupied = true;

                    SetAnimalDestinaion(_deliverer, _exitSpot);
                    return;
                }
            }
        }
        else
        {
            SetAnimalDestinaion(_deliverer, _exitSpot);
        }
    }

    protected override void OnCancelTask()
    {
        base.OnCancelTask();

        if (_exitSpot != null) { _exitSpot.Occupied = false; _exitSpot = null; }

        _deliverer.ReachedDestination -= FinishTask;
    }
}

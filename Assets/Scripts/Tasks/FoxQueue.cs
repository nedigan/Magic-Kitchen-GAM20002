using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FoxQueue : Task
{
    private Animal _fox;
    public static List<Animal> AnimalsInQueue = new List<Animal>();
    private NavMeshPath _navMeshPath;
    private float _spacing = 3f;

    private bool _isFrontOfLine = false;

    public void Setup(Animal fox,NavMeshPath path)
    {
        _fox = fox;
        _navMeshPath = path;
    }
    public override TaskHolder FindTaskHolder()
    {
        return null;
    }

    public override void FinishTask()
    {
        _fox.MovedRoom -= this.FinishTask;
        AnimalsInQueue.Remove(_fox);
        _fox.TaskHolder.RemoveCurrentTask();
        Debug.Log("Finished queue task");

        FoxFindTable task = ScriptableObject.CreateInstance<FoxFindTable>();
        task.Manager.ManageTask(task);
    }

    public override void PerformTask()
    {
        if (AnimalsInQueue.Count > 0)
        {
            int index = AnimalsInQueue.IndexOf(_fox);
            if (index > 0)
            {
                _fox.SetDestination(CalculatePositionOnPath(index));
            } 
            else if (index == 0 && !_isFrontOfLine)
            {
                _fox.SetDestination(RoomType.Dining, false);
                _isFrontOfLine=true;
            }
        }
        else
        {
            Debug.LogWarning("The animal queue is zero, this shouldn't be happening");
        }
    }

    public override void StartTask()
    {
        if (!AnimalsInQueue.Contains(_fox))
        {
            _fox.MovedRoom += FinishTask;
            AnimalsInQueue.Add(_fox);
        }
        _isFrontOfLine = false;
    }

    // Imma be honest i got my mate gpt to help with this one
    Vector3 CalculatePositionOnPath(int indexOfFox)
    {
        float distance = indexOfFox * _spacing;
        float totalDistance = 0f;
        Vector3 previousCorner = _navMeshPath.corners[0];

        // Iterate through the corners to find the point at the specified distance
        for (int i = 1; i < _navMeshPath.corners.Length; i++)
        {
            totalDistance += Vector3.Distance(previousCorner, _navMeshPath.corners[i]);

            // If the total distance exceeds the desired distance, interpolate between the corners
            if (totalDistance >= distance)
            {
                float overDistance = totalDistance - distance;
                return Vector3.Lerp(_navMeshPath.corners[i - 1], _navMeshPath.corners[i], 1f - (overDistance / Vector3.Distance(_navMeshPath.corners[i - 1], _navMeshPath.corners[i])));
            }

            previousCorner = _navMeshPath.corners[i];
        }

        // If the specified distance is greater than the total path distance, return the last corner
        return _navMeshPath.corners[_navMeshPath.corners.Length - 1];
    }
}

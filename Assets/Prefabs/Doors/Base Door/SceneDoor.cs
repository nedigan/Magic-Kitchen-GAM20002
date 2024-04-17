using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Collider))]
public class SceneDoor : MonoBehaviour
{
    [SerializeField]
    protected Collider _enteranceCollider;
    [SerializeField] 
    protected GameObject _exitPosition;

    public Collider EnteranceCollider => _enteranceCollider;
    public GameObject ExitPosition => _exitPosition;

    private Door _connectingDoor = null;

    public void OnTriggerEnter(Collider other)
    {
        //if (other.CompareTag("Animal"))
        //{
        //    if (_connectingDoor != null)
        //    {
        //        Debug.Log("Transporting...");
        //        other.GetComponent<NavMeshAgent>().enabled = false;
        //        other.transform.position  = _connectingDoor.SceneDoor.ExitPosition.transform.position;
        //        other.GetComponent<NavMeshAgent>().enabled = true;

        //        // Try task again once in the other room
        //        other.GetComponent<Animal>().CurrentRoom = _connectingDoor.Room;
        //        other.GetComponent<TaskHolder>().ResetTask();
        //        // CHANGE PARENT if you want
        //    }
        //    else
        //    {
        //        //Debug.Log("No connecting door, will not transport animal");
        //    }
        //}

        if (_connectingDoor != null && other.TryGetComponent(out Animal animal))
        {
            animal.MoveToRoom(_connectingDoor);
        }
    }

    public void OnTriggerStay(Collider other)
    {
        //if (other.CompareTag("Animal"))
        //{
        //    if (_connectingDoor != null)
        //    {
        //        //Debug.Log("Transporting...");
        //        other.GetComponent<NavMeshAgent>().enabled = false;
        //        other.transform.position = _connectingDoor.SceneDoor.ExitPosition.transform.position;
        //        other.GetComponent<NavMeshAgent>().enabled = true;

        //        // Try task again once in the other room
        //        other.GetComponent<Animal>().CurrentRoom = _connectingDoor.Room;
        //        other.GetComponent<TaskHolder>().ResetTask();
        //        // CHANGE PARENT if you want
        //    }
        //    else
        //    {
        //       // Debug.Log("No connecting door, will not transport animal");
        //    }
        //}

        if (_connectingDoor != null && other.TryGetComponent(out Animal animal))
        {
            animal.MoveToRoom(_connectingDoor);
        }
    }

    public void OnConnectedTo(Door door)
    {
        _connectingDoor = door;
        Debug.Log(_connectingDoor.Room);
    }

    public void OnDisconnectFrom(Door door)
    {
        _connectingDoor = null;
    }
}

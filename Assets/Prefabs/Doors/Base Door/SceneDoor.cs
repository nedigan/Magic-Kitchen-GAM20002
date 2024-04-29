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

    //public void OnTriggerEnter(Collider other)
    //{
    //    if (_connectingDoor != null && other.TryGetComponent(out Animal animal))
    //    {
    //        animal.MoveToRoom(_connectingDoor);
    //    }
    //}

    //public void OnTriggerStay(Collider other)
    //{
    //    if (_connectingDoor != null && other.TryGetComponent(out Animal animal))
    //    {
    //        animal.MoveToRoom(_connectingDoor);
    //    }
    //}

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

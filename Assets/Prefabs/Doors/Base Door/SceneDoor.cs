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
        if (other.CompareTag("Animal"))
        {
            if (_connectingDoor != null)
            {
                Debug.Log("Try");
                other.GetComponent<NavMeshAgent>().enabled = false;
                other.transform.position  = _connectingDoor.SceneDoor.ExitPosition.transform.position;
                other.GetComponent<NavMeshAgent>().enabled = true;
            }
            else
            {
                Debug.Log("No connecting door, will not transport animal");
            }
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

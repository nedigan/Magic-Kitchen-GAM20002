using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class SceneDoor : MonoBehaviour
{
    [SerializeField]
    protected Collider _enteranceCollider;
    [SerializeField] 
    protected GameObject _exitPosition;

    public Collider EnteranceCollider => _enteranceCollider;
    public GameObject ExitPosition => _exitPosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnConnectedTo(Room room)
    {

    }

    public void OnDisconnectFrom(Room room)
    {

    }
}

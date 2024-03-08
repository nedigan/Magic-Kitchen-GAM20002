using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    protected Room _connectingRoom = null;
    [SerializeField]
    protected SceneDoor _sceneDoor;
    [SerializeField]
    protected TableDoor _tableDoor;

    public Room ConnectingRoom => _connectingRoom;
    public SceneDoor SceneDoor => _sceneDoor;
    public TableDoor TableDoor => _tableDoor;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

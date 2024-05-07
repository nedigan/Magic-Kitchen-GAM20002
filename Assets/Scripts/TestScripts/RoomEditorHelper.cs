using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Room))]
public class RoomEditorHelper : MonoBehaviour
{
    [SerializeField]
    private Room _room;

    [SerializeField]
    private GameObject _sceneRoomWallsAndFloor;
    [SerializeField]
    private BoxCollider _tableRoomCollider;

    [SerializeField]
    private GameObject _sceneDoor;
    [SerializeField]
    private GameObject _tableDoor;

    private void Awake()
    {
        //Validate();
    }

    // Update is called once per frame
    void Update()
    {
        _tableRoomCollider.size = new Vector3(
            _sceneRoomWallsAndFloor.transform.localScale.x * 10, 
            1, 
            _sceneRoomWallsAndFloor.transform.localScale.z * 10);


        _tableDoor.transform.localPosition = new Vector3(
            _sceneDoor.transform.localPosition.x, 
            0f, 
            _sceneDoor.transform.localPosition.z);

        _tableDoor.transform.localRotation = _sceneDoor.transform.localRotation;
    }

    private void Validate()
    {
        Debug.Log("hi");

        object[] toCheck =
        {
            _room,
            _sceneRoomWallsAndFloor,
            _tableRoomCollider,
            _sceneDoor,
            _tableDoor,
        };

        if (_room == null) { _room = GetComponent<Room>(); }

        foreach (object obj in toCheck) { Debug.Log(obj != null); }

        if (toCheck.Any(obj => obj == null))
        { enabled = false; Debug.Log("Failed Validation"); }
        else
        { enabled = true; Debug.Log("Passed Validation"); }
    }

    private void OnValidate()
    {
        //Validate();
    }
}

using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class CustomerSpawner : MonoBehaviour
{
    [SerializeField] private DayManager _dayManager;
    [SerializeField] private GameObject _customer; // fox prefab
    [SerializeField] private float _spawnTimeInterval = 20f;
    [SerializeField] private Transform _targetDoor;
    private NavMeshPath _navMeshPath;

    private int _customerIndex = 0;
    private bool _spawning = true;
    private List<float> _customerSpawnTimes;

    [SerializeField] private Room _room;

    public float SpawnTimeInterval { get { return _spawnTimeInterval; } }


    // Start is called before the first frame update
    void Start()
    {
        _navMeshPath = new NavMeshPath();
        if (!NavMesh.CalculatePath(_targetDoor.position, transform.position, NavMesh.AllAreas, _navMeshPath))
            Debug.LogError("Path couldnt be created");

        _customerSpawnTimes = _dayManager.GetCurrentDayCustomerSpawns();
        Debug.Log(_customerSpawnTimes.Count);
    }

    public void Update()
    {
        if (_spawning && _customerIndex >= _customerSpawnTimes.Count)
            StopSpawning();

        if (_spawning && _dayManager.DayProgress >= _customerSpawnTimes[_customerIndex])
        {
            _customerIndex++;
            SpawnCustomer();
        }
    }

    public void StopSpawning()
    {
        _spawning = false;
    }

    public void SpawnCustomer()
    {
        TaskHolder taskHolder = Instantiate(_customer, transform.position, Quaternion.identity, transform.parent).GetComponentInChildren<TaskHolder>();
        FoxQueue task = ScriptableObject.CreateInstance<FoxQueue>();

        Animal animal = taskHolder.gameObject.GetComponent<Animal>(); // yucky
        animal.SetCurrentRoom(_room);
        task.Setup(animal, _navMeshPath); 
        taskHolder.SetTask(task);
    }
}

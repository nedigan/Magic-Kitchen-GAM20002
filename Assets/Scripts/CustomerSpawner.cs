using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class CustomerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _customer; // fox prefab
    [SerializeField] private float _spawnTimeInterval = 20f;
    [SerializeField] private Transform _targetDoor;
    private NavMeshPath _navMeshPath;

    public float SpawnTimeInterval { get { return _spawnTimeInterval; } }


    // Start is called before the first frame update
    void Start()
    {
        _navMeshPath = new NavMeshPath();
        if (!NavMesh.CalculatePath(_targetDoor.position, transform.position, NavMesh.AllAreas, _navMeshPath))
            Debug.LogError("Path couldnt be created");

        StartCoroutine(SpawnCustomer());
    }

    public void SetSpawnInterval(float interval, bool instantlySpawn)
    {
        _spawnTimeInterval = interval;
        if (instantlySpawn)
        {
            StopAllCoroutines();
            StartCoroutine(SpawnCustomer());
        }
    }

    IEnumerator SpawnCustomer()
    {
        TaskHolder taskHolder = Instantiate(_customer, transform.position, Quaternion.identity, transform.parent).GetComponentInChildren<TaskHolder>();
        FoxQueue task = ScriptableObject.CreateInstance<FoxQueue>();

        task.Setup(taskHolder.gameObject.GetComponent<Animal>(), _navMeshPath); // yucky
        taskHolder.SetTask(task);

        yield return new WaitForSeconds(_spawnTimeInterval);

        StartCoroutine(SpawnCustomer());
    }
}

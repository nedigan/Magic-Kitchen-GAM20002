using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _customer; // fox prefab
    [SerializeField] private float _spawnTimeInterval = 20f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnCustomer());
    }

    IEnumerator SpawnCustomer()
    {
        TaskHolder taskHolder = Instantiate(_customer, transform.position, Quaternion.identity, transform.parent).GetComponentInChildren<TaskHolder>();
        FoxQueue task = ScriptableObject.CreateInstance<FoxQueue>();

        task.Setup(taskHolder.gameObject.GetComponent<Animal>()); // yucky
        taskHolder.SetTask(task);

        yield return new WaitForSeconds(_spawnTimeInterval);

        StartCoroutine(SpawnCustomer());
    }
}

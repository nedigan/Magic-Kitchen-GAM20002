using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animal))]
public class FoxTester : MonoBehaviour
{
    [SerializeField]
    private Animal fox;

    // Start is called before the first frame update
    void Start()
    {
        //fox.TaskHolder.SetTask(ScriptableObject.CreateInstance<FoxFindTable>());
        FoxFindTable task = ScriptableObject.CreateInstance<FoxFindTable>();
        task.Manager.ManageTask(task);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

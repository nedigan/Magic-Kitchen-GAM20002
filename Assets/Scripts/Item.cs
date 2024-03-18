using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        TaskManager manager = FindFirstObjectByType<TaskManager>();
        if (manager != null) { manager.Items.Add(this); }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

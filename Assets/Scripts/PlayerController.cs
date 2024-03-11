using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    // THIS SCRIPT IS FOR NAVMESH TESTING, THE ACTUAL AI WILL BE CONTROLLED DIFFERENTLY

    [SerializeField] private Camera _cam;
    [SerializeField] private NavMeshAgent _agent;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = _cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // Move agent
                _agent.SetDestination(hit.point);
            }
        }
    }
}

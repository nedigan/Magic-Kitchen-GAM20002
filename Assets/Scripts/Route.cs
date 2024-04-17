using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Route : MonoBehaviour
{
    public Transform[] controlPoints;

    [SerializeField] private LineRenderer _lineRenderer;
    private Vector3 gizmosPosition;

    private void OnDrawGizmos()
    {
        // Took from this https://youtu.be/11ofnLOE8pw?si=4upce-gRrI13-g-i

        _lineRenderer.positionCount = 20;
        int index = 0;
        for (float t = 0; t <= 1; t += 0.05f)
        {
            gizmosPosition = Mathf.Pow(1 - t, 3) * controlPoints[0].position + 3 * Mathf.Pow(1 - t, 2) * t * controlPoints[1].position + 3 * (1 - t) * Mathf.Pow(t, 2) * controlPoints[2].position + Mathf.Pow(t, 3) * controlPoints[3].position;

            //Gizmos.DrawSphere(gizmosPosition, 0.25f);

            _lineRenderer.SetPosition(index++, gizmosPosition);
        }

        Gizmos.DrawLine(new Vector3(controlPoints[0].position.x, controlPoints[0].position.y, controlPoints[0].position.z), new Vector3(controlPoints[1].position.x, controlPoints[1].position.y, controlPoints[1].position.z));
        Gizmos.DrawLine(new Vector3(controlPoints[2].position.x, controlPoints[2].position.y, controlPoints[2].position.z), new Vector3(controlPoints[3].position.x, controlPoints[3].position.y, controlPoints[3].position.z));

    }

    public void Update()
    {
        _lineRenderer.positionCount = 20;
        int index = 0;
        for (float t = 0; t <= 1; t += 0.05f)
        {
            gizmosPosition = Mathf.Pow(1 - t, 3) * controlPoints[0].position + 3 * Mathf.Pow(1 - t, 2) * t * controlPoints[1].position + 3 * (1 - t) * Mathf.Pow(t, 2) * controlPoints[2].position + Mathf.Pow(t, 3) * controlPoints[3].position;

            //Gizmos.DrawSphere(gizmosPosition, 0.25f);

            _lineRenderer.SetPosition(index++, gizmosPosition);
        }
    }
}
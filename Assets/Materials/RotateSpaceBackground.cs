using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSpaceBackground : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0.005f, 0);
    }
}

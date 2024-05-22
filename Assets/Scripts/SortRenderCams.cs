using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class SortRenderCams : MonoBehaviour
{
    private UniversalAdditionalCameraData _URPCam;
    private Dictionary<Camera, MoveRenderCam> _cameraPairs = new Dictionary<Camera, MoveRenderCam>();
    private void Start()
    {
        _URPCam = GetComponent<UniversalAdditionalCameraData>();

        foreach (Camera camera in _URPCam.cameraStack)
        {
            if (camera.CompareTag("MainCamera"))
                continue;
            _cameraPairs.Add(camera, camera.GetComponent<MoveRenderCam>());
        }
    }
    // Update is called once per frame
    void Update()
    {
        _URPCam.cameraStack.Sort(CompareViewPortYPos);
    }

    private int CompareViewPortYPos(Camera x, Camera y)
    {
        if (x.CompareTag("MainCamera"))
            return -1;
        if (y.CompareTag("MainCamera"))
            return 1;

        if (_cameraPairs[x].ViewPortPos.y == _cameraPairs[y].ViewPortPos.y)
            return 0;
        else if (_cameraPairs[x].ViewPortPos.y < _cameraPairs[y].ViewPortPos.y)
        {
            return 1;
        }
        else
            return -1;
    }
}

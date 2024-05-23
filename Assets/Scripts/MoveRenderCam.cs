using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class MoveRenderCam : MonoBehaviour
{
    [SerializeField] private Transform _camRoom;
    [SerializeField] private Transform _physicalRoom;
    [SerializeField] private Camera _mainCamera;

    public Vector3 ViewPortPos = Vector3.zero;

    public Camera Camera { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        if (_mainCamera == null) { _mainCamera = Camera.main; }
        
        Camera = GetComponent<Camera>();    
        //Debug.Log(_mainCamera.WorldToViewportPoint(_physicalRoom.position));
        //Debug.Log(_camera.WorldToViewportPoint(_camRoom.position));
    }

    // Update is called once per frame
    void Update()
    {
        //_camRoom.position = _camera.ViewportToWorldPoint(_mainCamera.WorldToViewportPoint(_physicalRoom.position));
        //Debug.Log(_mainCamera.WorldToViewportPoint(_physicalRoom.position));
        ViewPortPos = (_mainCamera.WorldToViewportPoint(_physicalRoom.position));

        float height = 2f* Camera.orthographicSize;
        float width = height * Camera.aspect;
        transform.localPosition = new Vector3((-ViewPortPos.x * width) + width / 2f, (-ViewPortPos.y * height) +  height / 2f, transform.localPosition.z);

        //_camera.depth = 100 - (ViewPortPos.y * 100);
        //UniversalAdditionalCameraData mainURPCamera =  _mainCamera.GetUniversalAdditionalCameraData();
        //mainURPCamera.cameraStack.Sort()
    }
}

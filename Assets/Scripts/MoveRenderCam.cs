using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRenderCam : MonoBehaviour
{
    [SerializeField] private Transform _camRoom;
    [SerializeField] private Transform _physicalRoom;
    [SerializeField] private Camera _mainCamera;

    private Camera _camera;
    // Start is called before the first frame update
    void Start()
    {
        if (_mainCamera == null) { _mainCamera = Camera.main; }
        
        _camera = GetComponent<Camera>();    
        //Debug.Log(_mainCamera.WorldToViewportPoint(_physicalRoom.position));
        //Debug.Log(_camera.WorldToViewportPoint(_camRoom.position));
    }

    // Update is called once per frame
    void Update()
    {
        //_camRoom.position = _camera.ViewportToWorldPoint(_mainCamera.WorldToViewportPoint(_physicalRoom.position));
        //Debug.Log(_mainCamera.WorldToViewportPoint(_physicalRoom.position));
        Vector3 pos = (_mainCamera.WorldToViewportPoint(_physicalRoom.position));

        float height = 2f* _camera.orthographicSize;
        float width = height * _camera.aspect;
        transform.localPosition = new Vector3((-pos.x * width) + width / 2f, (-pos.y * height) +  height / 2f, transform.localPosition.z);

        _camera.depth = 100 - (pos.y * 100);
    }
}

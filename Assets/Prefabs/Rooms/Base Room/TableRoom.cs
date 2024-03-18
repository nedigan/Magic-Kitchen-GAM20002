using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class TableRoom : MonoBehaviour
{
    [SerializeField]
    private Collider GrabCollider;
    [SerializeField] 
    private TableDoor _tableDoor;
    [SerializeField]
    private int _xBounds;
    [SerializeField]
    private int _zBounds;

    private Vector3 _mousePos;

    private Rigidbody _rb;
    private Vector3 _targetPos;
    private float _followSpeed = 10f;
    private bool _moving = false;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_moving)
        {
            _rb.velocity = (_targetPos - transform.position) * _followSpeed;
        }
    }

    private Vector3 GetMousePos()
    {
        return Camera.main.WorldToScreenPoint(transform.position);
    }

    private void OnMouseDown()
    {
        _mousePos = Input.mousePosition - GetMousePos();
        _moving = true;
    }

    private void OnMouseDrag()
    {
        _targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition - _mousePos);
        //ClampPosition();
    }

    private void OnMouseUp()
    {
        _moving = false;
        _rb.velocity = Vector3.zero;
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    //ClampPosition();
    //}
    //
    //private void ClampPosition()
    //{
    //    transform.position = new Vector3(transform.position.x, 0, transform.position.z);
    //
    //    if (transform.position.x > _xBounds)
    //    {
    //        transform.position = new Vector3(_xBounds, transform.position.y, transform.position.z);
    //    }
    //    else if (transform.position.x < -_xBounds)
    //    {
    //        transform.position = new Vector3(-_xBounds, transform.position.y, transform.position.z);
    //    }
    //
    //    if (transform.position.z > _zBounds)
    //    {
    //        transform.position = new Vector3(transform.position.x, transform.position.y, _zBounds);
    //    }
    //    else if (transform.position.z < -_zBounds)
    //    {
    //        transform.position = new Vector3(transform.position.x, transform.position.y, -_zBounds);
    //    }
    //}
}

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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private Vector3 GetMousePos()
    {
        return Camera.main.WorldToScreenPoint(transform.position);
    }

    private void OnMouseDown()
    {
        _mousePos = Input.mousePosition - GetMousePos();
    }

    private void OnMouseDrag()
    {
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition - _mousePos);
        ClampPosition();
    }

    private void OnCollisionEnter(Collision collision)
    {
        ClampPosition();
    }

    private void ClampPosition()
    {
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);

        if (transform.position.x > _xBounds)
        {
            transform.position = new Vector3(_xBounds, transform.position.y, transform.position.z);
        }
        else if (transform.position.x < -_xBounds)
        {
            transform.position = new Vector3(-_xBounds, transform.position.y, transform.position.z);
        }

        if (transform.position.z > _zBounds)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, _zBounds);
        }
        else if (transform.position.z < -_zBounds)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -_zBounds);
        }
    }
}

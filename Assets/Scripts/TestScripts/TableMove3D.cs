using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableMove3D : MonoBehaviour
{
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private AudioSource _concreteScrape;

    private Vector3 _mousePos;

    private Rigidbody _rb;
    private Vector3 _targetPos;
    private float _followSpeed = 10f;
    private bool _moving = false;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        if (_concreteScrape == null ) 
        {
            //AudioSource source = gameObject.AddComponent<AudioSource>();
            //source.clip = Resources.Load<AudioClip>("Audio/Table_Move");
            //_concreteScrape = source;
        }
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
        // Generate a ray from the mouse position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Perform the raycast and check for collisions with objects in the specified layer
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, _layerMask))
        {
            // If the ray hits an object in the specified layer, you can access the hit object here
            //Debug.Log("Hit object: " + hit.collider.gameObject.name);

            // Do something with the hit object if needed
            // For example, you can get its position:
            Vector3 hitPoint = hit.point;
            // Or you can get the GameObject itself:
            GameObject hitObject = hit.collider.gameObject;
            // Now you can perform any actions you need with the hit object
            return hit.point;
        }

        Debug.LogError("Something wrong with raycast");
        return Vector3.zero;
    }

    public void OnMouseDownRoom()
    {
        _mousePos = GetMousePos() - transform.position;
        _moving = true;
        //Debug.Log(this);
    }

    public void OnMouseDragRoom()
    {
        _targetPos = GetMousePos() - _mousePos;

        if (_rb.velocity.magnitude > 0.1f)
        {
            if (!_concreteScrape.isPlaying)
                _concreteScrape.Play();
        }
        else
            _concreteScrape.Pause();
        
        //ClampPosition();
    }

    public void OnMouseUpRoom()
    {
        _concreteScrape.Pause();
        _moving = false;
        _rb.velocity = Vector3.zero;
    }
}

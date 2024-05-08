using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMover : MonoBehaviour
{
    public float Speed = 5;

    [SerializeField]
    private bool _moving = false;

    // Wwy the fuck is this even needed I have no idea I am going to go insane
    public Vector3 Offset = new Vector3(43, 0, 0);

    private Transform _target;

    public event EventHandler ReachedDestination;

    // Update is called once per frame
    void Update()
    {
        if (_moving && _target != null)
        {
            Vector3 differance = _target.position - Offset - transform.position;

            if (differance.magnitude < Speed * Time.deltaTime)
            {
                transform.position = _target.position - Offset;
                _moving = false;
                ReachedDestination?.Invoke(this, EventArgs.Empty);
                return;
            }

            transform.position += (Speed * Time.deltaTime * differance.normalized);
        }
    }

    public void SetTarget(Transform target)
    {
        if (target == null)
        {
            _moving = false;
            return;
        }

        _target = target;
    }

    public void Go()
    {
        _moving = true;
    }

    public void Stop()
    {
        _moving = false;
    }

    public void GoTo(Transform target)
    {
        SetTarget(target);
        if (_target != null) { Go(); }        
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// This script will be used to create a tunnel between connected doors
// Assumes a door can only be connected to ONE other door
public class TunnelManager : MonoBehaviour
{
    [SerializeField] private GameObject _tunnelPrefab;
    [SerializeField] private float _offsetLength = 6f;
    private List<Tuple<Door, Door>> _connections = new();
    private List<Route> _routes = new();
    public void DoorsHaveConnected(Door door1, Door door2)
    {
        _connections.Add(new Tuple<Door, Door>(door1, door2));
        _routes.Add(Instantiate(_tunnelPrefab).GetComponent<Route>());
    }

    public void DoorsHaveDisconnected(Door door1, Door door2)
    {
        bool removed = false;
        
        var objToRemove = _connections.FirstOrDefault(obj => obj.Item1 == door1 || obj.Item2 == door1); // shouldnt matter which door

        if (objToRemove != null)
        {
             removed = _connections.Remove(objToRemove);
        }

        if (removed)
        {
            if (_routes.Count < 1) { Debug.LogWarning("Something is wrong in tunnel manager script"); return; }

            Destroy(_routes[_routes.Count - 1].gameObject);
            _routes.RemoveAt(_routes.Count - 1);  // doesnt matter which one it removes
            
        }
    }

    public void Update()
    {
        for (int i = 0; i < _connections.Count; i++)
        {
            var conn = _connections[i];

            Vector3 offset = new Vector3(_offsetLength, 0);

            if (conn.Item1.TableDoor.transform.position.x > conn.Item2.TableDoor.transform.position.x)
                offset *= -1;

            _routes[i].controlPoints[0].position = conn.Item1.TableDoor.transform.position;
            _routes[i].controlPoints[1].position = conn.Item1.TableDoor.transform.position + offset;

            _routes[i].controlPoints[2].position = conn.Item2.TableDoor.transform.position - offset;
            _routes[i].controlPoints[3].position = conn.Item2.TableDoor.transform.position;
            
        }
    }
}

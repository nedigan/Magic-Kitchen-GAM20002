using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private TableMove3D _roomDragging;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            // Generate a ray from the mouse position
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            // Perform the raycast and check for collisions with objects in the specified layer
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                bool noRoomSelected = _roomDragging == null;
                // Check if is room
                if (noRoomSelected && hit.collider.gameObject.TryGetComponent<TableMove3D>(out _roomDragging))
                {
                    _roomDragging.OnMouseDownRoom();
                }
            }
            
            if (_roomDragging != null)
            {
                _roomDragging.OnMouseDragRoom();
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (_roomDragging != null)
                _roomDragging.OnMouseUpRoom();
            _roomDragging = null;
        }
    }
}

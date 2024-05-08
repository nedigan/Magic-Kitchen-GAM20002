using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MageHand : MonoBehaviour
{
    [SerializeField] private Sprite[] _sprites;
    [SerializeField] private Image _renderer;
    //[SerializeField] private Camera _cam;
    // Start is called before the first frame update
    void Start()
    {
        _renderer.sprite = _sprites[0];
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        //mousePos.z = 10;
        transform.position = mousePos;

        if (Input.GetMouseButtonDown(0))
        {
            _renderer.sprite = _sprites[1];
        }
        if (Input.GetMouseButtonUp(0))
        {
            _renderer.sprite = _sprites[0];
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageHand : MonoBehaviour
{
    [SerializeField] private Sprite[] _sprites;
    [SerializeField] private SpriteRenderer _renderer;
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
        mousePos.z = 10;
        transform.position = Camera.main.ScreenToWorldPoint(mousePos);

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

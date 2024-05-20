using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenClosedSign : MonoBehaviour
{
    public bool IsOpen = true;
    [SerializeField] private DayManager _dayManager;
    [SerializeField] private Sprite[] _sprites;
    private Image _image;

    private void Start()
    {
        _image = GetComponent<Image>();
    }

    public void ChangeState()
    {
        IsOpen = !IsOpen;
        _image.sprite = _sprites[IsOpen ? 0 : 1];
        _dayManager.SetStateOfRestaurant(IsOpen);
    }
}

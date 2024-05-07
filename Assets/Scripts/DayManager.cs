using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayManager : MonoBehaviour
{
    [SerializeField] private float _dayLengthSeconds = 300f; // 5mins?
    [SerializeField] private Slider _slider;

    private float _currentTime = 0f;
    public static float DayProgress = 0f;

    // Update is called once per frame
    void Update()
    {
        _currentTime += Time.deltaTime;
        DayProgress = Mathf.Clamp(_currentTime / _dayLengthSeconds, 0f, 1f);

        // Set slider
        _slider.value = DayProgress;
    }
}

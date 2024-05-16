using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

//[Serializable]
//public class WavePair // Thought this is a cool way 
//{
//    [Range(0, 1)]
//    public float StartWave;
//    [Range(0, 1)]
//    public float EndWave;
//    public float SpawnInterval;
//}
public class DayManager : MonoBehaviour
{
    [SerializeField] private float _dayLengthSeconds = 300f; // 5mins?
    [SerializeField] private Slider _slider;
    [SerializeField] private Day[] _days;
    private int _currentDayIndex = 0;

    public EventHandler OnDayEnd;

    //private int _waveIndex = 0;
    //private bool _waveInProgress = false;
    //private float _defaultSpawnInterval;

    //public WavePair[] Waves;

    private float _currentTime = 0f;
    public float DayProgress = 0f;

    private bool _dayEnded = false;

    public void Start()
    {
        _dayLengthSeconds = _days[_currentDayIndex].DayLengthSeconds;
    }
    public List<float> GetCurrentDayCustomerSpawns()
    {
        return _days[_currentDayIndex].GetCustomerSpawnTimes();
    }
    public void EndDay()
    {
        _dayEnded = true;

        OnDayEnd?.Invoke(this, EventArgs.Empty);
    }

    // Update is called once per frame
    void Update()
    {
        //// Update time
        _currentTime += Time.deltaTime;
        // Set time between 0 - 1
        DayProgress = Mathf.Clamp(_currentTime / _dayLengthSeconds, 0f, 1f);
        //// Set slider
        _slider.value = DayProgress;
        
        if (!_dayEnded && DayProgress >= 1)
            EndDay();
    }
}

using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using Unity.VisualScripting;

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
    [SerializeField] private TaskManager _taskManager;
    private int _currentDayIndex = 0;
    public bool IsOpen { get; private set; }    

    public EventHandler OnDayEnd;
    private static DayManager _instance;

    //private int _waveIndex = 0;
    //private bool _waveInProgress = false;
    //private float _defaultSpawnInterval;

    //public WavePair[] Waves;

    private float _currentTime = 0f;
    public float DayProgress = 0f;

    private bool _dayEnded = false;

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
    }
    public void Start()
    {
        _dayLengthSeconds = _days[_currentDayIndex].DayLengthSeconds;
        _currentTime = 0;
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

    public void SetStateOfRestaurant(bool open)
    {
        IsOpen = open;
    }

    public static DayManager GetInstance()
    {
        if (_instance != null)
            return _instance;
        Debug.LogError("There is no daymanager in the scene.");
        return null;
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
        
        if (!_dayEnded && DayProgress >= 1 && _taskManager.NumFoxesInRestaurant() == 0)
            EndDay();
    }
}

using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class WavePair // Thought this is a cool way 
{
    [Range(0, 1)]
    public float StartWave;
    [Range(0, 1)]
    public float EndWave;
    public float SpawnInterval;
}
public class DayManager : MonoBehaviour
{
    [SerializeField] private float _dayLengthSeconds = 300f; // 5mins?
    [SerializeField] private Slider _slider;
    [SerializeField] private CustomerSpawner _customerSpawner;

    public EventHandler OnDayEnd;

    private int _waveIndex = 0;
    private bool _waveInProgress = false;
    private float _defaultSpawnInterval;

    public WavePair[] Waves;

    private float _currentTime = 0f;
    public float DayProgress = 0f;

    private void Start()
    {
        _defaultSpawnInterval = _customerSpawner.SpawnTimeInterval;
    }

    private bool _dayEnded = false;
    public void EndDay()
    {
        _customerSpawner.StopSpawning();
        _dayEnded = true;

        OnDayEnd?.Invoke(this, EventArgs.Empty);
    }

    // Update is called once per frame
    void Update()
    {
        // Update time
        _currentTime += Time.deltaTime;
        // Set time between 0 - 1
        DayProgress = Mathf.Clamp(_currentTime / _dayLengthSeconds, 0f, 1f);
        // Set slider
        _slider.value = DayProgress;

        if (Waves.Length > _waveIndex)
        {
            if (!_waveInProgress && DayProgress >= Waves[_waveIndex].StartWave)
            {
                _waveInProgress = true;
                _customerSpawner.SetSpawnInterval(Waves[_waveIndex].SpawnInterval, true);
            }

            if (_waveInProgress && DayProgress >= Waves[_waveIndex].EndWave)
            {
                _waveInProgress = false;
                _waveIndex++;

                _customerSpawner.SetSpawnInterval(_defaultSpawnInterval, false);
            }
        }

        if (!_dayEnded && DayProgress >= 1)
            EndDay();
    }
}

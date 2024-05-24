using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    private static AudioManager _instance;
    private AudioSource _audioSource;
    [SerializeField] private AudioClip[] _SFXAudioClips;

    // Start is called before the first frame update
    void Awake()
    {
        if (_instance == null )
            _instance = this;
    }

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayClipIndex(int index)
    {
        if (index < _SFXAudioClips.Length && index >= 0)
        {
            _audioSource.PlayOneShot(_SFXAudioClips[index]);
        }
        else
        {
            Debug.LogWarning($"No audio to play at index {index}");
        }
    }

    public static AudioManager GetInstance()
    {
        if (_instance != null)
            return _instance;
        Debug.LogError("There is no AudioManager in the scene.");
        return null;
    }
}

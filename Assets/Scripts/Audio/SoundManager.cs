using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource _MusicSource, _SFXSource;
    private static SoundManager _instance;
    public static SoundManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<SoundManager>();
                if (_instance == null)
                {
                    Debug.LogError("Sound manager not found in the scene.");
                }
                else
                {
                    _instance.Initialise();
                }
            }
            return _instance;
        }
    }
    
    // Start is called before the first frame update
    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            Initialise();
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Initialise()
    {
        DontDestroyOnLoad(gameObject);

        // Make other audio children copy accross
        foreach (Transform child in transform)
        {
            DontDestroyOnLoad(child.gameObject);
        }
    }

    public void PlaySound(AudioClip clip, bool loop)
    {
        if (clip == null) return; //countdown clip is null after scene reload -- this is just to prevent errors
        if (loop)
        {
            _SFXSource.clip = clip;
            _SFXSource.loop = true;
            _SFXSource.Play();
            return;
        }
        _SFXSource.PlayOneShot(clip);
    }

    public void StopSFX(AudioClip SFX)
    {
        _SFXSource.Stop();
    }

    public void ChangeMasterVolume(float value)
    {
        AudioListener.volume = value;
    }

    public void ChangeMusicVolume(float value)
    {
        _MusicSource.volume = value;
    }

    public void ChangeSFXVolume(float value)
    {
        _SFXSource.volume = value;
    }
}

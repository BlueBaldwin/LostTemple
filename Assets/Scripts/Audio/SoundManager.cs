using System;
using System.Collections;
using Audio;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource _MusicSource, _SFXSource, _DialogSource;
    [SerializeField]  AudioClip musicClip;
    
    private AudioAnalyzer _audioAnalyzer;
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
            }
            return _instance;
        }
    }

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);

            // Make other audio children copy accross
            foreach (Transform child in transform)
            {
                DontDestroyOnLoad(child.gameObject);
            }
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _audioAnalyzer = GetComponent<AudioAnalyzer>();
    }

    public void PlaySound(AudioClip clip, bool loop)
    {
        if (clip == null) return;
        if (loop)
        {
            _SFXSource.clip = clip;
            _SFXSource.loop = true;
            _SFXSource.Play();
            return;
        }
        _SFXSource.PlayOneShot(clip);
    }
    
    public void PlayDialog(AudioClip clip)
    {
        if (clip == null) return;
        _DialogSource.clip = clip;
        _DialogSource.Play();
        _audioAnalyzer.audioSource = _DialogSource;
    }

    public float GetClipLength(AudioClip clip)
    {
        return clip.length;
    }

    public void StopSFX()
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

    public void PlayMusic()
    {
        _MusicSource.clip = musicClip;
        _MusicSource.Play();
    }

    // private IEnumerator PlayMusicCoroutine(AudioClip music)
    // {
    //     yield return new WaitForSeconds(GetClipLength(music) + 0.5f);
    //     _MusicSource.Play();
    // }
}

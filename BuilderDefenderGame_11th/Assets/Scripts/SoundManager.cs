using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Sound
{
        BuildingPlaced,
        BuildingDamanged,
        BuildingDestroyed,
        EnemyDie,
        EnemyHit,
        GameOver,
}

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    private Dictionary<Sound, AudioClip> soundAudioClipDictionary;

    private AudioSource audioSource;

    private float volume = .5f;
    
    private void Awake()
    {
        Instance = this;
        
        soundAudioClipDictionary = new Dictionary<Sound, AudioClip>();

        foreach (Sound sound in System.Enum.GetValues(typeof(Sound)))
        {
            soundAudioClipDictionary[sound] = Resources.Load<AudioClip>(sound.ToString());
        }
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(Sound sound)
    {
        audioSource.PlayOneShot(soundAudioClipDictionary[sound], volume);
    }

    public void IncreaseVolume()
    {
        volume += .1f;
        volume = Mathf.Clamp01(volume);
    }
    
    public void DecreaseVolume()
    {
        volume -= .1f;
        volume = Mathf.Clamp01(volume);
    }

    public float GetVolume()
    {
        return volume;
    }
    
    
}

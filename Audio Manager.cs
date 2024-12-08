using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource musicAudioSource;
    public AudioSource sfxAudioSource;

    void Start()
    {
        // save slider settings from options menu
        float masterVolume = PlayerPrefs.GetFloat("MasterVolume", 1f);
        float musicVolume = PlayerPrefs.GetFloat("MusicVolume", 1f);
        float sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 1f);

        // applies slider options
        AudioListener.volume = masterVolume; 
        if (musicAudioSource != null) musicAudioSource.volume = musicVolume;
        if (sfxAudioSource != null) sfxAudioSource.volume = sfxVolume;
    }
}

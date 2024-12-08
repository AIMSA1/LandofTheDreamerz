using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OptionsMenuController : MonoBehaviour
{
    public TMP_Dropdown resolutionDropdown;
    public Slider masterVolumeSlider;
    public Slider sfxVolumeSlider;
    public Slider musicVolumeSlider;
    public AudioSource musicAudioSource;
    public AudioSource sfxAudioSource;

    private Resolution[] resolutions; 

    void Start()
    {
        // get all possible resolutions
        resolutions = Screen.resolutions;
        // clear whatever was already in the dropdown
        resolutionDropdown.ClearOptions();

        int currentResolutionIndex = 0; 
        List<string> options = new List<string>(); 

        // loop through all the resolutions
        for (int i = 0; i < resolutions.Length; i++)
        {
            // create an option like "1920 x 1080"
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            // if this is the player's current resolution, save the index
            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        // add all the resolution options to the dropdown
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex; 
        resolutionDropdown.RefreshShownValue();

        // load saved volume settings or use defaults
        masterVolumeSlider.value = PlayerPrefs.GetFloat("MasterVolume", 1f);
        musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume", musicAudioSource != null ? musicAudioSource.volume : 1f);
        sfxVolumeSlider.value = PlayerPrefs.GetFloat("SFXVolume", sfxAudioSource != null ? sfxAudioSource.volume : 1f);

        // apply the loaded volume settings
        AudioListener.volume = masterVolumeSlider.value;
        if (musicAudioSource != null) musicAudioSource.volume = musicVolumeSlider.value;
        if (sfxAudioSource != null) sfxAudioSource.volume = sfxVolumeSlider.value;

        // link the UI controls to their respective methods
        resolutionDropdown.onValueChanged.AddListener(SetResolution);
        masterVolumeSlider.onValueChanged.AddListener(SetMasterVolume);
        musicVolumeSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxVolumeSlider.onValueChanged.AddListener(SetSFXVolume);
    }

    void SetResolution(int resolutionIndex)
    {
        // change the screen resolution to the selected one
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    void SetMasterVolume(float volume)
    {
        AudioListener.volume = volume;
        PlayerPrefs.SetFloat("MasterVolume", volume);
        PlayerPrefs.Save();
    }

    void SetMusicVolume(float volume)
    {
        if (musicAudioSource != null)
        {
            musicAudioSource.volume = volume;
        }
        PlayerPrefs.SetFloat("MusicVolume", volume);
        PlayerPrefs.Save();
    }

    void SetSFXVolume(float volume)
    {
        if (sfxAudioSource != null)
        {
            sfxAudioSource.volume = volume;
        }
        PlayerPrefs.SetFloat("SFXVolume", volume);
        PlayerPrefs.Save();
    }
}

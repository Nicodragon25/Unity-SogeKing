using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSliders : MonoBehaviour
{
    public static VolumeSliders volumen;
    public AudioMixer audioMixer;
    private void Awake()
    {
        FindObjectOfType<GameManager>().OnApplyButton += Apply;

        gameObject.GetComponent<Slider>().maxValue = 1;
        switch (gameObject.name)
        {
            case "Master Volume":
                if (PlayerPrefs.GetFloat("MasterVolume") != 0)
                {
                    gameObject.GetComponent<Slider>().value = PlayerPrefs.GetFloat("MasterVolume");
                    audioMixer.SetFloat("AudioMixerMasterVolume", Mathf.Log10(PlayerPrefs.GetFloat("MasterVolume")) * 20);
                }
                else if (PlayerPrefs.GetFloat("MasterVolume") == 0)
                {
                    gameObject.GetComponent<Slider>().value = 0.5f;
                    audioMixer.SetFloat("AudioMixerMasterVolume", Mathf.Log10(0.5f) * 20);
                }
                break;
            case "Music Volume":
                if (PlayerPrefs.GetFloat("MusicVolume") != 0)
                {
                    gameObject.GetComponent<Slider>().value = PlayerPrefs.GetFloat("MusicVolume");
                    audioMixer.SetFloat("AudioMixerMusicVolume", Mathf.Log10(PlayerPrefs.GetFloat("MusicVolume")) * 20);
                }
                else if (PlayerPrefs.GetFloat("MusicVolume") == 0)
                {
                    gameObject.GetComponent<Slider>().value = 0.5f;
                    audioMixer.SetFloat("AudioMixerMusicVolume", Mathf.Log10(0.5f) * 20);
                }
                break;
            case "SFX Volume":
                if (PlayerPrefs.GetFloat("SFXVolume") != 0)
                {
                    gameObject.GetComponent<Slider>().value = PlayerPrefs.GetFloat("SFXVolume");
                    audioMixer.SetFloat("AudioMixerSFXVolume", Mathf.Log10(PlayerPrefs.GetFloat("SFXVolume")) * 20);
                }
                else if (PlayerPrefs.GetFloat("SFXVolume") == 0)
                {
                    gameObject.GetComponent<Slider>().value = 0.5f;
                    audioMixer.SetFloat("AudioMixerSFXVolume", Mathf.Log10(0.5f) * 20);
                }
                break;
        }
    }

    public void Apply()
    {
        switch (gameObject.name)
        {
            case "Master Volume":
                PlayerPrefs.SetFloat("MasterVolume", gameObject.GetComponent<Slider>().value);
                audioMixer.SetFloat("AudioMixerMasterVolume", Mathf.Log10(PlayerPrefs.GetFloat("MasterVolume")) * 20);
                break;
            case "Music Volume":
                PlayerPrefs.SetFloat("MusicVolume", gameObject.GetComponent<Slider>().value);
                audioMixer.SetFloat("AudioMixerMusicVolume", Mathf.Log10(PlayerPrefs.GetFloat("MusicVolume")) * 20);
                break;
            case "SFX Volume":
                PlayerPrefs.SetFloat("SFXVolume", gameObject.GetComponent<Slider>().value);
                audioMixer.SetFloat("AudioMixerSFXVolume", Mathf.Log10(PlayerPrefs.GetFloat("SFXVolume")) * 20);
                break;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class AudioController : MonoBehaviour
{
    public static AudioController Audio;
    public Slider MasterSlider;
    public Slider MusicSlider;
    public Slider SFXSlider;

    public AudioMixer audioMixer;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        if (PlayerPrefs.GetFloat("MasterVolume") != 0)
        {
            MasterSlider.value = PlayerPrefs.GetFloat("MasterVolume");
            audioMixer.SetFloat("AudioMixerMasterVolume", Mathf.Log10(PlayerPrefs.GetFloat("MasterVolume")) * 20);
        }
        else if (PlayerPrefs.GetFloat("MasterVolume") == 0)
        {
            MasterSlider.value = 0.5f;
            audioMixer.SetFloat("AudioMixerMasterVolume", Mathf.Log10(0.5f) * 20);
        }

        if (PlayerPrefs.GetFloat("MusicVolume") != 0)
        {
            MusicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
            audioMixer.SetFloat("AudioMixerMusicVolume", Mathf.Log10(PlayerPrefs.GetFloat("MusicVolume")) * 20);
        }
        else if (PlayerPrefs.GetFloat("MusicVolume") == 0)
        {
            MusicSlider.value = 0.5f;
            audioMixer.SetFloat("AudioMixerMusicVolume", Mathf.Log10(0.5f) * 20);
        }

        if (PlayerPrefs.GetFloat("SFXVolume") != 0)
        {
            SFXSlider.value = PlayerPrefs.GetFloat("SFXVolume");
            audioMixer.SetFloat("AudioMixerSFXVolume", Mathf.Log10(PlayerPrefs.GetFloat("SFXVolume")) * 20);
        }
        else if (PlayerPrefs.GetFloat("SFXVolume") == 0)
        {
            SFXSlider.value = 0.5f;
            audioMixer.SetFloat("AudioMixerSFXVolume", Mathf.Log10(0.5f) * 20);
        }

    }


    private void Awake()
    {
        if (Audio == null)
        {
            Audio = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Audio.MasterSlider = MasterSlider;
            Audio.MusicSlider = MusicSlider;
            Audio.SFXSlider = SFXSlider;
            Destroy(gameObject);
        }
        FindObjectOfType<GameManager>().OnApplyButton += Apply;
    }

    void Apply()
    {
        PlayerPrefs.SetFloat("MasterVolume", MasterSlider.value);
        audioMixer.SetFloat("AudioMixerMasterVolume", Mathf.Log10(PlayerPrefs.GetFloat("MasterVolume")) * 20);

        PlayerPrefs.SetFloat("MusicVolume", MusicSlider.value);
        audioMixer.SetFloat("AudioMixerMusicVolume", Mathf.Log10(PlayerPrefs.GetFloat("MusicVolume")) * 20);

        PlayerPrefs.SetFloat("SFXVolume", SFXSlider.value);
        audioMixer.SetFloat("AudioMixerSFXVolume", Mathf.Log10(PlayerPrefs.GetFloat("SFXVolume")) * 20);

    }
}

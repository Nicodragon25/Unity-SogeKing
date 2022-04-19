using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MouseSensitivityController : MonoBehaviour
{
    public static MouseSensitivityController mouseSensi;
    public Slider MouseSensiSlider;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        if (PlayerPrefs.GetFloat("MouseSensitivity") > 0 && FindObjectOfType<MouseLook>())
        {
            MouseSensiSlider.value = PlayerPrefs.GetFloat("MouseSensitivity");
            FindObjectOfType<MouseLook>().mouseSensitivity = PlayerPrefs.GetFloat("MouseSensitivity");
        }
        if (PlayerPrefs.GetFloat("MouseSensitivity") <= 0 && FindObjectOfType<MouseLook>())
        {
            MouseSensiSlider.value = 150;
            FindObjectOfType<MouseLook>().mouseSensitivity = 150;
        }
    }

    private void Awake()
    {
        if(mouseSensi == null)
        {
            mouseSensi = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            mouseSensi.MouseSensiSlider = MouseSensiSlider;
            Destroy(gameObject);
        }

        FindObjectOfType<GameManager>().OnApplyButton += apply;
    }

    void apply()
    {
        PlayerPrefs.SetFloat("MouseSensitivity", MouseSensiSlider.value);
        if(FindObjectOfType<MouseLook>() != null) FindObjectOfType<MouseLook>().mouseSensitivity = PlayerPrefs.GetFloat("MouseSensitivity");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseSensiSlider : MonoBehaviour
{

    private void Awake()
    {
        FindObjectOfType<GameManager>().OnApplyButton += apply;
        if (PlayerPrefs.GetFloat("MouseSensitivity") > 0)
        {
            gameObject.GetComponent<Slider>().value = PlayerPrefs.GetFloat("MouseSensitivity");
            FindObjectOfType<MouseLook>().mouseSensitivity = PlayerPrefs.GetFloat("MouseSensitivity");
        }
        if (PlayerPrefs.GetFloat("MouseSensitivity") <= 0)
        {
            gameObject.GetComponent<Slider>().value = 150;
            FindObjectOfType<MouseLook>().mouseSensitivity = 150;
        }
    }

    void apply()
    {
      PlayerPrefs.SetFloat("MouseSensitivity" , gameObject.GetComponent<Slider>().value);
    }
}

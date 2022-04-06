using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiController : MonoBehaviour
{
    public GameObject optionsPanel;
    public GameObject creditsPanel;
    public void PlayButton()
    {
        GameManager.Instance.NextLevel();
    }
    public void OptionsButton()
    {
        OptionsSwitch();
    }
    public void CreditsButton()
    {
        creditsPanel.SetActive(true);
    }
    public void QuitButton()
    {
        Application.Quit();
    }
    public void OptionsSwitch()
    {
        if (optionsPanel.activeInHierarchy == false)
        {
            optionsPanel.SetActive(true);
        }
        if(optionsPanel.activeInHierarchy == true)
        {
            optionsPanel.SetActive(false);
        }
    }
}

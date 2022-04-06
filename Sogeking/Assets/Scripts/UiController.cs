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
        if (creditsPanel.activeInHierarchy == true) creditsPanel.SetActive(false);
        else if (creditsPanel.activeInHierarchy == false) creditsPanel.SetActive(true);
    }
    public void QuitButton()
    {
        Application.Quit();
    }
    public void ApplyButton()
    {
        GameManager.Instance.ApplyChanges();
    }

    public void OptionsSwitch()
    {
        if (optionsPanel.activeInHierarchy == false) optionsPanel.SetActive(true);
        else if (optionsPanel.activeInHierarchy == true) optionsPanel.SetActive(false);
        if (Time.timeScale == 0)
        {
            GameManager.Instance.PauseToggle();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    public void ApplyButton()
    {
        GameManager.Instance.ApplyChanges();
    }

    public void ResumeButton()
    {
        GameManager.Instance.PauseToggle();
    }

    public void RestartButton()
    {
        GameManager.Instance.RestartLevel();
    }
    public void MainMenuButton()
    {
        GameManager.Instance.LoadLevel(0);
    }

    public void PlayButton()
    {
        GameManager.Instance.NextLevel();
    }
    public void OptionsButton()
    {
        GameManager.Instance.GetComponent<UiController>().OptionsButton();
    }
    public void CreditsButton()
    {
        GameManager.Instance.GetComponent<UiController>().CreditsButton();
    }
    public void QuitButton()
    {
        Application.Quit();
    }
}

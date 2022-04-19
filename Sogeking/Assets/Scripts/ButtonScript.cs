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
}

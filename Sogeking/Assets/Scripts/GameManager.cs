using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject door;
    public GameObject player;

    public GameObject pausePanel;
    int doorHp;
    public Slider doorHpBar;
    public Slider powerBar;
    public Slider mouseSensitivity;
    public TextMeshProUGUI enemiesText;
    bool isPaused;

    public int actualEnemies;
    void Start()
    {
        powerBar.gameObject.SetActive(false);
        doorHp = door.GetComponent<DoorController>().doorHP;
        doorHpBar.maxValue = doorHp;
        doorHpBar.value = doorHp;

        powerBar.minValue = player.GetComponent<PlayerController>().minArrowSpeed - 10;
        powerBar.maxValue = player.GetComponent<PlayerController>().maxArrowSpeed;
    }

    // Update is called once per frame
    void Update()
    {
       doorHpBar.value = door.GetComponent<DoorController>().doorHP;
        Camera.main.GetComponent<MouseLook>().mouseSensitivity = mouseSensitivity.value;
        if (Input.GetKeyDown(KeyCode.P))
        {
            if(!isPaused)
            {
                Time.timeScale = 0;
                pausePanel.SetActive(true);
                Cursor.lockState = CursorLockMode.Confined;
                isPaused = true;
            }
            if (isPaused)
            {
                Unpause();
            }
            
        }
        if (Time.timeScale == 1) isPaused = false;
    }
    public void Unpause()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void ChargePower(float power)
    {
        powerBar.value = power;
    }


    public void ChangeEnemiesLeft(int enemies)
    {
        actualEnemies = enemies;
        enemiesText.text = "Enemies Left: " + enemies.ToString();
    }
}

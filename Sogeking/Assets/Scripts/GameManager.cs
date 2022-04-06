using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] UiController uiController;
    public GameObject door;
    public GameObject player;

    
    int doorHp;
    public Slider doorHpBar;
    public Slider powerBar;
    public Slider mouseSensitivity;
    public TextMeshProUGUI enemiesText;
    public GameObject HitmarkerGO;

    public int actualEnemies;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);

        uiController = gameObject.GetComponent<UiController>();
    }
    void Start()
    {
        Time.timeScale = 1;
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
            if (Input.GetKeyDown(KeyCode.P))
            {
            PauseToggle();
            }
    }
    public void PauseToggle()
    {
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
            uiController.optionsPanel.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
        }
        else if (Time.timeScale == 1)
        {
            uiController.optionsPanel.SetActive(true);
            Cursor.lockState = CursorLockMode.Confined;
            Time.timeScale = 0;
        }
    }
    public void ChargePower(float power)
    {
        powerBar.value = power;
    }

    void ApplyChanges()
    {
        Camera.main.GetComponent<MouseLook>().mouseSensitivity = mouseSensitivity.value;
    }

    public void ChangeEnemiesLeft(int enemies)
    {
        actualEnemies = enemies;
        enemiesText.text = "Enemies Left: " + enemies.ToString();
    }


    IEnumerator hitmarkerDuration()
    {
        yield return new WaitForSeconds(0.5f);
        HitmarkerGO.SetActive(false);

    }
    public void HitMarker()
    {
        HitmarkerGO.SetActive(true);
        HitmarkerGO.GetComponent<Animator>().Play("Hit");
        StartCoroutine(hitmarkerDuration());
    }
    
    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ChargeLevel(int levelIndex)
    {
        SceneManager.LoadScene(levelIndex);
    }
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

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


    public int xd = 0;
    int doorHp;
    public Slider doorHpBar;
    public Slider powerBar;
    public Slider mouseSensitivity;
    [SerializeField] float mouseSensitivityFloat;
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
        else
        {
            Instance.doorHpBar = doorHpBar;
            Instance.powerBar = powerBar;
            Instance.mouseSensitivity = mouseSensitivity;
            //Instance.player = player;
            Instance.door = door;
            Instance.enemiesText = enemiesText;
            Instance.HitmarkerGO = HitmarkerGO;
            Destroy(gameObject);
        }

        uiController = gameObject.GetComponent<UiController>();
        if (FindObjectOfType<MouseLook>() != null) FindObjectOfType<MouseLook>().mouseSensitivity = Instance.mouseSensitivityFloat;

        Instance.powerBar.minValue = player.GetComponent<PlayerController>().minArrowSpeed - 10;
        Instance.powerBar.maxValue = player.GetComponent<PlayerController>().maxArrowSpeed;

        Instance.doorHpBar.maxValue = doorHp;
        Instance.doorHpBar.value = doorHp;
    }
    void Start()
    {
        Time.timeScale = 1;
        powerBar.gameObject.SetActive(false);

        if (door.GetComponent<DoorController>())
        {
        doorHp = door.GetComponent<DoorController>().doorHP;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (doorHp != 0)
        {
            doorHpBar.value = door.GetComponent<DoorController>().doorHP;
        }
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

    public void ApplyChanges()
    {
        if (FindObjectOfType<MouseLook>() != null)
        {
            mouseSensitivity = GameObject.Find("Sensitivity").GetComponent<Slider>();
            FindObjectOfType<MouseLook>().mouseSensitivity = mouseSensitivity.value;
        }
        else if (FindObjectOfType<MouseLook>() == null)
        {
            mouseSensitivityFloat = mouseSensitivity.value;
        }
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

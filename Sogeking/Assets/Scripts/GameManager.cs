using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;
using System;
using TMPro;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] UiController uiController;
    public ScoreController scoreController;
    public GameObject door;
    public GameObject player;
    public AudioMixer audioMixer;

    int doorHp;
    public Slider doorHpBar;
    public Slider powerBar;
    //public Slider mouseSensitivitySlider;
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
            //Instance.mouseSensitivitySlider = mouseSensitivitySlider;
            //Instance.player = player;
            Instance.door = door;
            Instance.HitmarkerGO = HitmarkerGO;
            Destroy(gameObject);
        }

        scoreController = gameObject.GetComponent<ScoreController>();
        uiController = gameObject.GetComponent<UiController>();
        Instance.uiController.gameOverPanel = uiController.gameOverPanel;
        Instance.uiController.optionsPanel = uiController.optionsPanel;
        FindObjectOfType<DoorController>().OnDoorBreak += GameOver;

        Instance.powerBar.minValue = player.GetComponent<PlayerController>().minArrowSpeed - 10;
        Instance.powerBar.maxValue = player.GetComponent<PlayerController>().maxArrowSpeed;

        //if (door.GetComponent<DoorController>()) doorHp = door.GetComponent<DoorController>().doorHP;
        Instance.doorHpBar.maxValue = door.GetComponent<DoorController>().doorHP;
        Instance.doorHpBar.value = door.GetComponent<DoorController>().doorHP;
    }
    void Start()
    {
        Time.timeScale = 1;
        powerBar.gameObject.SetActive(false);
        if (PlayerPrefs.GetFloat("MasterVolume") != 0)
        {
            audioMixer.SetFloat("AudioMixerMasterVolume", Mathf.Log10(PlayerPrefs.GetFloat("MasterVolume")) * 20);
        }
        else if (PlayerPrefs.GetFloat("MasterVolume") == 0)
        {
            audioMixer.SetFloat("AudioMixerMasterVolume", Mathf.Log10(0.5f) * 20);
        }
        if (PlayerPrefs.GetFloat("MusicVolume") != 0)
        {
            audioMixer.SetFloat("AudioMixerMusicVolume", Mathf.Log10(PlayerPrefs.GetFloat("MusicVolume")) * 20);
        }
        else if (PlayerPrefs.GetFloat("MusicVolume") == 0)
        {
            audioMixer.SetFloat("AudioMixerMusicVolume", Mathf.Log10(0.5f) * 20);
        }
        if (PlayerPrefs.GetFloat("SFXVolume") != 0)
        {
            audioMixer.SetFloat("AudioMixerSFXVolume", Mathf.Log10(PlayerPrefs.GetFloat("SFXVolume")) * 20);
        }
        else if (PlayerPrefs.GetFloat("SFXVolume") == 0)
        {
            audioMixer.SetFloat("AudioMixerSFXVolume", Mathf.Log10(0.5f) * 20);
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
        OnApplyButton();
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

    public void LoadLevel(int levelIndex)
    {
        SceneManager.LoadScene(levelIndex);
    }
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void GameOver()
    {
        uiController.gameOverPanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "High Score : " + PlayerPrefs.GetInt("HighScore").ToString();
        uiController.GameOverPanel();
        Cursor.lockState = CursorLockMode.Confined;

        Time.timeScale = 0;
    }
    public event Action OnApplyButton;
}

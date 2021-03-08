using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject titleScreen;
    public GameObject optionsScreen;
    public GameObject pauseScreen;
    public Transition transition;

    public AudioMixer audioMixer;

    public Slider[] masterSliders;
    public Slider[] musicSliders;
    public Slider[] sfxSliders;

    public int highScore = 0;

    bool isPaused = false;
    float timeScale;

    static GameController instance = null;
    public static GameController Instance { 
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this);
    }

    void Start()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        //PlayerPrefs.SetInt("HighScore", highScore);

        OnMasterVolume(PlayerPrefs.GetFloat("MasterVolume", 0));
        OnMusicVolume(PlayerPrefs.GetFloat("MusicVolume", 0));
        OnSFXVolume(PlayerPrefs.GetFloat("SFXVolume", 0));

        foreach (var slider in masterSliders)
        {
            audioMixer.GetFloat("MasterVolume", out float volume);
            slider.value = volume;
        }
        foreach (var slider in musicSliders)
        {
            audioMixer.GetFloat("MusicVolume", out float volume);
            slider.value = volume;
        }
        foreach (var slider in sfxSliders)
        {
            audioMixer.GetFloat("SFXVolume", out float volume);
            slider.value = volume;
        }
    }

    public void SetHighScore(int score)
    {
        highScore = score;
        PlayerPrefs.SetInt("HighScore", highScore);
    }

    public void OnLoadGameScene(string scene)
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        StartCoroutine(LoadGameScene(scene));
    }

    IEnumerator LoadGameScene(string scene)
    {
        transition.StartTransition(Color.black, 1);

        while (!transition.IsDone) { yield return null; }

        titleScreen.SetActive(false);
        SceneManager.LoadScene(scene);

        yield return null;
    }

    public void OnLoadMenuScene(string scene)
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        StartCoroutine(LoadMenuScene(scene));
    }

    IEnumerator LoadMenuScene(string scene)
    {
        transition.StartTransition(Color.black, 1);

        while (!transition.IsDone) { yield return null; }

        titleScreen.SetActive(true);
        SceneManager.LoadScene(scene);

        yield return null;
    }

    public void OnTitleScreen()
    {
        titleScreen.SetActive(true);
        optionsScreen.SetActive(false);
    }

    public void OnOptionsScreen()
    {
        titleScreen.SetActive(false);
        optionsScreen.SetActive(true);
    }

    public void OnPauseScreen()
    {
        if (isPaused)
        {
            isPaused = false;
            pauseScreen.SetActive(false);
            Time.timeScale = timeScale;

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            isPaused = true;
            pauseScreen.SetActive(true);
            timeScale = Time.timeScale;
            Time.timeScale = 0;

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void OnPause()
    {
        OnPauseScreen();
    }

    public void OnMasterVolume(float level)
    {
        audioMixer.SetFloat("MasterVolume", level);
        PlayerPrefs.SetFloat("MasterVolume", level);
    }

    public void OnMusicVolume(float level)
    {
        audioMixer.SetFloat("MusicVolume", level);
        PlayerPrefs.SetFloat("MusicVolume", level);
    }

    public void OnSFXVolume(float level)
    {
        audioMixer.SetFloat("SFXVolume", level);
        PlayerPrefs.SetFloat("SFXVolume", level);
    }
}

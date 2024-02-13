using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using EasyTransition;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject mainMenuPause;
    [SerializeField] GameObject optionsMenuPause;
    public static bool gameIsPaused = false;

    [Header("Gameplay Settings")]
    [SerializeField] private TMP_Text controllerCamSensTextValue = null;
    [SerializeField] private TMP_Text controllerShotSensTextValue = null;
    [SerializeField] private Slider controllerCamSensSlider = null;
    [SerializeField] private Slider controllerShotSensSlider = null;
    [SerializeField] private CameraController mainCamera = null;
    private int defaultCamSens = 5;
    private int defaultShotSens = 5;
    public int mainControllerCamSens = 5;
    public int mainControllerShotSens = 5;

    [Header("Volume Settings")]
    [SerializeField] private TMP_Text volumeTextValue = null;
    [SerializeField] private Slider volumeSlider = null;
    private float defaultVolume = 0.5f;
    private float precVolume;
    private float precCamSens;
    private float precShotSens;
    public TransitionSettings transitionRestart;
    public TransitionSettings transitionQuit;



    private void Awake()
    {

        precVolume = PlayerPrefs.GetFloat("masterVolume");
        volumeSlider.value = precVolume;
        SetVolume(precVolume);
        precCamSens = PlayerPrefs.GetFloat("masterCameraSens");
        controllerCamSensSlider.value = precCamSens;
        SetCameraSens(precCamSens);
        precShotSens = PlayerPrefs.GetFloat("masterShotSens");
        controllerShotSensSlider.value = precShotSens;
        SetShotSens(precShotSens);
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        gameIsPaused = false;
        mainMenuPause.SetActive(false);
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        mainMenuPause.SetActive(true);
        gameIsPaused = true;
    }


    public void Restart()
    {
        Time.timeScale = 1f;
        gameIsPaused = false;
        mainMenuPause.SetActive(false);
        TransitionManager.Instance().Transition("Game", transitionRestart, 0);


    }

    public void Quit()
    {
        Time.timeScale = 1f;
        gameIsPaused = false;
        mainMenuPause.SetActive(false);
        TransitionManager.Instance().Transition("MainMenu", transitionQuit, 0);
    }

     public void Options()
    {
        optionsMenuPause.SetActive(true);
        mainMenuPause.SetActive(false);
    }

    public void Back()
    {
        optionsMenuPause.SetActive(false);
        mainMenuPause.SetActive(true);
    }

    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
        volumeTextValue.text = volume.ToString("0.0");
    }

    public void VolumeApply()
    {
        PlayerPrefs.SetFloat("masterVolume", AudioListener.volume);
    }

    public void SetCameraSens(float value)
    {
        
        mainControllerCamSens = Mathf.RoundToInt(value);
        controllerCamSensTextValue.text = value.ToString("0");
    }

    public void SetShotSens(float value)
    {
        mainControllerShotSens = Mathf.RoundToInt(value);
        controllerShotSensTextValue.text = value.ToString("0");
    }

    public void GameplayApply()
    {
        PlayerPrefs.SetFloat("masterShotSens", controllerShotSensSlider.value);
        PlayerPrefs.SetFloat("masterCameraSens", controllerCamSensSlider.value);
        mainCamera.cameraSens = (int)controllerCamSensSlider.value;
        mainCamera.shotSens = (int)controllerShotSensSlider.value;
    }

    public void ResetButton(string menuType)
    {
        if (menuType == "Audio")
        {
            AudioListener.volume = defaultVolume;
            volumeSlider.value = defaultVolume;
            volumeTextValue.text = defaultVolume.ToString("0.0");
            VolumeApply();
        }
        if (menuType == "GamePlay")
        {
            controllerShotSensTextValue.text = defaultShotSens.ToString("0");
            controllerShotSensSlider.value = defaultShotSens;
            controllerCamSensSlider.value = defaultCamSens;
            controllerCamSensTextValue.text = defaultCamSens.ToString("0");
            GameplayApply();
        }
    }


    
}

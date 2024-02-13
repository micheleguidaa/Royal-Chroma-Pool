using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuController : MonoBehaviour
{
    [Header("Volume Settings")]
    [SerializeField] private TMP_Text volumeTextValue = null;
    [SerializeField] private Slider volumeSlider = null;
    [SerializeField] private float defaultVolume = 1.0f;

    [Header("Gameplay Settings")]
    [SerializeField] private TMP_Text controllerCamSensTextValue = null;
    [SerializeField] private TMP_Text controllerShotSensTextValue = null;
    [SerializeField] private Slider controllerCamSensSlider = null;
    [SerializeField] private Slider controllerShotSensSlider = null;
    [SerializeField] private int defaultCamSens = 5;
    [SerializeField] private int defaultShotSens = 5;
    public int mainControllerCamSens = 5;
    public int mainControllerShotSens = 5;
    private float precVolume;
    private float precCamSens;
    private float precShotSens;
    private static bool isFirstTime = true;

    [Header("Levels To Load")]
    public string newGameLevel;

    private void Awake()
    {
        if (isFirstTime)
        {
            isFirstTime = false;
            return;
        } 
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

    public void NewGameDialogYes()
    {
        SceneManager.LoadScene(newGameLevel);
    }

    public void ExitButton()
    {
        Application.Quit();
    }

    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
        volumeTextValue.text = volume.ToString("0.0");
    }

    public void VolumeApply()
    {
        PlayerPrefs.SetFloat("masterVolume",AudioListener.volume);
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
        CameraController.cameraSensDefault = (int)controllerCamSensSlider.value;
        CameraController.shotSensDefault = (int)controllerShotSensSlider.value;
    }

    public void ResetButton(string menuType)
    {
        if(menuType == "Audio")
        {
            AudioListener.volume = defaultVolume;
            volumeSlider.value = defaultVolume;
            volumeTextValue.text = defaultVolume.ToString("0.0");
            VolumeApply();
        }
        if(menuType == "GamePlay")
        {
            controllerShotSensTextValue.text = defaultShotSens.ToString("0");
            controllerShotSensSlider.value = defaultShotSens;
            controllerCamSensSlider.value = defaultCamSens;
            controllerCamSensTextValue.text = defaultCamSens.ToString("0");
            GameplayApply();
        }
    }



}

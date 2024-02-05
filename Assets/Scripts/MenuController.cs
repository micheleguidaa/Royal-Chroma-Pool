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
    [SerializeField] private int defaultCamSens = 4;
    [SerializeField] private int defaultShotSens = 4;
    public int mainControllerSens1 = 4;
    public int mainControllerSens2 = 4;

    [Header("Levels To Load")]
    public string newGameLevel;
    private string levelToLoad;
    [SerializeField] private GameObject noSavedGameDialog = null;

    [Header("Confirmation")]
    [SerializeField] private GameObject confirmationPrompt = null;

    public void NewGameDialogYes()
    {
        SceneManager.LoadScene(newGameLevel);
    }

    public void LoadGameDialogYes()
    {
        if (PlayerPrefs.HasKey("SavedLevel"))
        {
            levelToLoad = PlayerPrefs.GetString("SavedLevel");
            SceneManager.LoadScene(levelToLoad);
        }
        else
        {
            noSavedGameDialog.SetActive(true);
        }
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
        mainControllerSens1 = Mathf.RoundToInt(value);
        controllerCamSensTextValue.text = value.ToString("0");
    }

    public void SetShotaSens(float value)
    {
        mainControllerSens2 = Mathf.RoundToInt(value);
        controllerShotSensTextValue.text = value.ToString("0");
    }

    public void GameplayApply()
    {
        PlayerPrefs.SetFloat("masterCamSens", mainControllerSens1);
        PlayerPrefs.SetFloat("masterShotSens", mainControllerSens2);
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

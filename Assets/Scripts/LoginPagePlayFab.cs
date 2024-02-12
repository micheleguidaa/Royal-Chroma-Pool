using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using PlayFab;
using PlayFab.ClientModels;
using System;
using UnityEngine.SceneManagement;

public class LoginPagePlayFab : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI topText;
    [SerializeField] TextMeshProUGUI messageText;

    [Header("Login")]
    [SerializeField] TMP_InputField emailLoginInput;
    [SerializeField] TMP_InputField passwordLoginInput;
    [SerializeField] GameObject loginPage;

    [Header("Register")]
    [SerializeField] TMP_InputField emailRegisterInput;
    [SerializeField] TMP_InputField passwordRegisterInput;
    [SerializeField] GameObject registerPage;

    [Header("Recovery")]
    [SerializeField] TMP_InputField emailRecoveryInput;
    [SerializeField] GameObject recoverPage;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RegisterUser()
    {
        var request = new RegisterPlayFabUserRequest
        {
            Email = emailRegisterInput.text,
            Password = passwordRegisterInput.text,
            RequireBothUsernameAndEmail = false
        };

        PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSucces, OnError);
    }

    public void Login()
    {
        var request = new LoginWithEmailAddressRequest
        {
            Email = emailLoginInput.text,
            Password = passwordLoginInput.text
        };
        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSucces, OnError);
    }

    private void OnLoginSucces(LoginResult result)
    {
        messageText.text = "Loggin in";
        SceneManager.LoadScene("MainMenu");
    }

    public void RecoveryUser()
    {
        var request = new SendAccountRecoveryEmailRequest
        {
            Email = emailRecoveryInput.text,
            TitleId = "C2004",
        };

        PlayFabClientAPI.SendAccountRecoveryEmail(request, OnRecovererySucces, OnErrorRecovery);
    }

    private void OnErrorRecovery(PlayFabError result)
    {
        messageText.text = "No Email Found!";
    }

    private void OnRecovererySucces(SendAccountRecoveryEmailResult result)
    {
        OpenLoginPage();
        messageText.text = "Recovery Mail Sent";
    }

    private void OnError(PlayFabError error)
    {
        messageText.text = error.ErrorMessage;
        Debug.Log(error.GenerateErrorReport());
    }

    private void OnRegisterSucces(RegisterPlayFabUserResult result)
    {
        messageText.text = "New Account Is Created";
        OpenLoginPage();
    }



    public void OpenLoginPage()
    {
        loginPage.SetActive(true);
        registerPage.SetActive(false);
        recoverPage.SetActive(false);
        topText.text = "Login";
    }

    public void OpenRegisterPage()
    {
        loginPage.SetActive(false);
        registerPage.SetActive(true);
        recoverPage.SetActive(false);
        topText.text = "Register";
    }

    public void OpenRecoveryPage()
    {
        loginPage.SetActive(false);
        registerPage.SetActive(false);
        recoverPage.SetActive(true);
        topText.text = "Recovery";
    }

}

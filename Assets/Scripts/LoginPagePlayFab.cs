using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LoginPagePlayFab : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI topText;
    [SerializeField] TextMeshProUGUI messageText;

    [Header("Login")]
    [SerializeField] TMP_InputField emailLoginInput;
    [SerializeField] TMP_InputField passwordLoginInput;
    [SerializeField] GameObject loginPage;

    [Header("Register")]
    [SerializeField] TMP_InputField usernameRegisterInput;
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

    public void OpenLoginPage()
    {
        loginPage.SetActive(true);
        registerPage.SetActive(false);
        recoverPage.SetActive(false);
    }

    public void OpenRegisterPage()
    {
        loginPage.SetActive(false);
        registerPage.SetActive(true);
        recoverPage.SetActive(false);
    }

    public void OpenRecoveryPage()
    {
        loginPage.SetActive(false);
        registerPage.SetActive(false);
        recoverPage.SetActive(true);
    }

}

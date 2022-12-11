using Firebase;
using Firebase.Auth;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Auth : MonoBehaviour
{
    [SerializeField] private Toggle rememberMeToggle;
    FirebaseAuth auth;
    [SerializeField] private TextMeshProUGUI exeptionText;
    [SerializeField] private TMP_InputField emailField;
    [SerializeField] private TMP_InputField passwordField;
    void Start()
    {
        auth = FirebaseAuth.DefaultInstance;
        int toggle = PlayerPrefs.GetInt("RememberMe", 0);
        if (toggle == 0)
        {
            Debug.Log("ToggleOff");
            rememberMeToggle.isOn = false;
            auth.SignOut();
        }
        if (toggle == 1)
        {
            Debug.Log("ToggleOn");
            rememberMeToggle.isOn = true;
            ChangeScene();
        }


    }
    public void OnRegButtonClick()
    {
        StartCoroutine(Registation());
    }
    public void OnLoginButtonClick()
    {
        StartCoroutine(Login());
    }
    private void ChangeScene()
    {   
        if (auth.CurrentUser != null)
        {
            if (rememberMeToggle.isOn) { PlayerPrefs.SetInt("RememberMe", 1); }
            else { PlayerPrefs.SetInt("RememberMe", 0); }
            SceneManager.LoadScene(1);
        }
    }

    private IEnumerator Registation()
    {
        var authtask = auth.CreateUserWithEmailAndPasswordAsync(emailField.text, passwordField.text);
        yield return new WaitUntil(predicate: () => authtask.IsCompleted);

        if (authtask.Exception != null)
        {
            Debug.Log(authtask.Exception.GetBaseException());
            var exept = authtask.Exception.GetBaseException() as FirebaseException;
            exeptionText.text = authtask.Exception.GetBaseException().ToString().Replace("Firebase.FirebaseException:", "");
        }
    }
    private IEnumerator Login()
    {
        if (auth.CurrentUser == null)
        {
            var authtask = auth.SignInWithEmailAndPasswordAsync(emailField.text, passwordField.text);
            yield return new WaitUntil(predicate: () => authtask.IsCompleted);
            if (authtask.Exception != null)
            {
                Debug.Log(authtask.Exception.GetBaseException());
                exeptionText.text = authtask.Exception.GetBaseException().ToString().Replace("Firebase.FirebaseException:", "");
            }
        }
        else
        {
            ChangeScene();
        }
    }
}

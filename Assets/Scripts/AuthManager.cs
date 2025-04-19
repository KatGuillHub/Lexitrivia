using UnityEngine;
using Firebase.Auth;
using TMPro;
using Firebase.Extensions;

public class AuthManager : MonoBehaviour
{
    FirebaseAuth auth;

    public TMP_InputField emailInput;
    public TMP_InputField passwordInput;
    public TMP_Text statusText;

    void Start()
    {
        auth = FirebaseAuth.DefaultInstance;
    }

    public void RegisterUser()
    {
        string email = emailInput.text;
        string password = passwordInput.text;

        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled || task.IsFaulted)
            {
                statusText.text = " Registro fallido: " + task.Exception.Message;
            }
            else
            {
                FirebaseUser newUser = task.Result.User;
                statusText.text = " Usuario registrado: " + newUser.Email;
            }
        });
    }

    public void LoginUser()
    {
        string email = emailInput.text;
        string password = passwordInput.text;

        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled || task.IsFaulted)
            {
                statusText.text = " Login fallido: " + task.Exception.Message;
            }
            else
            {
                FirebaseUser user = task.Result.User;
                statusText.text = " Bienvenido: " + user.Email;
            }
        });
    }
}

using UnityEngine;
using Firebase;
using Firebase.Auth;
using Firebase.Extensions;
using TMPro;

public class FirebaseInitializer : MonoBehaviour
{
    public TMP_InputField emailInput;
    public TMP_InputField passwordInput;
    public TMP_Text statusText;

    private FirebaseAuth auth;

    void Start()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            if (task.Result == DependencyStatus.Available)
            {
                FirebaseApp app = FirebaseApp.DefaultInstance;

                // Asegúrate de poner tu URL real aquí
                app.Options.DatabaseUrl = new System.Uri("https://lextrivia-umng-default-rtdb.firebaseio.com/");
                Debug.Log("✅ Firebase inicializado con DatabaseURL.");

                auth = FirebaseAuth.DefaultInstance;
            }
            else
            {
                Debug.LogError("❌ Error al inicializar Firebase: " + task.Result);
            }
        });
    }

    public void RegisterUser()
    {
        if (auth == null)
        {
            statusText.text = "Firebase aún no está listo.";
            return;
        }

        string email = emailInput.text;
        string password = passwordInput.text;

        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled || task.IsFaulted)
            {
                statusText.text = "Registro fallido: " + task.Exception?.Message;
            }
            else
            {
                FirebaseUser newUser = task.Result.User;
                statusText.text = "Usuario registrado: " + newUser.Email;
            }
        });
    }

    public void LoginUser()
    {
        if (auth == null)
        {
            statusText.text = "Firebase aún no está listo.";
            return;
        }

        string email = emailInput.text;
        string password = passwordInput.text;

        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled || task.IsFaulted)
            {
                statusText.text = "Login fallido: " + task.Exception?.Message;
            }
            else
            {
                FirebaseUser user = task.Result.User;
                statusText.text = "Bienvenido: " + user.Email;
            }
        });
    }
}

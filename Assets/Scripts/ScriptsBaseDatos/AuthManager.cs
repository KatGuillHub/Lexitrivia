using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Extensions;

public class AuthManager : MonoBehaviour
{
    [Header("Firebase")]
    private FirebaseAuth auth;
    private DatabaseReference databaseReference;

    [Header("UI References")]
    public TMP_InputField emailInput;
    public TMP_InputField passwordInput;
    public TMP_InputField nicknameInput;
    public TMP_Text statusText;

    private void Start()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            if (task.Result == DependencyStatus.Available)
            {
                FirebaseApp app = FirebaseApp.DefaultInstance;

                auth = FirebaseAuth.DefaultInstance;
                databaseReference = FirebaseDatabase.GetInstance(app, "https://lextrivia-umng-default-rtdb.firebaseio.com/").RootReference;


                Debug.Log("✅ Firebase inicializado correctamente.");

                // 🔒 Forzar cierre de sesión al iniciar
                if (auth.CurrentUser != null)
                {
                    Debug.Log("⚠️ Cerrando sesión anterior...");
                    auth.SignOut();
                }

                // No hacer login automático si hay usuario, porque no se mantendrá la sesión
                if (auth.CurrentUser != null)
                {
                    Debug.Log("⚠️ Usuario detectado pero se reiniciará sesión (persistencia está desactivada).");
                    auth.SignOut();
                }
            }
            else
            {
                Debug.LogError("❌ Error al inicializar Firebase: " + task.Result);
                statusText.text = "Error al inicializar Firebase.";
            }
        });
    }

    public void RegisterUser()
    {
        string email = emailInput.text;
        string password = passwordInput.text;
        string nickname = nicknameInput.text;

        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(nickname))
        {
            statusText.text = "Por favor completa todos los campos.";
            return;
        }

        // Validación personalizada de contraseña
        if (password.Length < 7 ||
            !System.Text.RegularExpressions.Regex.IsMatch(password, "[A-Z]") ||
            !System.Text.RegularExpressions.Regex.IsMatch(password, @"[./]"))
        {
            statusText.text = "La contraseña debe tener mínimo 7 caracteres, una mayúscula y un carácter especial como '.' o '/'.";
            return;
        }

        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled || task.IsFaulted)
            {
                statusText.text = "Registro fallido:" + task.Exception?.Flatten().InnerExceptions[0].Message;
                return;
            }

            FirebaseUser newUser = task.Result.User;
            statusText.text = "Registro exitoso. Bienvenido," + newUser.Email;

            UserProfileData userData = new UserProfileData
            {
                email = newUser.Email,
                uid = newUser.UserId,
                nickname = nickname,
                score = 0
            };

            string json = JsonUtility.ToJson(userData);
            databaseReference.Child("users").Child(newUser.UserId).SetRawJsonValueAsync(json);

            UserSession.uid = userData.uid;
            UserSession.email = userData.email;
            UserSession.nickname = userData.nickname;
            UserSession.score = userData.score;

            SceneManager.LoadScene("04 Pantalla Principal");
        });
    }


    public void LoginUser()
    {
        string email = emailInput.text;
        string password = passwordInput.text;

        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
        {
            statusText.text = "Por favor completa todos los campos.";
            return;
        }

        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled || task.IsFaulted)
            {
                statusText.text = "Inicio de sesión fallido: " + task.Exception?.Message;
                return;
            }

            FirebaseUser user = task.Result.User;
            statusText.text = "Inicio exitoso. Bienvenido, " + user.Email;

            databaseReference.Child("users").Child(user.UserId).GetValueAsync().ContinueWithOnMainThread(dataTask =>
            {
                if (dataTask.IsCompleted && dataTask.Result.Exists)
                {
                    var json = dataTask.Result.GetRawJsonValue();
                    UserProfileData profile = JsonUtility.FromJson<UserProfileData>(json);

                    UserSession.uid = profile.uid;
                    UserSession.email = profile.email;
                    UserSession.nickname = profile.nickname;
                    UserSession.score = profile.score;

                    SceneManager.LoadScene("04 Pantalla Principal");
                }
                else
                {
                    statusText.text = "Error al obtener datos del perfil.";
                }
            });
        });
    }

    public void LogoutUser()
    {
        if (auth != null)
        {
            auth.SignOut();
            Debug.Log("🔓 Sesión cerrada.");
        }

        UserSession.uid = null;
        UserSession.email = null;
        UserSession.nickname = null;
        UserSession.score = 0;

        SceneManager.LoadScene("LoginScene");
    }
}

[System.Serializable]
public class UserProfileData
{
    public string email;
    public string uid;
    public string nickname;
    public int score;
}

public static class UserSession
{
    public static string uid;
    public static string email;
    public static string nickname;
    public static int score;
}





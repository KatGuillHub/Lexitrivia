using UnityEngine;
using Firebase.Auth;
using UnityEngine.SceneManagement;

public class SessionChecker : MonoBehaviour
{
    private void Start()
    {
        if (FirebaseAuth.DefaultInstance.CurrentUser != null)
        {
            SceneManager.LoadScene("04 Pantalla Principal"); 
        }
    }
}


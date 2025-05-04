using UnityEngine;
using TMPro;
using Firebase.Auth;
using UnityEngine.SceneManagement;

public class MainGameUIManager : MonoBehaviour
{
    public TMP_Text nicknameText;
    public TMP_Text scoreText;

    private void Start()
    {
        nicknameText.text = ""+ UserSession.nickname;
        scoreText.text = "Puntaje: "+ UserSession.score.ToString();
    }

    public void Logout()
    {
        FirebaseAuth.DefaultInstance.SignOut();

        // Limpiar los datos del usuario actual
        UserSession.uid = null;
        UserSession.email = null;
        UserSession.nickname = null;
        UserSession.score = 0;

        SceneManager.LoadScene("LoginScene"); // o la escena donde está tu formulario
    }
}



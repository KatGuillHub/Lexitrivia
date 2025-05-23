using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;
using Firebase;
using Firebase.Auth;
using Firebase.Database;

public class GestorPregunta : MonoBehaviour
{
    public GameObject chuloVerde;
    public GameObject cruzRoja;
    public GameObject panelRetroalimentacion;
    public TextMeshProUGUI textoRetroalimentacion;
    public TextMeshProUGUI textoPuntuacion;

    public string proximaEscena = "EscenaSiguiente";

    private DatabaseReference dbRef;

    void Start()
    {
        chuloVerde.SetActive(false);
        cruzRoja.SetActive(false);
        panelRetroalimentacion.SetActive(false);

        FirebaseApp app = FirebaseApp.DefaultInstance;
        FirebaseDatabase database = FirebaseDatabase.GetInstance(app, "https://lextrivia-umng-default-rtdb.firebaseio.com/");
        dbRef = database.RootReference;

        ActualizarTextoPuntuacion();
    }


    public void SeleccionarRespuesta(bool esCorrecta)
    {
        if (esCorrecta)
        {
            UserSession.score += 100;
            chuloVerde.SetActive(true);
            cruzRoja.SetActive(false);
            panelRetroalimentacion.SetActive(true);
            GuardarPuntajeEnFirebase();
            ActualizarTextoPuntuacion();
            StartCoroutine(CambiarEscenaDespuesDeSegundos(proximaEscena, 5f));
        }
        else
        {
            UserSession.score -= 25;
            chuloVerde.SetActive(false);
            cruzRoja.SetActive(true);
            panelRetroalimentacion.SetActive(false);
            GuardarPuntajeEnFirebase();
            ActualizarTextoPuntuacion();
            StartCoroutine(ReintentarPreguntaDespuesDeSegundos(1f)); // más tiempo
        }

    }

    void GuardarPuntajeEnFirebase()
    {
        if (!string.IsNullOrEmpty(UserSession.uid))
        {
            dbRef.Child("users").Child(UserSession.uid).Child("score").SetValueAsync(UserSession.score);
        }
    }

    IEnumerator CambiarEscenaDespuesDeSegundos(string nombreEscena, float segundos)
    {
        yield return new WaitForSeconds(segundos);
        SceneManager.LoadScene(nombreEscena);
    }

    IEnumerator ReintentarPreguntaDespuesDeSegundos(float segundos)
    {
        yield return new WaitForSeconds(segundos);
        chuloVerde.SetActive(false);
        cruzRoja.SetActive(false);
        panelRetroalimentacion.SetActive(false);
    }

    void ActualizarTextoPuntuacion()
    {
        if (textoPuntuacion != null)
        {
            textoPuntuacion.text = "Puntaje: " + UserSession.score.ToString();
        }
    }
}


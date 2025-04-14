using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;


public class GestorPregunta : MonoBehaviour
{
    public GameObject chuloVerde;
    public GameObject cruzRoja;
    public GameObject panelRetroalimentacion;
    public TextMeshProUGUI textoRetroalimentacion;

    public string proximaEscena = "EscenaSiguiente";

    void Start()
    {
        chuloVerde.SetActive(false);
        cruzRoja.SetActive(false);
        panelRetroalimentacion.SetActive(false);
    }

    public void SeleccionarRespuesta(bool esCorrecta)
    {
        if (esCorrecta)
        {
            chuloVerde.SetActive(true);
            cruzRoja.SetActive(false);
            panelRetroalimentacion.SetActive(true);
            StartCoroutine(CambiarEscenaDespuesDeSegundos(proximaEscena, 2f));
        }
        else
        {
            chuloVerde.SetActive(false);
            cruzRoja.SetActive(true);
            panelRetroalimentacion.SetActive(false);
            StartCoroutine(ReintentarPreguntaDespuesDeSegundos(2f));
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
}

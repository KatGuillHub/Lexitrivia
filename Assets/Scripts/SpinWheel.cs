using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RuletaController : MonoBehaviour
{
    public Button botonGirar;
    public string[] nombresEscenas; // Asegúrate de poner los nombres en el orden de las secciones de la ruleta
    public float tiempoEspera = 10f;

    private bool girando = false;
    private float velocidadAngular = 0f;
    private float desaceleracion = 50f;
    private float anguloFinal;

    void Start()
    {
        botonGirar.onClick.AddListener(IniciarGiro);
    }

    void Update()
    {
        if (girando)
        {
            transform.Rotate(0, 0, -velocidadAngular * Time.deltaTime);
            velocidadAngular -= desaceleracion * Time.deltaTime;

            if (velocidadAngular <= 0)
            {
                girando = false;
                velocidadAngular = 0;

                // Determina el ángulo final
                anguloFinal = transform.eulerAngles.z;
                StartCoroutine(EsperarYEjecutar());
            }
        }
    }

    void IniciarGiro()
    {
        if (!girando)
        {
            velocidadAngular = Random.Range(500, 800); // Velocidad inicial aleatoria
            girando = true;
        }
    }

    IEnumerator EsperarYEjecutar()
    {
        yield return new WaitForSeconds(tiempoEspera);

        int indiceTema = ObtenerTemaDesdeAngulo(anguloFinal);
        SceneManager.LoadScene(nombresEscenas[indiceTema]);
    }

    int ObtenerTemaDesdeAngulo(float angulo)
    {
        // Asegúrate de que el ángulo esté entre 0 y 360
        angulo = (360 - (angulo % 360)) % 360;

        float seccion = 360f / nombresEscenas.Length;
        return Mathf.FloorToInt(angulo / seccion);
    }
}

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class SpinWheel : MonoBehaviour
{
    public Button botonGirar;
    public Transform ruleta;
    public float duracionGiro = 4f;
    public int vueltasMinimas = 5;
    public int vueltasMaximas = 7;

    private bool girando = false;
    private float tiempoGiro = 0f;
    private float anguloInicial = 0f;
    private float anguloObjetivo = 0f;
    private float anguloFinal = 0f;

    private int ultimoSector = -1; // Para evitar repetición

    void Start()
    {
        botonGirar.onClick.AddListener(IniciarGiro);
    }

    void Update()
    {
        if (girando)
        {
            tiempoGiro += Time.deltaTime;
            float t = Mathf.Clamp01(tiempoGiro / duracionGiro);
            float anguloActual = Mathf.Lerp(anguloInicial, anguloObjetivo, EaseOutCubic(t));
            ruleta.eulerAngles = new Vector3(0f, 0f, -anguloActual);

            if (t >= 1f)
            {
                girando = false;
                tiempoGiro = 0f;
                anguloFinal = anguloActual % 360;
                Debug.Log("Ángulo final: " + anguloFinal);
                StartCoroutine(CambiarEscenaDespuesDe(0.7f));
            }
        }
    }

    void IniciarGiro()
    {
        if (!girando)
        {
            int sectorAleatorio;
            do
            {
                sectorAleatorio = Random.Range(0, 6);
            } while (sectorAleatorio == ultimoSector);

            ultimoSector = sectorAleatorio;

            float anguloSector = sectorAleatorio * 60f + 30f; // Centro del sector
            int vueltas = Random.Range(vueltasMinimas, vueltasMaximas + 1);
            anguloInicial = ruleta.eulerAngles.z * -1f;
            anguloObjetivo = vueltas * 360f + anguloSector;

            girando = true;
            tiempoGiro = 0f;
        }
    }

    float EaseOutCubic(float t)
    {
        return 1f - Mathf.Pow(1f - t, 3f);
    }

    IEnumerator CambiarEscenaDespuesDe(float segundos)
    {
        yield return new WaitForSeconds(segundos);
        string nombreEscena = ObtenerEscenaPorAngulo(anguloFinal);
        SceneManager.LoadScene(nombreEscena);
    }

    string ObtenerEscenaPorAngulo(float angulo)
    {
        angulo = (angulo + 360f) % 360f;

        if (angulo >= 0f && angulo < 60f)
            return "11 Pantalla Tema 2";          // Azul (Público e impacto)
        else if (angulo >= 60f && angulo < 120f)
            return "15 Pantalla Tema 4";          // Rojo oscuro (Privado)
        else if (angulo >= 120f && angulo < 180f)
            return "17 Pantalla Tema 5";          // Rojo claro (Derecho público)
        else if (angulo >= 180f && angulo < 240f)
            return "19 Pantalla Tema 6";          // Fucsia (Laboral)
        else if (angulo >= 240f && angulo < 300f)
            return "Información";          // Verde (SER humano)
        else
            return "13 Pantalla Tema 3";          // Morado (Ética)
    }
}

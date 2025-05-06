using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement; // Importante para cambiar de escena

public class SpinWheel : MonoBehaviour
{
    public float spinDuration = 4f;
    public AnimationCurve easeCurve; // Asigna una curva suave desde el Inspector

    private bool isSpinning = false;

    private string[] temas = {
        "Derecho Penal",
        "Derecho Civil",
        "Derecho Laboral",
        "Derecho Constitucional",
        "Derecho Internacional"
    };

    private string[] escenas = {
        "Brújula de un abogado  impecable",         // Asegúrate que estas escenas existen y están en Build Settings
        "Guardianes de la ética",
        "La magia de ser humano",
        "Navegando el derech  público",
        "Navegando el derecho privado"
    };

    public void StartSpin()
    {
        Debug.Log("¡Botón presionado! Iniciando giro.");
        if (!isSpinning)
        {
            StartCoroutine(Spin());
        }
    }

    IEnumerator Spin()
    {
        isSpinning = true;

        float totalAngle = 360 * Random.Range(3, 6) + Random.Range(0, 360);
        float currentAngle = 0f;
        float elapsed = 0f;

        Quaternion startRotation = transform.rotation;

        while (elapsed < spinDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / spinDuration;
            float angle = Mathf.Lerp(0, totalAngle, easeCurve.Evaluate(t));
            transform.rotation = startRotation * Quaternion.Euler(0, 0, -angle);
            yield return null;
        }

        // Calcular el tema seleccionado según el ángulo
        int selectedSegment = GetSegmentFromAngle(transform.eulerAngles.z);
        Debug.Log("Tema seleccionado: " + temas[selectedSegment]);

        // Cargar escena correspondiente
        LoadThemeScene(selectedSegment);

        isSpinning = false;
    }

    int GetSegmentFromAngle(float angle)
    {
        float fixedAngle = (360 - angle + 36) % 360; // Centrar el ángulo
        return (int)(fixedAngle / 72f); // 360 / 5 temas = 72 grados por segmento
    }

    void LoadThemeScene(int temaIndex)
    {
        string sceneName = escenas[temaIndex];
        Debug.Log("Cargando escena: " + sceneName);
        SceneManager.LoadScene(sceneName);
    }
}

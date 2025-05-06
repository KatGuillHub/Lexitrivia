using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SpinWheel : MonoBehaviour
{
    public float spinDuration = 4f;
    public AnimationCurve easeCurve; // Asigna desde el Inspector

    private bool isSpinning = false;
    private int lastSegment = -1;

    private string[] temas = {
        "Derecho Penal",
        "Derecho Civil",
        "Derecho Laboral",
        "Derecho Constitucional",
        "Derecho Internacional"
    };

    private string[] escenas = {
        "Brújula de un abogado impecable",        
        "Guardianes de la ética",
        "La magia de ser humano",
        "Navegando el derecho público",
        "Navegando el derecho privado"
    };

    public void StartSpin()
    {
        if (!isSpinning)
        {
            StartCoroutine(Spin());
        }
    }

    IEnumerator Spin()
    {
        isSpinning = true;

        // Elegir un nuevo segmento diferente al anterior
        int newSegment;
        do
        {
            newSegment = Random.Range(0, temas.Length);
        } while (newSegment == lastSegment);

        lastSegment = newSegment;

        // Calcular ángulo total para detenerse exactamente en ese segmento
        float segmentAngle = 360f / temas.Length;
        float targetAngle = segmentAngle * newSegment;

        // Añadir varias vueltas completas para el efecto visual
        float extraRotations = Random.Range(3, 6) * 360f;
        float totalAngle = extraRotations + targetAngle;

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

        Debug.Log("Tema seleccionado: " + temas[newSegment]);

        LoadThemeScene(newSegment);

        isSpinning = false;
    }

    void LoadThemeScene(int temaIndex)
    {
        string sceneName = escenas[temaIndex];
        Debug.Log("Cargando escena: " + sceneName);
        SceneManager.LoadScene(sceneName);
    }
}

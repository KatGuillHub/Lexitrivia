using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SpinWheel : MonoBehaviour
{
    public float spinDuration = 4f;
    public AnimationCurve easeCurve; // Para una desaceleración suave
    public GameObject questionPanel;
    public Text questionText;

    private bool isSpinning = false;

    private string[] temas = { "Derecho Penal", "Derecho Civil", "Derecho Laboral", "Derecho Constitucional", "Derecho Internacional" };
    private string[,] preguntas = new string[5, 3]
    {
        { "¿Qué es el dolo?", "¿Qué es un delito?", "¿Qué significa atenuante?" },
        { "¿Qué es un contrato?", "¿Qué es la posesión?", "¿Qué es un bien inmueble?" },
        { "¿Qué es una relación laboral?", "¿Qué es el salario?", "¿Qué es un despido injustificado?" },
        { "¿Qué es la Constitución?", "¿Qué es un derecho fundamental?", "¿Qué es el amparo?" },
        { "¿Qué es un tratado internacional?", "¿Qué es el derecho consuetudinario?", "¿Qué es la ONU?" }
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

        int selectedSegment = GetSegmentFromAngle(transform.eulerAngles.z);
        Debug.Log("Tema seleccionado: " + temas[selectedSegment]);

        ShowQuestion(selectedSegment);
        isSpinning = false;
    }

    int GetSegmentFromAngle(float angle)
    {
        float fixedAngle = (360 - angle + 36) % 360; // 36 = la mitad de 72, para centrar la selección
        return (int)(fixedAngle / 72f);
    }

    void ShowQuestion(int temaIndex)
    {
        questionPanel.SetActive(true);
        string question = preguntas[temaIndex, Random.Range(0, 3)];
        questionText.text = "Tema: " + temas[temaIndex] + "\n\n" + question;
    }
}

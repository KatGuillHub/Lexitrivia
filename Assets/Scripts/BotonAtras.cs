using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VolverAtras : MonoBehaviour
{
    [Tooltip("Nombre de la escena a la que se debe volver. Déjalo vacío para cargar la anterior en el orden de construcción.")]
    public string escenaAnterior = "";

    public void Volver()
    {
        if (!string.IsNullOrEmpty(escenaAnterior))
        {
            SceneManager.LoadScene(escenaAnterior);
        }
        else
        {
            int escenaActual = SceneManager.GetActiveScene().buildIndex;
            if (escenaActual > 0)
            {
                SceneManager.LoadScene(escenaActual - 1);
            }
            else
            {
                Debug.LogWarning("No hay una escena anterior en el índice.");
            }
        }
    }
}


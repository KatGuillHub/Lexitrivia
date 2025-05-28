using UnityEngine;
using UnityEngine.UI;

public class ControladorSonidoGlobal : MonoBehaviour
{
    public Toggle efectosToggle;

    void Start()
    {
        bool efectosActivos = PlayerPrefs.GetInt("efectosActivos", 1) == 1;
        efectosToggle.isOn = efectosActivos;
        AplicarEstado(efectosActivos);

        efectosToggle.onValueChanged.AddListener(delegate { OnToggleChange(); });
    }

    void OnToggleChange()
    {
        bool estado = efectosToggle.isOn;
        AplicarEstado(estado);
        PlayerPrefs.SetInt("efectosActivos", estado ? 1 : 0);
        PlayerPrefs.Save();
    }

    void OnEnable()
{
    bool efectosActivos = PlayerPrefs.GetInt("efectosActivos", 1) == 1;
    AplicarEstado(efectosActivos);
}


    void AplicarEstado(bool estado)
    {
        GameObject[] objetosConTag = GameObject.FindGameObjectsWithTag("EfectoSonido");

        foreach (GameObject obj in objetosConTag)
        {
            AudioSource audio = obj.GetComponent<AudioSource>();
            if (audio != null)
            {
                audio.mute = !estado;
            }
        }
    }
}

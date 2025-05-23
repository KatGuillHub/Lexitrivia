using UnityEngine;
using UnityEngine.UI;

public class ControlOpciones : MonoBehaviour
{
    public GameObject panelOpciones;
    public Slider sliderVolumen;

    void Start()
    {
        panelOpciones.SetActive(false);

        if (MusicaFondo.instancia != null)
        {
            sliderVolumen.value = MusicaFondo.instancia.ObtenerVolumen();
            sliderVolumen.onValueChanged.AddListener((valor) =>
            {
                MusicaFondo.instancia.CambiarVolumen(valor);
            });
        }
    }

    public void AbrirOpciones()
    {
        panelOpciones.SetActive(true);
    }

    public void CerrarOpciones()
    {
        panelOpciones.SetActive(false);
    }
}

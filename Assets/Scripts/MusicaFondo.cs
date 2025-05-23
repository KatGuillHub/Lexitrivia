using UnityEngine;

public class MusicaFondo : MonoBehaviour
{
    public static MusicaFondo instancia;

    private AudioSource audioSource;

    private void Awake()
    {
        if (instancia != null && instancia != this)
        {
            Destroy(gameObject); // evita duplicados
            return;
        }

        instancia = this;
        DontDestroyOnLoad(gameObject);

        audioSource = GetComponent<AudioSource>();
        if (!audioSource.isPlaying)
        {
            audioSource.loop = true;
            audioSource.Play();
        }
    }

    public void PausarMusica() => audioSource.Pause();
    public void ReanudarMusica() => audioSource.UnPause();
    public void CambiarVolumen(float nuevoVolumen) => audioSource.volume = nuevoVolumen;
    public float ObtenerVolumen() => audioSource.volume;
}


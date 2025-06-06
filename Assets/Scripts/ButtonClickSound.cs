using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public AudioSource source;
    public AudioClip clickSound;

    void Awake()
    {
        // Singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Opcional si lo usas entre escenas
        }
        else
        {
            //Destroy(gameObject);
        }
    }

    public void PlayClickSound()
    {
        Debug.Log("sonido");
        source.PlayOneShot(clickSound);
    }
}


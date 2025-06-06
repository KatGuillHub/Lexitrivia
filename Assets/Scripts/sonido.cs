using UnityEngine;

public class sonido : MonoBehaviour
{
    public AudioClip clickSound;
    private AudioSource source;

    private bool isClicked = false;

    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    void OnMouseDown()
    {
        if (!isClicked)
        {
            isClicked = true;

            if (clickSound != null && source != null)
            {
                source.PlayOneShot(clickSound);
            }

            // Desaparece después de que termine el sonido
            //Destroy(gameObject, clickSound.length);
        }
    }
}

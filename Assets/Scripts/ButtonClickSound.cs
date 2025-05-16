
using UnityEngine;

public class ButtonClickSound : MonoBehaviour
{
    public AudioSource clickSource;

    public void PlayClick()
    {
        if (clickSource != null)
        {
            clickSource.Play();
        }
        else
        {
            Debug.LogWarning("AudioSource no asignado.");
        }
    }
}


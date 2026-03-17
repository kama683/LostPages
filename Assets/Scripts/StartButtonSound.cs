using UnityEngine;
using UnityEngine.EventSystems;

public class StartButtonSound : MonoBehaviour, IPointerClickHandler
{
    public AudioSource audioSource;
    public AudioClip clickClip;

    [Range(0f, 1f)] public float clickVolume = 0.6f;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (audioSource != null && clickClip != null)
        {
            audioSource.PlayOneShot(clickClip, clickVolume);
        }
    }
}
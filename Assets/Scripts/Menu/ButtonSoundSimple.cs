using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(AudioSource))]
public class ButtonSoundSimple : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    public AudioClip hoverClip;
    public AudioClip clickClip;

    private AudioSource audioSource;

    // Nombre de la clave en PlayerPrefs
    private const string volumeKey = "SFXVolume";

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        PlaySound(hoverClip);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        PlaySound(clickClip);
    }

    void PlaySound(AudioClip clip)
    {
        if (clip == null) return;

        float volume = PlayerPrefs.GetFloat(volumeKey, 0.5f); // 1f por defecto
        audioSource.PlayOneShot(clip, volume);
    }
}

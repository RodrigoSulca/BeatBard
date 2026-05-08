using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    public enum VolumeType { Master, Music, SFX, Notes };
    public VolumeType volumeType;
    private Slider slider;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeVolume()
    {
        switch (volumeType)
        {
            case VolumeType.Master:
                AudioManager.instance.masterVolume = slider.value;
                break;

            case VolumeType.Music:
                AudioManager.instance.musicVolume = slider.value;
                break;

            case VolumeType.SFX:
                AudioManager.instance.sfxVolume = slider.value;
                break;
            case VolumeType.Notes:
                AudioManager.instance.sfxNotes = slider.value;
                break;
        }
    }
}

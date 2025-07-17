using FMODUnity;
using UnityEngine;

public class FMODEvents : MonoBehaviour
{
    [field: Header("Song")]
    [field: SerializeField] public EventReference song { get; private set; }
    [field: SerializeField] public EventReference menuSong { get; private set; }

    [field: Header("SFX")]
    [field: SerializeField] public EventReference changeInstrument { get; private set; }
    [field: SerializeField] public EventReference playNote { get; private set; }
    [field: SerializeField] public EventReference noteFailed { get; private set; }
    [field: SerializeField] public EventReference failInstrument { get; private set; }
    [field: SerializeField] public EventReference impactPlayer { get; private set; }
    [field: SerializeField] public EventReference gameOver { get; private set; }
    [field: SerializeField] public EventReference victory { get; private set; }
    
    [field: Header("PowerUps SFX")]
    [field: SerializeField] public EventReference Invulnerable { get; private set; }
    [field: SerializeField] public EventReference healthPlayer { get; private set; }
    public static FMODEvents instance { get; private set; }

    private void Awake()
    {
        
        if (instance != null)
        {
            Debug.LogError("Hay mas de 1 FMODEvents en escena");
        }
        instance = this;
    }
}

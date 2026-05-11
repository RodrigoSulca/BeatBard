using UnityEngine;
using System.Collections;
using FMOD.Studio;
using System.Collections.Generic;
using TMPro;

public class CharterDemo : MonoBehaviour
{
    public enum Instrument { I1, I2, I3 }

    [Header("Instrument")]
    public Instrument instrument;

    [Header("Chart")]
    public TextAsset chart;

    [Header("Gameplay")]
    public Transform[] lines;
    public GameObject[] notePrefabs;
    public List<Note> notesList = new();

    [HideInInspector] public int noteId;

    [Header("References")]
    public HitNotes hitNotes;
    public InstrumentVController[] instrumentVControllers;
    public MultiplierController multiplierController;

    [HideInInspector] public EventInstance musicEventInstance;


    public float tiempoActual;

    // Detectar seek hacia atrás
    private float ultimoTiempo;


    [Header("Spawn")]
    public float spawnAheadTime = 2f;

    // Índice de próxima nota a generar
    private int nextNoteIndex = 0;

    void Start()
    {
        if (chart)
        {
            CargarCancion();
        }
        AudioManager.instance.InitializeSong(FMODEvents.instance.song);
        musicEventInstance = AudioManager.instance.GetMusicEventInstance();
        musicEventInstance.setPitch(1);
        musicEventInstance.setParameterByName("FocusInstrument",(int)instrument);
        musicEventInstance.start();
    }

    void Update()
    {

        PLAYBACK_STATE state;

        musicEventInstance.getPlaybackState(out state);

        if (notesList == null)
        {
            return;
        }

        int timelinePosition;

        musicEventInstance.getTimelinePosition(out timelinePosition);

        tiempoActual = timelinePosition / 1000f;

        float diferenciaTiempo = Mathf.Abs(tiempoActual - ultimoTiempo);

        // Si hubo un salto grande en el tiempo
        if (diferenciaTiempo > 0.5f)
        {
            OnSongSeek();
        }

        ultimoTiempo = tiempoActual;


        while (
            nextNoteIndex < notesList.Count &&
            notesList[nextNoteIndex].spawnTime <= tiempoActual + spawnAheadTime
        )
        {
            GenerarNota(notesList[nextNoteIndex]);

            nextNoteIndex++;
        }
    }


    void OnSongSeek()
    {
        Debug.Log("Seek detectado");

        // Eliminar notas actuales
        DeleteNotes();

        // Recalcular índice correcto
        nextNoteIndex = 0;

        while (
            nextNoteIndex < notesList.Count &&
            notesList[nextNoteIndex].spawnTime < tiempoActual
        )
        {
            nextNoteIndex++;
        }
    }

    void CargarCancion()
    {
        NotesList fullList =
            JsonUtility.FromJson<NotesList>(chart.text);

        if (fullList != null && fullList.notes != null)
        {
            notesList = fullList.notes;

            // Reiniciar índice
            nextNoteIndex = 0;

            // Buscar primera nota futura
            while (
                nextNoteIndex < notesList.Count &&
                notesList[nextNoteIndex].spawnTime < tiempoActual
            )
            {
                nextNoteIndex++;
            }

            Debug.Log("Notas cargadas: " + notesList.Count);
        }
    }

    void GenerarNota(Note nota)
    {
        if (nota.line < 1 || nota.line > lines.Length)
        {
            Debug.LogWarning("Línea inválida: " + nota.line);

            return;
        }

        Transform posicionline = lines[nota.line - 1];

        GameObject newNote =
            Instantiate(
                notePrefabs[(int)nota.type],
                posicionline.position,
                Quaternion.identity
            );
        newNote.GetComponentInChildren<TMP_Text>().text = $"{nextNoteIndex}"; 

        newNote.GetComponent<NoteController>().materialId = (int)instrument;
        if(nota.type == Note.NoteType.Hold)
        {
            newNote.GetComponent<HoldNote>().holdTime = nota.holdTime;
        }
    }

    public void ChangeMastil()
    {
        foreach (Renderer renderer in hitNotes.mastilRenderers)
        {
            renderer.material =
                hitNotes.mastilMaterial[(int)instrument];
        }

        foreach (GameObject ground in hitNotes.grounds)
        {
            ground.SetActive(false);
        }

        hitNotes.grounds[(int)instrument].SetActive(true);
    }


    public void DeleteNotes()
    {
        GameObject[] notes =
            GameObject.FindGameObjectsWithTag("Note");

        foreach (GameObject note in notes)
        {
            note.GetComponent<NoteController>().DestroyNote();
        }
    }
}

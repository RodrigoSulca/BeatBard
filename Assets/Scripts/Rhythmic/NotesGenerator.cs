using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections.Generic;
using FMOD.Studio;
using UnityEngine.SceneManagement;

public class NotesGenerator : MonoBehaviour
{
    public enum Instrument { I1, I2, I3 }
    public Instrument instrument;
    public bool canChange;
    public float changeCooldown;
    public Image finishObj;
    public TextAsset[] charts;
    public Transform[] lines;
    public GameObject[] notePrefabs;
    public NotesList notesList;
    /*[HideInInspector]*/ public int noteId;
    private HashSet<int> notasGeneradas = new();
    public float tiempoActual;
    public HitNotes hitNotes;
    public InstrumentVController[] instrumentVControllers;
    public MultiplierController multiplierController;
    private EventInstance musicEventInstance;

    void Start()
    {
        CargarCancion();
        AudioManager.instance.InitializeSong(FMODEvents.instance.song);
        musicEventInstance = AudioManager.instance.GetMusicEventInstance();
        musicEventInstance.setPitch(1);
        musicEventInstance.start();
    }

    void Update()
    {
        PLAYBACK_STATE state;
        musicEventInstance.getPlaybackState(out state);

        if (state == PLAYBACK_STATE.STOPPED && !RailMove.death)
        {
            finishObj.DOFade(1, 0.5f).OnComplete(() =>
            {
                SceneManager.LoadScene("FinishScore");
            });
        }
        if (Input.GetKeyDown(KeyCode.Space) && canChange)
        {
            ChangeInstrument();
            canChange = false;
            StartCoroutine(ChangeCooldown());
        }

        if (notesList == null || notesList.notes == null)
        {
            return;
        }

        int timelinePosition;
        musicEventInstance.getTimelinePosition(out timelinePosition);
        tiempoActual = timelinePosition / 1000f;

        for (int i = 0; i < notesList.notes.Count; i++)
        {
            if (!notasGeneradas.Contains(i) && notesList.notes[i].spawnTime <= tiempoActual)
            {
                GenerarNota(notesList.notes[i]);
                notasGeneradas.Add(i);
            }
        }
    }

    void CargarCancion()
    {
        NotesList fullList = JsonUtility.FromJson<NotesList>(charts[(int)instrument].text);
        notasGeneradas.Clear();

        if (fullList != null && fullList.notes != null)
        {
            List<Note> futuras = new();
            foreach (var n in fullList.notes)
            {
                if (n.spawnTime > tiempoActual)
                    futuras.Add(n);
            }
            notesList = new NotesList { notes = futuras };
            Debug.Log("Notas futuras cargadas: " + notesList.notes.Count);
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
        GameObject newNote = Instantiate(notePrefabs[noteId], posicionline.position, Quaternion.identity);
        newNote.GetComponent<NoteController>().materialId = (int)instrument;
    }


    void ChangeInstrument()
    {
        instrumentVControllers[(int)instrument].active = false;
        instrument = (Instrument)(((int)instrument + 1) % System.Enum.GetValues(typeof(Instrument)).Length);
        hitNotes.defaultMaterial = hitNotes.materials[(int)instrument];
        instrumentVControllers[(int)instrument].active = true;
        hitNotes.mRenderer.material = hitNotes.defaultMaterial;
        ChangeMastil();
        DeleteNotes();
        CargarCancion();
        Debug.Log("Instrumento actual: " + instrument);
    }

    public void ChangeMastil()
    {
        foreach (Renderer renderer in hitNotes.mastilRenderers)
        {
            renderer.material = hitNotes.mastilMaterial[(int)instrument];
        }
        foreach (GameObject ground in hitNotes.grounds)
        {
            ground.SetActive(false);
        }
        hitNotes.grounds[(int)instrument].SetActive(true);
    }

    private IEnumerator ChangeCooldown()
    {
        yield return new WaitForSeconds(changeCooldown);
        canChange = true;
    }

    public void DeleteNotes()
    {
        GameObject[] notes = GameObject.FindGameObjectsWithTag("Note");
        foreach (GameObject note in notes)
        {
            note.GetComponent<NoteController>().DestroyNote();
        }
    }
}
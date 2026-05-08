using UnityEngine;
using System.Collections.Generic;
using System.IO;
public class NoteTimer : MonoBehaviour
{
    public float time;
    public AudioSource audioSource; // Asigna el AudioSource que reproduce la canción
    public float delay = 2.3f; // Offset del spawnTime

    private List<Note> noteList = new();
    private bool saved = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        time = audioSource.time;
        float spawnTime = time - delay;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Note note = new() { spawnTime = Mathf.Round(spawnTime * 1000f) / 1000f, line = 1};
            noteList.Add(note);
            Debug.Log("Marcado: " + note.spawnTime);
        }

        if (!audioSource.isPlaying && !saved && time > 1f)
        {
            SaveChart();
            saved = true;
        }
    }
    
    void SaveChart()
    {
        string json = JsonHelper.ToJson(noteList.ToArray(), true);
        string path = Path.Combine(Application.dataPath, "chart.json");
        File.WriteAllText(path, json);
        Debug.Log("¡Chart guardado en: " + path + " con " + noteList.Count + " notas");
    }
}

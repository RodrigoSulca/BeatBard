using UnityEngine;
using System.Collections.Generic;
using System.IO;
using FMOD.Studio;
using UnityEngine.UI;
using TMPro;
using System.Collections;
public class NoteTimer : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Slider timeSlider;
    [SerializeField] private TMP_Text timeTxt;

    [Header("Config")]
    public float time;
    public float delay = 2.3f; // Offset del spawnTime
    [SerializeField] private CharterDemo charterDemo;
    private int songLength;
    private bool isDragging;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.5f);
        AudioManager.instance.InitializeSong(FMODEvents.instance.song);
        charterDemo.musicEventInstance.getDescription(out EventDescription desc);
        desc.getLength(out songLength);
        timeSlider.maxValue = songLength;
    }

    // Update is called once per frame
    void Update()
    {
        int timelinePosition;
        if (!isDragging)
        {
            charterDemo.musicEventInstance.getTimelinePosition(out timelinePosition);
            time = timelinePosition / 1000f;
            timeSlider.value = timelinePosition;

            float minutes = time / 60;
            float seconds = time % 60;

            // Mostrar formato MM:ss
            timeTxt.text = $"{minutes:00}:{seconds:00}";
        }
        

        float spawnTime = time - delay;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Note note = new() { spawnTime = Mathf.Round(spawnTime * 1000f) / 1000f, line = 1};
            charterDemo.notesList.Add(note);
            Debug.Log("Marcado: " + note.spawnTime);
        }
    }
    
    public void SaveChart()
    {
        string json = JsonHelper.ToJson(charterDemo.notesList.ToArray(), true);
        string path = Path.Combine(Application.dataPath, "chart.json");
        File.WriteAllText(path, json);
        Debug.Log("¡Chart guardado en: " + path + " con " + charterDemo.notesList.Count + " notas");
    }

    public void BeginDrag()
    {
        Debug.Log("Pointer down");
        isDragging = true;
    }

    public void EndDrag()
    {
        charterDemo.musicEventInstance.setTimelinePosition((int)timeSlider.value);
        isDragging = false;
    }
}

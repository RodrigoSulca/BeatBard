using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;
using TMPro;
using System;
public class EnemyCharter : MonoBehaviour
{
    public enum Type {Red, Blue, Green}
    public Type type;
    public int time;
    public TMP_Text timeTxt;
    public GameObject[] enemyPref;
    public List<Enemy> enemiesLists = new();
    private EventInstance musicEventInstance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AudioManager.instance.InitializeSong(FMODEvents.instance.song);
        musicEventInstance = AudioManager.instance.GetMusicEventInstance();
        musicEventInstance.start();
    }

    // Update is called once per frame
    void Update()
    {
        musicEventInstance.getTimelinePosition(out time);
        TimeSpan timeFormat = TimeSpan.FromMilliseconds(time);
        string tiempoFormateado = string.Format("{0:D2}:{1:D2}", timeFormat.Minutes, timeFormat.Seconds);
        timeTxt.text = tiempoFormateado;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(enemyPref[(int)type], transform.position, Quaternion.identity);
            Enemy enemy = new() { spawnTime = Mathf.Round(time * 1000f) / 1000f };
            enemiesLists.Add(enemy);
        }
    }
}

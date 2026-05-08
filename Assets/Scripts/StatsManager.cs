using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using FMOD.Studio;
public class StatsManager : MonoBehaviour
{
    public PlayerStats playerStats;
    [Header("UI")]
    public Image blackEffect;
    public TMP_Text scoreTxt;
    public TMP_Text failedNotesTxt;
    public TMP_Text hitNotesTxt;
    private EventInstance musicEventInstance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AudioManager.instance.InitializeSong(FMODEvents.instance.victory);
        musicEventInstance = AudioManager.instance.GetMusicEventInstance();
        musicEventInstance.start();

        scoreTxt.text += playerStats.score.ToString();
        failedNotesTxt.text += playerStats.failedNotes.ToString();
        hitNotesTxt.text += playerStats.hitNotes.ToString();
        blackEffect.DOFade(0, 0.5f).OnComplete(() =>
        {
            blackEffect.gameObject.SetActive(false);
        });;
    }

    public void ChangeScene(string sceneName)
    {
        AudioManager.instance.GetMusicEventInstance().stop(STOP_MODE.IMMEDIATE);
        SceneManager.LoadScene(sceneName);
    }
}

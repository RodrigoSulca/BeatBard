using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StatsManager : MonoBehaviour
{
    public PlayerStats playerStats;
    [Header("UI")]
    public TMP_Text scoreTxt;
    public TMP_Text failedNotesTxt;
    public TMP_Text hitNotesTxt;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scoreTxt.text += playerStats.score.ToString();
        failedNotesTxt.text += playerStats.failedNotes.ToString();
        hitNotesTxt.text += playerStats.hitNotes.ToString();
    }

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}

using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StatsManager : MonoBehaviour
{
    public PlayerStats playerStats;
    [Header("UI")]
    public Image blackEffect;
    public TMP_Text scoreTxt;
    public TMP_Text failedNotesTxt;
    public TMP_Text hitNotesTxt;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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
        SceneManager.LoadScene(sceneName);
    }
}

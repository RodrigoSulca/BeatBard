using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class MultiplierController : MonoBehaviour
{
    public int actualMult;
    public int cantNotes;
    public int actualNotes;
    public int totalPoints;
    public Slider hpSlider;
    private int initCantNotes;
    public Sprite[] multNums;
    public GameObject[] multBars;
    public SpriteRenderer multSpriteR;
    public TMP_Text multiplierTxt;
    public TMP_Text pointsTxt;
    private ComboRewards comboRewards;
    public NoteFeedback feedback;
    public HitNotes hitNotes;
    public BeatFlash beatFlash;
    public Animator multAnimator;
    public PlayerStats playerStats;

    void Start()
    {
        comboRewards = GetComponent<ComboRewards>();
        initCantNotes = cantNotes;
        playerStats.failedNotes = 0;
        playerStats.hitNotes = 0;
    }

    // Update is called once per frame
    void Update()
    {
        multAnimator.SetInteger("mult", actualMult);
        CheckMult();
        multiplierTxt.text = $"x{actualMult}";
        pointsTxt.text = totalPoints.ToString();
        playerStats.score = totalPoints;
    }

    void CheckMult()
    {
        if (actualMult < 4)
        {
            switch (actualNotes)
            {
                case 1:
                    multBars[0].SetActive(true);
                    break;

                case 2:
                    multBars[1].SetActive(true);
                    break;

                case 3:
                    multBars[2].SetActive(true);
                    break;

                case 4:
                    multBars[3].SetActive(true);
                    break;

                case 5:
                    actualNotes = 0;
                    actualMult++;
                    multSpriteR.sprite = multNums[actualMult - 1];
                    ResetMultBars();
                    break;
            }
        }
        else
        {
            multBars[0].SetActive(true);
            multBars[1].SetActive(true);
            multBars[2].SetActive(true);
            multBars[3].SetActive(true);
        }
    }

    public void FailNote()
    {
        ResetMultBars();
        comboRewards.ResetCombo();
        actualMult = 1;
        cantNotes = initCantNotes;
        AudioManager.instance.PlayOneShot(FMODEvents.instance.noteFailed, this.transform.position);
        if (feedback != null)
            feedback.ShowFeedback(false);

        if (beatFlash != null)
            beatFlash.Flash();
    }

    private void ResetMultBars()
    {
        foreach (GameObject multBar in multBars)
        {
            multBar.SetActive(false);
        }
        actualNotes = 0;
    }
}

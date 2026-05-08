using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;

public class BeatController : MonoBehaviour
{
    public Color flashColor;
    public float beatDuration;
    public float interval;
    public NotesGenerator notesGenerator;
    private Image img;

    void Awake()
    {
        img = GetComponent<Image>();
    }
    void Start()
    {
        StartCoroutine(FlashCoroutime());
    }

    private IEnumerator FlashCoroutime()
    {
        img.color = flashColor;
        notesGenerator.onRhythm = true;
        img.DOFade(0, beatDuration).From(1).SetEase(Ease.OutQuad).OnComplete(() =>
        {
            notesGenerator.onRhythm = false;
        });
        yield return new WaitForSeconds(interval);
        StartCoroutine(FlashCoroutime());
    }
}

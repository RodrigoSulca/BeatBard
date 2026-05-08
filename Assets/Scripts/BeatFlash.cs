using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;

public class BeatFlash : MonoBehaviour
{
    public Color flashColor;
    private Image img;

    void Awake()
    {
        img = GetComponent<Image>();
    }
    public void Flash()
    {
        img.color = flashColor;
        img.DOFade(0, 0.3f).From(1).SetEase(Ease.OutQuad);
    }
}

using System.Collections;
using UnityEngine;
using DG.Tweening;
using System;

[RequireComponent(typeof(MeshRenderer))]
public class HoldNote : NoteController
{
    public float holdTime;
    [SerializeField] private bool holding;
    [SerializeField] private Transform holdbar;
    private MeshRenderer meshRenderer;
    private bool completed = false;
    

    public override void Start()
    {
        base.Start();
        meshRenderer = GetComponent<MeshRenderer>();
        SetBarLength();
    }
    public override void PlayNote()
    {
        if (!holding)
        {
            StartCoroutine(HoldRoutine());
            meshRenderer.enabled = false;
        }
    }

    public override void MoveDown()
    {
        rb.DOMoveZ(endPoint.transform.position.z - holdTime*speed, speed).SetEase(Ease.Linear).SetSpeedBased().OnComplete(() =>
        {
            /*if (gameObject.CompareTag("Note"))
            {
                multiplierController.FailNote();
                hitNotes.NoteText("Miss");
                playerStats.failedNotes++;
            }*/
            Destroy(gameObject);
        });
    }

    IEnumerator HoldRoutine()
    {
        holding = true;

        float timer = 0f;

        // aqui puedes activar efectos visuales del rayo
        Debug.Log("Hold Started");

        while (timer < holdTime)
        {
            // Si el jugador suelta la tecla
            if (!Input.GetKey(KeyCode.L))
            {
                FailHold();
                yield break;
            }

            timer += Time.deltaTime;
            multiplierController.totalPoints += 1;

            yield return null;
        }

        CompleteHold();
    }

    void CompleteHold()
    {
        if (completed) return;

        completed = true;

        int finalPoints = points * multiplierController.actualMult;

        multiplierController.totalPoints += finalPoints;
        multiplierController.hpSlider.value += finalPoints / 10;

        Debug.Log("Hold Complete");

        DestroyNote();
    }

    void FailHold()
    {
        Debug.Log("Hold Failed");

        // aqui puedes romper combo o quitar hp

        DestroyNote();
    }

    void CompleteNote()
    {
        int finalPoints = points * multiplierController.actualMult;

        multiplierController.totalPoints += finalPoints;
        multiplierController.hpSlider.value += finalPoints / 10;

        DestroyNote();
    }

    void SetBarLength()
    {
        holdbar.localScale = new Vector3(
            1,
            1,
            holdbar.localScale.z + holdTime * speed
        );
    }
}

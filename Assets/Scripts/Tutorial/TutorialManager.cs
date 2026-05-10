using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class TutorialManager : MonoBehaviour
{
    [Serializable]
    public class TutorialStep
    {
        public string message;
        public float duration;
        public float g_duration;
    }

    [SerializeField] private TextAsset tutorialCSV;
    [SerializeField] private TMP_Text tutorialTxt;
    [SerializeField] private CanvasGroup tutorialGroup;

    [SerializeField] private List<TutorialStep> steps = new();
    [SerializeField] private NotesGenerator notesGenerator;

    private int currentStep = 0;
    private bool playerCompletedAction = false;

    void Start()
    {
        LoadCSV();
        StartCoroutine(RunTutorial());
    }

    void LoadCSV()
    {
        string[] lines = tutorialCSV.text.Split('\n');

        // Saltar header
        for (int i = 1; i < lines.Length; i++)
        {
            string line = lines[i].Trim();

            if (string.IsNullOrEmpty(line))
                continue;

            string[] values = line.Split(';');

            TutorialStep step = new TutorialStep
            {
                message = values[0],
                duration = float.Parse(values[1]),
                g_duration = float.Parse(values[2])
            };

            steps.Add(step);
        }
    }

    IEnumerator RunTutorial()
    {
        while (currentStep < steps.Count)
        {
            TutorialStep step = steps[currentStep];

            Debug.Log(step.message);
            tutorialTxt.text = step.message;

            if (step.g_duration > 0)
            {
                tutorialGroup.DOFade(1,0.5f);
                yield return new WaitForSeconds(step.duration);
                tutorialGroup.DOFade(0,0.5f);
                playerCompletedAction = false;
                notesGenerator.noteId = 0;
                yield return new WaitForSeconds(step.g_duration);
            }
            else
            {
                notesGenerator.noteId = 1;
                tutorialGroup.DOFade(1,0.5f);
                yield return new WaitForSeconds(step.duration);
            }

            currentStep++;
        }

        Debug.Log("Tutorial terminado");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerCompletedAction = true;
        }
    }
}
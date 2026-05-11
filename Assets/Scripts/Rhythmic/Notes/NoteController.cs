using UnityEngine;
using DG.Tweening;
[RequireComponent(typeof(MeshRenderer))]
public class NoteController : MonoBehaviour
{
    public float speed;
    public int points;
    public int materialId;
    [SerializeField] private Material[] materials;
    public PlayerStats playerStats;
    [HideInInspector] public HitNotes hitNotes;
    [HideInInspector] public GameObject endPoint;
    [HideInInspector] public MultiplierController multiplierController;
    [HideInInspector] public Rigidbody rb;
    public virtual void Start()
    {
        GetComponent<MeshRenderer>().material = materials[materialId];
        endPoint = GameObject.FindWithTag("EndPoint");
        multiplierController = GameObject.FindWithTag("MultiplierM").GetComponent<MultiplierController>();
        hitNotes = GameObject.FindWithTag("NoteHitter").GetComponent<HitNotes>();
        rb = GetComponent<Rigidbody>();
        MoveDown();

    }

    public virtual void MoveDown()
    {
        rb.DOMoveZ(endPoint.transform.position.z, speed).SetEase(Ease.Linear).SetSpeedBased().OnComplete(() =>
        {
            if (gameObject.CompareTag("Note"))
            {
                multiplierController.FailNote();
                hitNotes.NoteText("Miss");
                playerStats.failedNotes++;
            }
            Destroy(gameObject);
        });
    }

    public virtual void PlayNote()
    {
        int finalPoints = points * multiplierController.actualMult;
        multiplierController.totalPoints += finalPoints;
        multiplierController.hpSlider.value += finalPoints / 10;
        DestroyNote();
    }

    public void DestroyNote()
    {
        rb.DOKill();
        Destroy(gameObject);
    }
}

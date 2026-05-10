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
    private HitNotes hitNotes;
    private GameObject endPoint;
    private MultiplierController multiplierController;
    private Rigidbody rb;
    void Start()
    {
        GetComponent<MeshRenderer>().material = materials[materialId];
        endPoint = GameObject.FindWithTag("EndPoint");
        multiplierController = GameObject.FindWithTag("MultiplierM").GetComponent<MultiplierController>();
        hitNotes = GameObject.FindWithTag("NoteHitter").GetComponent<HitNotes>();
        MoveDown();

    }

    void MoveDown()
    {
        rb = GetComponent<Rigidbody>();
        rb.DOMoveZ(endPoint.transform.position.z, speed).SetEase(Ease.Linear).OnComplete(() =>
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

    public void PlayNote()
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

using UnityEngine;
using DG.Tweening;
public class NoteController : MonoBehaviour
{
    public float speed;
    public int points;
    private HitNotes hitNotes;
    private GameObject endPoint;
    private MultiplierController multiplierController;
    private Rigidbody rb;
    void Start()
    {
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
            }
            Destroy(gameObject);
        });
    }

    public void PlayNote()
    {
        int finalPoints = points * multiplierController.actualMult;
        multiplierController.totalPoints += finalPoints;
        multiplierController.hpSlider.value += finalPoints / 10;
        rb.DOKill();
        Destroy(gameObject);
    }

    public void DestroyNote()
    {
        rb.DOKill();
        Destroy(gameObject);
    }
}

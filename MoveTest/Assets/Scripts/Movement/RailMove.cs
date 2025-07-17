using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
public class RailMove : MonoBehaviour
{
    public GameObject[] rails;
    public GameObject[] models;
    public float moveTime;
    public bool onHorse;
    public bool armor;
    [Header("Death")]
    public Image dmgPanel;
    public Color dmgColor;
    public GameObject deathBg;
    public GameObject deathPanel;
    private int railIndex;
    private bool isMoving;
    private Rigidbody rb;
    private BoxCollider playerColl;
    private RawImage deathImgBg;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerColl = GetComponent<BoxCollider>();
        deathImgBg = deathBg.GetComponent<RawImage>();
        railIndex++;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        if (Input.GetKeyDown(KeyCode.A) && !isMoving && railIndex > 0)
        {
            railIndex--;
            isMoving = true;
            rb.DOMoveX(rails[railIndex].transform.position.x, moveTime).SetEase(Ease.Linear).OnComplete(() =>
            {
                isMoving = false;
            });
        }
        else if (Input.GetKeyDown(KeyCode.D) && !isMoving && railIndex < rails.Length - 1)
        {
            railIndex++;
            isMoving = true;
            rb.DOMoveX(rails[railIndex].transform.position.x, moveTime).SetEase(Ease.Linear).OnComplete(() =>
            {
                isMoving = false;
            });
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && onHorse && !armor)
        {
            
            moveTime = 0.2f;
            DmgFlash();
            onHorse = false;
            other.transform.DOKill();
            Destroy(other.gameObject);
            ChangeModel();
        }
        else if (other.CompareTag("Enemy") && onHorse && armor)
        {
            Debug.Log("Armadura perdida");
            armor = false;
            DmgFlash();
            other.transform.DOKill();
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Enemy") && !onHorse)
        {
            other.transform.DOKill();
            Destroy(other.gameObject);
            Death();

        }
    }

    private void DmgFlash()
    {
        AudioManager.instance.PlayOneShot(FMODEvents.instance.impactPlayer, this.transform.position);
        dmgPanel.color = dmgColor;
        dmgPanel.DOFade(0, 0.3f).From(1).SetEase(Ease.OutQuad);
    }

    public void Death()
    {
        Debug.Log("PlayerDeath");
        AudioManager.instance.GetMusicEventInstance().setPitch(0.68f);
        deathBg.SetActive(true);
        deathImgBg.DOFade(1, 0.5f).OnComplete(() =>
        {
            AudioManager.instance.PlayOneShot(FMODEvents.instance.gameOver, this.transform.position);
            deathPanel.SetActive(true);
            Time.timeScale = 0;
        });
    }

    private void ChangeModel()
    {
        models[0].SetActive(false);
        models[1].SetActive(true);
        playerColl.size = new Vector3(playerColl.size.x, playerColl.size.y, 0.6452804f);
        playerColl.center = new Vector3(playerColl.center.x, playerColl.center.y, 0.3253124f);
    }
}

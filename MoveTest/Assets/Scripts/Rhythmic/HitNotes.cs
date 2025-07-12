using TMPro;
using UnityEngine;
public class HitNotes : MonoBehaviour
{
    public KeyCode inputKey;
    public Material activeMaterial;
    public ComboRewards comboRewards;

    public Material[] materials;
    public Material[] mastilMaterial;
    public Renderer[] mastilRenderers;
    public GameObject textPrefab;
    public Transform textSpawner;
    public ParticleSystem hitParticle;
    public PlayerStats playerStats;

    private bool active;
    [HideInInspector] public Renderer mRenderer;
    [HideInInspector] public Material defaultMaterial;

    void Start()
    {
        mRenderer = GetComponent<Renderer>();
        defaultMaterial = mRenderer.material;
        
    }

    void Update()
    {
        if (Input.GetKeyDown(inputKey))
        {
            mRenderer.material = activeMaterial;
            active = true;
        }
        else if (Input.GetKeyUp(inputKey))
        {
            mRenderer.material = defaultMaterial;
            active = false;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (active && other.CompareTag("Note"))
        {
            hitParticle.Play();
            comboRewards.actualCombo++;
            other.GetComponent<NoteController>().PlayNote();
            AudioManager.instance.PlayOneShot(FMODEvents.instance.playNote, this.transform.position);
            NoteText("Hit!");
            active = false;
            playerStats.hitNotes++;
        }
    }
    public void NoteText(string text)
    {
        GameObject noteTxt = Instantiate(textPrefab, textSpawner.position, textPrefab.transform.rotation, transform);
        noteTxt.GetComponent<TMP_Text>().text = text;
        Destroy(noteTxt, 0.5f);
    }
}

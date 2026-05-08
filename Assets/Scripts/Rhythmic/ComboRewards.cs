using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ComboRewards : MonoBehaviour
{
    public int comboNeeded;
    public GameObject player;
    public float invulnerableTime;
    public Material invulnerableM;
    public int actualCombo;
    public Slider hpSlider;
    public NotesGenerator notesGenerator;
    private int initComboN;
    private Collider playerColl;
    private Renderer playerRenderer;
    private Material normalMaterial;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerColl = player.GetComponent<Collider>();
        playerRenderer = player.GetComponent<Renderer>();
        initComboN = comboNeeded;
        normalMaterial = playerRenderer.material;
    }

    // Update is called once per frame
    void Update()
    {
        if (actualCombo >= comboNeeded)
        {
            Reward();
        }
    }


    private void Reward()
    {
        comboNeeded += initComboN;
        switch ((int)notesGenerator.instrument)
        {
            case 0:
                hpSlider.value += 50;
                AudioManager.instance.PlayOneShot(FMODEvents.instance.healthPlayer, this.transform.position);
                break;
            case 1:
                HorseArmor();
                Debug.Log("Bufo Armor");
                break;
            case 2:
                Debug.Log("Bufo Inv");
                AudioManager.instance.PlayOneShot(FMODEvents.instance.Invulnerable, this.transform.position);
                StartCoroutine(Invulnerable());
                break;

        }
    }

    private IEnumerator Invulnerable()
    {
        playerColl.enabled = false;
        playerRenderer.material = invulnerableM;
        yield return new WaitForSeconds(invulnerableTime);
        playerColl.enabled = true;
        playerRenderer.material = normalMaterial;
    }

    private void HorseArmor()
    {
        Debug.Log("Horse Armor");
    }

    public void ResetCombo()
    {
        actualCombo = 0;
        comboNeeded = initComboN;
    }
}

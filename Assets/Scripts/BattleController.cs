using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BattleController : MonoBehaviour
{
    public Slider battleSlider;
    public GameObject gameOverPanel;
    public float enemyDmg;
    public NotesGenerator notesGenerator;
    public ComboRewards comboRewards;
    public RailMove railMove;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (battleSlider.value <= 0)
        {
            railMove.Death();
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        EnemyController enemy = collision.gameObject.GetComponent<EnemyController>();
        if (collision.gameObject.CompareTag("Enemy") && ((int)enemy.type != (int)notesGenerator.instrument || comboRewards.actualCombo <=0))
        {
            Debug.Log("Daño");
            battleSlider.value -= enemyDmg;
        }
    }

}

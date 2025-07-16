using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;

    public int currentHealth;

    public PlayerHealthBar healthbar;

    public Animator animator;
    

    // Start is called before the first frame update
    void Start()
    {
        healthbar.SetMaxHealth(maxHealth);
        currentHealth = maxHealth;
        animator.SetInteger("currentHealth", maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(20);
        }
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        animator.SetInteger("currentHealth", animator.GetInteger("currentHealth")-damage);

        healthbar.SetHealth(animator.GetInteger("currentHealth"));
    }
}

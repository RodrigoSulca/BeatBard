using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Health : MonoBehaviour
{
    public int maxHealth = 100;

    public int currentHealth;

    public Healthbar healthbar;

    public Animator animator;
    

    // Start is called before the first frame update
    void Start()
    {
        healthbar.SetMaxHealth(maxHealth);
        animator.SetInteger("currentHealth", maxHealth);
    }


    // Temporary solution for the damage action
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(20);
        }
    }

    // Code that deals with taking damage
    void TakeDamage(int damage)
    {
        animator.SetInteger("currentHealth", animator.GetInteger("currentHealth")-damage);

        healthbar.SetHealth(animator.GetInteger("currentHealth"));
    }
}

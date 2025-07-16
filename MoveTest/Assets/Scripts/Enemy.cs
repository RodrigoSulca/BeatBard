using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Animator animator;

    public EnemyHealthBar healthbar;

    public int maxHealth = 100;

    public int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
        healthbar.SetMaxHealth(maxHealth);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        healthbar.SetHealth(currentHealth);

        animator.SetTrigger("Hurt");

        // Play hurt animation
        if(currentHealth == 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Enemy died!");

        // Die animation
        animator.SetBool("IsDead", true);
        
        int LayerBackground = LayerMask.NameToLayer("Background");
        gameObject.layer = LayerBackground;
        Debug.Log("Current layer: " + gameObject.layer);        

        this.enabled = false;
    }

    void Update()
    {
        
    }
}

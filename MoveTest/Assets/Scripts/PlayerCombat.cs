using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Animator animator;

    public Transform attackPoint;

    public LayerMask enemyLayers;

    public float attackRange = 0.5f;

    public int attackDamage = 20;

    public float attackRate = 2f;

    float nextAttackTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var isJumping = animator.GetBool("IsJumping");
        var isCrouching = animator.GetBool("IsCrouching");
        //Debug.Log("Try Attack, isJump: " + isJumping + ", isCrouch: " + isCrouching);
        if (isJumping  == false && isCrouching == false)
        {

            if (Time.time >= nextAttackTime)
            {
                if (Input.GetButtonDown("Kick"))
                {
                    // Play an attack animation
                    animator.SetTrigger("Kick");

                    Attack();
                    nextAttackTime = Time.time + 1f / attackRate;
                    Debug.Log("Attack, isJump: " + isJumping + ", isCrouch: " + isCrouching);
                }
                if (Input.GetButtonDown("Punch"))
                {
                    // Play an attack animation
                    animator.SetTrigger("Punch");

                    Attack();
                    nextAttackTime = Time.time + 1f / attackRate;
                    Debug.Log("Attack, isJump: " + isJumping + ", isCrouch: " + isCrouching);
                }
            }
        }

     
    }

    void Attack()
    {

        // Detect enemies in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        // Damage them
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}

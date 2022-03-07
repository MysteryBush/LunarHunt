using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//MELEE COMBAT in Unity
//https://www.youtube.com/watch?v=sPiVz1k-fEs&ab_channel=Brackeys

public class PlayerCombat : MonoBehaviour
{
    public Animator anim;

    public Transform attackPoint;
    public LayerMask enemyLayers;

    //about playerControl
    //public bool isControl;
    //public bool doingAction = false;


    public int maxHealth = 100;
    public int currentHealth;

    public float attackRange = 0.5f;
    public int attackDamage = 40;

    public float attackRate = 2f;
    float nextAttackTime = 0f;

    public bool inputAttack = false;
    void Start()
    {
        currentHealth = maxHealth;
    }
    void Update()
    {
        if (GetComponent<PlayerControl>().isControl == true && GetComponent<PlayerControl>().controlUI == false)
        {
            if (Time.time >= nextAttackTime)
            {
                GetComponent<PlayerControl>().doingAction = false;
                //When attack time finished, able to move again
                if (Input.GetKeyDown(KeyCode.Mouse0))
                //if (inputAttack == true)
                {
                    inputAttack = false;
                    GetComponent<PlayerControl>().doingAction = true;
                    Attack();
                    nextAttackTime = Time.time + 1f / attackRate;
                }
            }
        }

        if (currentHealth <= 0 && anim.GetBool("isDead") == false)
        {
            Die();
        }

        if (currentHealth > 0 && anim.GetBool("isDead") == true)
        {
            Revive();
        }
    }

    void Attack()
    {
        // Play an attack animation
        anim.SetTrigger("Attack");

        // Detect enemies in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        // Damage them
        foreach (Collider2D enemy in hitEnemies)
        {
            //Debug.Log("We hit " + enemy.name);
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        // Play hurt animation
        anim.SetTrigger("Hurt");

        if (currentHealth <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        Debug.Log("Game Over!");

        anim.SetBool("isDead", true);
        //this.GetComponent<PlayerControl>().toggleControl();
        this.GetComponent<PlayerControl>().isDead = true;
        //GetComponent<Collider2D>().enabled = false;
        //this.enabled = false;
    }

    void Revive()
    {
        Debug.Log("Arise!");

        anim.SetBool("isDead", false);
        //this.GetComponent<PlayerControl>().toggleControl();
        this.GetComponent<PlayerControl>().isDead = false;
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyStats : MonoBehaviour
{
    public GameObject player;

    public float maxHealth = 10;
    public float damage = 1;
    public float health;

    public ParticleSystem enemyTakeDamagePS;
    public ParticleSystem hitPS;

    public Transform attackPosition;
    public LayerMask objectsHitLM;
    public float areaOfAttack;
    public float rangeOfAttack;
    public float attackCooldown;
    public float attackTimer;


    void Awake()
    {
        health = maxHealth;
        rangeOfAttack = (attackPosition.position - transform.position).magnitude;
    }

    // Update is called once per frame
    void Update()
    {
        if(attackTimer <= 0)
        {
            if ((player.transform.position - transform.position).magnitude <= rangeOfAttack + areaOfAttack)
            {
                attackTimer = attackCooldown;
                meleeAttack();
            }

        }
        else
        {
            attackTimer -= Time.deltaTime;
        }
    }


    public void takeDamage(float damageTaken)
    {
        health -= damageTaken;
        enemyTakeDamagePS.Play();
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }

    private void meleeAttack()
    {
        hitPS.Play();

        Collider2D[] objectsHit = Physics2D.OverlapCircleAll(attackPosition.position, areaOfAttack, objectsHitLM);
        foreach (Collider2D hit in objectsHit)
        {
            hit.GetComponent<playerStats>().takeDamage(damage);
        }
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPosition.position, areaOfAttack);
    }
}

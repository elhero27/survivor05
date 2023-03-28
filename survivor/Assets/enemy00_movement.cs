using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy00_movement : MonoBehaviour
{

    public Vector3 targetPosition;
    public Vector3 directionVector3;
    public float movespeed;
    public float health;
    public float damage;
    public float experienceValue;
    public logicManagerScript logic;

    // Start is called before the first frame update
    void Start()
    {
        movespeed = 4.5f;
        health = 5f;
        damage = 0.5f;
        experienceValue = 1;
        logic = GameObject.FindGameObjectWithTag("LogicManager").GetComponent<logicManagerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        // draw Vector to Position of player and move in that direction with "movespeed"
        targetPosition = GameObject.FindWithTag("Player").transform.position;
        directionVector3 = targetPosition - transform.position;
        transform.position = transform.position + directionVector3.normalized * movespeed * Time.deltaTime;

    }


    public void takeDamage(float damage)
    {
        health -= damage;
    
        if (health <= 0)
        {
            Destroy(gameObject);
            logic.addKillCount();

            if (GameObject.FindWithTag("Player").TryGetComponent<player_movement>(out player_movement player))
            {
                player.addExperience(experienceValue);
            }
        }

    }


    public float getDamage()
    {
        return damage;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Collision with "Projectile" -> destroy enemy
        if (collision.gameObject.tag == "Projectile")
        {
        }
    }

    public void setAttributes(float movespeedIn, float healthIn, float damageIn, float expIn)
    {
        movespeed = movespeedIn;
        health = healthIn;
        damage = damageIn;
        experienceValue = expIn;
    }

}

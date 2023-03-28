using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy00_movement : MonoBehaviour
{

    public Vector3 targetPosition;
    public Vector3 directionVector3;
    public float movespeed;
    public float health;
    public float maxHealth;
    public float damage;
    public float experienceValue;
    public logicManagerScript logic;
    public healthbarBehaviour healthbar;


    // Start is called before the first frame update
    void Start()
    {
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
        else
        {
            healthbar.setHealth(maxHealth, health);
        }

    }


    public float getDamage()
    {
        return damage;
    }


    public void setAttributes(float movespeedIn, float healthIn, float damageIn, float expIn)
    {
        movespeed = movespeedIn;
        maxHealth = healthIn;
        health = maxHealth;
        damage = damageIn;
        experienceValue = expIn;
        healthbar.setHealth(maxHealth, health);
    }

}

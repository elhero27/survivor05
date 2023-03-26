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

    // Start is called before the first frame update
    void Start()
    {
        movespeed = 3f;
        health = 1f;
        damage = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        // draw Vector to Position of player and move in that direction with "movespeed"
        targetPosition = GameObject.Find("player").transform.position;
        directionVector3 = targetPosition - transform.position;
        transform.position = transform.position + directionVector3.normalized * movespeed * Time.deltaTime;



    }


    public void takeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Collision with "Projectile" -> destroy enemy
        if (collision.gameObject.tag == "Projectile")
        {
        }
    }

}

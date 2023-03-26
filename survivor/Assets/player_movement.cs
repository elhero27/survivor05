using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_movement : MonoBehaviour
{

    public float movespeed;
    SpriteRenderer sprite;
    Color baseColor;
    bool waitOn;
    float counterWait;
    public GameObject projectilePrefab;
    public float shotSpeed;
    public float shotCooldown;
    public float shotTimer;
    public GameObject closestEnemy;

    // Start is called before the first frame update
    void Start()
    {
        movespeed = 10;
        shotSpeed = 5f;
        shotCooldown = 10f;
        shotTimer = 0f;
        sprite = GetComponent<SpriteRenderer>();
        baseColor = sprite.color;
        waitOn = false;
        counterWait = 0;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.W) == true)
        {
            transform.position = transform.position + (Vector3.up * movespeed) * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.S) == true)
        {
            transform.position = transform.position + (Vector3.down * movespeed) * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.D) == true)
        {
            transform.position = transform.position + (Vector3.right * movespeed) * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.A) == true)
        {
            transform.position = transform.position + (Vector3.left * movespeed) * Time.deltaTime;
        }


        // Change color back after being hit
        if (waitOn)
        {
            if (counterWait <= 0)
            {
                sprite.color = baseColor;
                waitOn = false;
            }
            else
            {
                counterWait -= Time.deltaTime;
            }

        }


        // NOT WORKING
        // Shoot projectile at fixed intervals and deactivate collision between player and projectile
        if (shotTimer < shotCooldown)
        {
            shotTimer += shotSpeed * Time.deltaTime;
        }
        else
        {
            // Shoot only when enemies are present
            closestEnemy = FindClosestEnemy();
            if (closestEnemy != null)
            {
                shotTimer = 0f;
                GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            }

            
            //Physics.IgnoreCollision(GetComponent<Collider>(), projectile.GetComponent<Collider>());
            //Physics.IgnoreCollision(GetComponent<Collider>(), Instantiate(projectilePrefab, transform.position, Quaternion.identity).GetComponent<Collider>());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Player hits enemy -> blink
        if (collision.transform.gameObject.tag == "Enemy")
        {
            sprite.color = new Color(1, 0, 0, 1);
            waitOn = true;
            counterWait = 0.3f;
        }


        if (collision.transform.gameObject.tag == "Projectile")
        {
        }

    }

    public GameObject FindClosestEnemy()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }
}

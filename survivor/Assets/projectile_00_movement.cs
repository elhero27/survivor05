using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile_00_movement : MonoBehaviour
{
    public Vector3 direction;
    public float movespeed = 5.5f;
    public float damageMultiplier = 1f;
    public float damage = 1f;


    // Start is called before the first frame update
    void Start()
    {
        // direction: last position of closest enemy
        direction = FindClosestEnemy().transform.position - transform.position;
        damageMultiplier = 1;
        damage = 1;
        
    }

    // Update is called once per frame
    void Update()
    {
        // move in direction with movespeed
        transform.position = transform.position + direction.normalized * movespeed * Time.deltaTime;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Collision with "Enemy" -> destroy this projectile
        if (collision.gameObject.TryGetComponent<enemy00_movement>(out enemy00_movement enemy))
        {
            enemy.takeDamage(damage);
            Destroy(gameObject);
        }


        // Collision with "Wall" -> destroy this projectile
        if (collision.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }

    }


    public float getDamageMultiplier()
    {
        return damageMultiplier;
    }


    public void setDamage(float newDamage)
    {
        damage = newDamage;
    }

    // Search for closest gameObject with Tag "Enemy" and return it
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

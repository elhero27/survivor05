using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillshotProjectile : MonoBehaviour
{

    public Vector3 direction;
    public float movespeed = 20f;
    public float damageMultiplier = 1;
    public float penetration = 1;
    public float damage;
    public Rigidbody2D rb;
    public GameObject parent;
    public Transform arrow;


    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // move in direction with movespeed
        //transform.position = transform.position + direction.normalized * movespeed * Time.deltaTime;
        rb.velocity = direction.normalized * movespeed;
        //rb.rotation = arrow.GetComponent<SpriteRenderer>().transform.rotation.z;
        transform.rotation = arrow.GetComponent<SpriteRenderer>().transform.rotation;
        Debug.Log("Velocity : " + rb.velocity.ToString());
        Debug.Log("Direction : " + direction.normalized.ToString());
        Debug.Log("rotation : " + rb.rotation.ToString());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Collision with "Enemy" -> destroy this projectile
        if (collision.gameObject.TryGetComponent<enemy00_movement>(out enemy00_movement enemy))
        {
            enemy.takeDamage(damage);
            penetration -= 1;
            if (penetration <= 0)
            {
                Destroy(gameObject);
            }
        }


        // Collision with "Wall" -> destroy this projectile
        else if (collision.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }

    }


    public void setDirection(Vector3 directionInput){ direction = directionInput; }
    public void setMovespeed(float movespeedInput) { movespeed = movespeedInput; }
    public void setDamageMultiplier(float damageMultiplierInput) { damageMultiplier = damageMultiplierInput; }
    public void setPenetration(float penetrationInput) { penetration = penetrationInput; }
    public void setDamage(float damageInput) { damage = damageInput; }
    public void setParent(GameObject parentInput) { parent = parentInput; }

}
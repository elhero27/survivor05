using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy00_movement : MonoBehaviour
{

    public Vector3 targetPosition;
    public Vector3 directionVector3;
    public float movespeed = 1;

    // Start is called before the first frame update
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        targetPosition = GameObject.Find("player").transform.position;
        directionVector3 = targetPosition - transform.position;
        transform.position = transform.position + directionVector3.normalized * movespeed * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.gameObject.tag == "Projectile")
        {
            Destroy(gameObject);
        }
    }

}

/*
    private SpriteRenderer renderer;

renderer = Component.renderer;
renderer.color = Color.blue;
*/
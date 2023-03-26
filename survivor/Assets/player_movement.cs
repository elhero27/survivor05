using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_movement : MonoBehaviour
{

    public float movespeed = 10;
    SpriteRenderer sprite;
    Color baseColor;
    bool waitOn;
    float counterWait;
    public 

    // Start is called before the first frame update
    void Start()
    {
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

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        sprite.color = new Color(1, 0, 0, 1);
        waitOn = true;
        counterWait = 0.3f;

    }
}

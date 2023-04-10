using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMovement : MonoBehaviour
{

    public float movespeed = 2;
    public Rigidbody2D enemyRB;
    public Vector3 direction;
    public GameObject player;
    public bool beingPushedBack;
    public float pushBackTime;
    public Vector2 pushBackDirection;

    // Start is called before the first frame update
    void Awake()
    {
        enemyRB = GetComponent<Rigidbody2D>();
        //enemyPS = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {

        if (beingPushedBack)
        {
            if(pushBackTime <= 0)
            {
                beingPushedBack = false;
                enemyRB.velocity = direction * movespeed;
            }
            else
            {
                pushBackTime -= Time.deltaTime;
            }
        }
        else
        {
            direction = (player.transform.position - transform.position).normalized;
            enemyRB.velocity = direction * movespeed;
            transform.up = player.transform.position - transform.position;
        }
    }

    public void pushBack(float pushBackSpeedIn, float pushBackTimeIn, Vector3 pushBackDirection)
    {
        //pushBackDirection = (Vector2)(pusher.position - transform.position).normalized;
        enemyRB.velocity = (Vector2)pushBackDirection * pushBackSpeedIn;
        beingPushedBack = true;
        pushBackTime = pushBackTimeIn;

    }

}

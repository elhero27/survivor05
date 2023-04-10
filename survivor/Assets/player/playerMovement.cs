using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{

    public Rigidbody2D playerRB;
    public float playerMovespeed = 10f;
    public Transform circle;

    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        circle.up = (mousePos - (Vector2)transform.position).normalized;

        Vector3 movementInput = Vector3.zero;
        if (Input.GetKey(KeyCode.W) == true)
        {
            movementInput += (Vector3.up) * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.S) == true)
        {
            movementInput += (Vector3.down) * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.D) == true)
        {
            movementInput += (Vector3.right) * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.A) == true)
        {
            movementInput += (Vector3.left) * Time.deltaTime;
        }
        //transform.position += movementInput;
        playerRB.velocity = movementInput.normalized * playerMovespeed;

    }

}

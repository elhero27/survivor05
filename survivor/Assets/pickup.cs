using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickup : MonoBehaviour
{
    private float healAmount;
    public player_movement PlayerMovement;
    public logicManagerScript logic;

    // Start is called before the first frame update
    void Start()
    {
        PlayerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<player_movement>();
        healAmount = 5f;
        logic = GameObject.FindGameObjectWithTag("LogicManager").GetComponent<logicManagerScript>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            PlayerMovement.heal(healAmount);
            Destroy(gameObject);
        }
    }
}

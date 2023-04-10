using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class DashSpell : Spell
{
    public float dashVelocity;

    public override void Activate(GameObject parent)
    {
        player_movement movement = parent.GetComponent<player_movement>();
        //Rigidbody2D rb = parent.GetComponent<Rigidbody2D>();

        //rb.velocity = movement.movementInput.normalized * dashVelocity;
        movement.movespeed = dashVelocity;
    }

    public override void BeginCooldown(GameObject parent)
    {
        player_movement movement = parent.GetComponent<player_movement>();
        movement.movespeed = movement.baseMovespeed;

    }
        

}

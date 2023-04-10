using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerStats : MonoBehaviour
{

    public float damage = 5;
    public float maxHealth = 20;
    public float health;
    public GameObject redSOD;

    // Start is called before the first frame update
    void Awake()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        // Change color back to normal
        var color = redSOD.GetComponent<Image>().color;
        color.a -= 5f * Time.deltaTime;
        redSOD.GetComponent<Image>().color = color;
    }


    public void takeDamage(float damageInput)
    {
        health -= damageInput;

        // turn screen red
        var color = redSOD.GetComponent<Image>().color;
        color.a = 0.8f;
        redSOD.GetComponent<Image>().color = color;

        // DIE
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}

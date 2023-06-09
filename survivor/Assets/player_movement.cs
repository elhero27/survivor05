using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_movement : MonoBehaviour
{

    public float movespeed;
    SpriteRenderer sprite;
    Color baseColor;
    public GameObject projectilePrefab;
    public GameObject projectilePrefab01;
    public float shotSpeed;
    public float shotCooldown;
    public float shotTimer;
    public float shotCooldown01;
    public float shotTimer01;
    public GameObject closestEnemy;
    public float experiencePoints;
    public float damage;
    public float health;
    public float maxHealth;
    public int level;
    public float expToNextLevel;
    public float vulnerableTimer;
    public bool vulnerable;

    public logicManagerScript logic;
    public healthbarBehaviourPlayer healthbar;
    public pauseManager pauseManager;
    public upgradeChoiceManager upgradeChoiceManager;

    public string[] playerAttributes;

    // Start is called before the first frame update
    void Start()
    {
        movespeed = 5;
        shotSpeed = 5f;
        shotCooldown = 10f;
        shotTimer = 0f;

        shotCooldown01 = 5f;
        shotTimer01 = 0f;

        sprite = GetComponent<SpriteRenderer>();
        baseColor = sprite.color;
        expToNextLevel = 5;
        experiencePoints = 0;
        level = 1;
        health = 10;
        maxHealth = 10;
        damage = 2;

        vulnerable = true;

        logic = GameObject.FindGameObjectWithTag("LogicManager").GetComponent<logicManagerScript>();
        logic.setPlayerHealth(maxHealth, health);

        playerAttributes = new string[] { "maxHealth", "shotSpeed", "damage", "movespeed" };

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
        if (!vulnerable)
        {
            if (vulnerableTimer <= 0)
            {
                sprite.color = baseColor;
                vulnerable = true;
            }
            else
            {
                vulnerableTimer -= Time.deltaTime;
            }

        }

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
                //projectile.transform.parent = gameObject.transform;

                if (projectile.gameObject.TryGetComponent<projectile_00_movement>(out projectile_00_movement proj))
                {
                    proj.setDamage(proj.getDamageMultiplier() * damage);
                }
            }
        }


        // Shoot projectile at fixed intervals and deactivate collision between player and projectile
        if (shotTimer01 < shotCooldown01)
        {
            shotTimer01 += shotSpeed * Time.deltaTime;
        }
        else
        {
            // Shoot only when enemies are present
            closestEnemy = FindClosestEnemy();
            if (closestEnemy != null)
            {
                shotTimer01 = 0f;
                GameObject projectile = Instantiate(projectilePrefab01, transform.position, Quaternion.identity);
                //projectile.transform.parent = gameObject.transform;

                if (projectile.gameObject.TryGetComponent<projectile_01_movement>(out projectile_01_movement proj))
                {
                    proj.setDamage(proj.getDamageMultiplier() * damage);
                }
            }
        }


    }


    public void addExperience(float experience)
    {
        experiencePoints += experience;
        if (experiencePoints >= expToNextLevel)
        {
            levelUp();
        }
    }


    public void levelUp()
    {
        level += 1;
        upgradeChoiceManager.pickUpgrade();
        expToNextLevel *= 1.25f;
        logic.increasePlayerLevel();

    }

    public void increaseStats(float maxHealthIn = 0, float shotSpeedIn = 0, float damageIn = 0, float movespeedIn = 0)
    {
        if (maxHealthIn != 0)
        {
            maxHealth += maxHealthIn;
            health += maxHealthIn;
            logic.setPlayerHealth(maxHealth, health);
            healthbar.setHealth(maxHealth, health);
        }
        if (shotSpeedIn != 0) { shotSpeed += shotSpeedIn; }
        if (damageIn != 0) { damage += damageIn; }
        if (movespeedIn != 0) { movespeed += movespeedIn; }
    }

    public void takeDamage(float damageInput)
    {

        if (vulnerable)
        {
            health -= damageInput;
            logic.changePlayerHealth(0, -damageInput);
            healthbar.setHealth(maxHealth, health);


            vulnerable = false;
            vulnerableTimer = 0.2f;
        }
        if (health <= 0)
        {
            die();
        }
    }

    public void heal(float healAmount)
    {
        health += healAmount;

        if (health >= maxHealth)
        {

            health = maxHealth;
        }

        logic.setPlayerHealth(maxHealth,health);
        healthbar.setHealth(maxHealth, health);
    }


    private void die()
    {
        logic.gameOver();
        movespeed = 0;
        shotSpeed = 0;

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Player hits enemy -> blink
        if (collision.gameObject.tag == "Enemy")
        {
            sprite.color = new Color(1, 0, 0, 1);
            if (collision.gameObject.TryGetComponent<enemy00_movement>(out enemy00_movement enemy))
            {

                takeDamage(enemy.getDamage());
            }
        }
    }


    private void OnCollisionStay2D(Collision2D collision)
    {
        // Player hits enemy -> blink
        if (collision.gameObject.tag == "Enemy")
        {
            sprite.color = new Color(1, 0, 0, 1);
            if (collision.gameObject.TryGetComponent<enemy00_movement>(out enemy00_movement enemy))
            {
                takeDamage(enemy.getDamage());
            }
        }
    }


    public string getPlayerAttribute(int index)
    {
        return playerAttributes[index];
    }

    public float getDamage()
    {
        return damage;
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

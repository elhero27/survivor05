using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class spellSkillShot : spellBase
{

    public float spellMovespeed;
    public float spellDamageMultiplier;
    public float spellDamage;
    public float spellPenetration;
    public float spellCooldown;
    public float spellRange;
    public float distanceTravelled;
    public GameObject spellTarget;

    // only for pushback spells
    public float spellPushBackSpeed;
    public float SpellPushBackTime;

    public Vector3 direction;
    public float rotation;
    public string spellName;


    public Rigidbody2D spellRB;

    void Awake()
    {
        spellRB = GetComponent<Rigidbody2D>();
        spellRB.rotation = rotation;
        distanceTravelled = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        spellRB.velocity = spellMovespeed * direction;
        distanceTravelled += spellRB.velocity.magnitude * Time.deltaTime;
        if (distanceTravelled > spellRange) { Destroy(gameObject); }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            if (other.gameObject.TryGetComponent<enemyStats>(out enemyStats enemyS))
            {
                enemyS.takeDamage(spellDamage);
            }

            if (other.gameObject.TryGetComponent<enemyMovement>(out enemyMovement enemyM))
            {
                enemyM.pushBack(spellPushBackSpeed, SpellPushBackTime, direction);
            }

            Destroy(gameObject);
        }
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            if (other.gameObject.TryGetComponent<enemyStats>(out enemyStats enemy))
            {
                enemy.takeDamage(spellDamage);
                spellPenetration -= 1;
                if (spellPenetration <= 0)
                {
                    Destroy(gameObject);
                }

            }
        }
    }

    public override void setDamage(float baseDamageIn)
    {
        spellDamage = spellDamageMultiplier * baseDamageIn;
    }


    public override void setNextTarget(GameObject targetIn = null)
    {
        if (spellTarget == null)
        {
            spellTarget = targetIn;
        }
        // Set direction to new target
        direction = spellTarget.GetComponent<playerMovement>().circle.up;
        spellRB.transform.up = direction;
    }


    public override float getSpellCooldown() { return spellCooldown; }
    public override void setSpellDirection(Vector3 directionInput) { direction = directionInput; }
    public override string getSpellName() { return spellName; }


    // Search for closest gameObject with Tag "Enemy" and return it
    //public GameObject FindClosestEnemy(GameObject parent)
    //{
    //    GameObject[] gos;
    //    gos = GameObject.FindGameObjectsWithTag("Enemy");
    //    GameObject closest = null;
    //    float distance = Mathf.Infinity;
    //    Vector3 position = parent.transform.position;
    //    foreach (GameObject go in gos)
    //    {
    //        Vector3 diff = go.transform.position - position;
    //        float curDistance = diff.sqrMagnitude;
    //        if (curDistance < distance)
    //        {
    //            closest = go;
    //            distance = curDistance;
    //        }
    //    }
    //    return closest;
    //}

}

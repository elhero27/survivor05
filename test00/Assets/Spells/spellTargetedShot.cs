using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class spellTargetedShot : spellBase
{
    public float spellMovespeed;
    public float spellDamageMultiplier;
    public float spellDamage;
    public float spellCooldown;
    public float spellPenetration;
    public GameObject spellTarget;

    // only for bouncy spells
    public GameObject spellPrefab;
    public List<GameObject> targetsAlreadyHit = new List<GameObject>();

    public Vector3 direction;
    public float rotation;
    public string spellName;


    public Rigidbody2D spellRB;

    void Awake()
    {
        targetsAlreadyHit.Clear();
        spellRB = GetComponent<Rigidbody2D>();
        spellRB.rotation = rotation;
        setNextTarget();
    }

    // Update is called once per frame
    void Update()
    {
        spellRB.velocity = spellMovespeed * direction;
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy" && !targetsAlreadyHit.Contains(other.gameObject))
        {
            if (other.gameObject.TryGetComponent<enemyStats>(out enemyStats enemy))
            {
                // add current enemy to objectsHit to exclude from future targets
                targetsAlreadyHit.Add(other.gameObject);
                enemy.takeDamage(spellDamage);

                // handle bounces
                spellPenetration -= 1;
                if (spellPenetration <= 0)
                {
                    destroySpell();
                }
                else
                {
                    setNextTarget();
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
        if(spellTarget == null)
        {
            spellTarget = FindClosestEnemy(gameObject);
        }
        else
        {
            spellTarget = FindClosestEnemyUnlike(transform, targetsAlreadyHit);
        }
        // only continue if targets are found
        if(spellTarget == null) 
        { 
            destroySpell(); 
        } 
        else
        {
            // Set direction to new target
            direction = (spellTarget.gameObject.transform.position - transform.position).normalized;
            spellRB.transform.up = direction;

        }

    }

    public override float getSpellCooldown() { return spellCooldown; }
    public override void setSpellDirection(Vector3 directionInput) { direction = directionInput; }
    public override string getSpellName() { return spellName; }

    // Search for closest gameObject with Tag "Enemy" and return it
    //public GameObject FindClosestEnemy(Transform parentPosition)
    //{
    //    GameObject[] gos;
    //    gos = GameObject.FindGameObjectsWithTag("Enemy");
    //    GameObject closest = null;
    //    float distance = Mathf.Infinity;
    //    Vector3 position = parentPosition.position;
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


    public GameObject FindClosestEnemyUnlike(Transform parentPosition, List<GameObject> excludeObjectList)
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = parentPosition.position;
        foreach (GameObject go in gos)
        {
            if (!excludeObjectList.Contains(go)) 
            { 
                Vector3 diff = go.transform.position - position;
                float curDistance = diff.sqrMagnitude;
                if (curDistance < distance)
                {
                    closest = go;
                    distance = curDistance;
                }
            }
        }
        return closest;
    }


    public void destroySpell()
    {
        Destroy(gameObject);
    }
}

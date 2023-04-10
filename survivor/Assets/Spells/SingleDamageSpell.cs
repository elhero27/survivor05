using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class SingleDamageSpell : Spell
{
    public GameObject projectilePrefab;
    public Vector3 direction;
    public float damageMultiplier;
    public float penetration;
    public float movespeed;

    public override void Activate(GameObject parent)
    {
        player_movement movement = parent.GetComponent<player_movement>();
        direction = FindClosestEnemy(parent).transform.position - parent.transform.position;
        GameObject projectileObj = Instantiate(projectilePrefab, parent.transform.position, Quaternion.identity);
        if (projectileObj.gameObject.TryGetComponent<SkillshotProjectile>(out SkillshotProjectile projectile))
        {
            projectile.setDirection(direction);
            projectile.setDamageMultiplier(damageMultiplier);
            projectile.setPenetration(penetration);
            projectile.setMovespeed(movespeed);
            projectile.setDamage(movement.damage * projectile.damageMultiplier);
            projectile.setParent(parent);
        }

        Debug.Log("Instantiated");
    }

    public override void BeginCooldown(GameObject parent)
    {
    }



    // Search for closest gameObject with Tag "Enemy" and return it
    public GameObject FindClosestEnemy(GameObject parent)
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = parent.transform.position;
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

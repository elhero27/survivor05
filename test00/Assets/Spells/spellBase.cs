using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class spellBase : MonoBehaviour
{

    public virtual void setDamage(float baseDamageIn) { }
    public virtual void setNextTarget(GameObject targetIn) { }

    public virtual float getSpellCooldown() { return 0f; }
    public virtual string getSpellName() { return ""; }
    public virtual void setSpellDirection(Vector3 directionInput) { }


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

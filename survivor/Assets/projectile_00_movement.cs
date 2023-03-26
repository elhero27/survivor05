using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile_00_movement : MonoBehaviour
{
    public Vector3 direction;
    public float movespeed = 5.5f;
    // Start is called before the first frame update
    void Start()
    {
        direction = FindClosestEnemy().transform.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + direction.normalized * movespeed * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
        }
        
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

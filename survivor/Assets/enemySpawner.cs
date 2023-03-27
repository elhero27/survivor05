using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnSpeed;
    public float spawnCooldown;
    public float spawnTimer;
    public bool spawnGroup;
    public int spawnGroupSize;
    public int spawnGroupCounter;
    public logicManagerScript logic;

    // Start is called before the first frame update
    void Start()
    {
        spawnSpeed = 1f;
        spawnCooldown = 5f;
        spawnTimer = 0f;
        spawnGroup = false;
        spawnGroupSize = 10;
        spawnGroupCounter = 0;

        logic = GameObject.FindGameObjectWithTag("LogicManager").GetComponent<logicManagerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnTimer < spawnCooldown)
        {
            spawnTimer += spawnSpeed * Time.deltaTime;
        }
        else
        {
            spawnTimer = 0f;
            spawnGroup = true;
            logic.increaseWaveNumber();
        }

        if (spawnGroup)
        {
            for(int k = 1; k < spawnGroupSize; k++)
            {
                Vector3 randPos;
                randPos = Random.insideUnitCircle;
                GameObject enemy = Instantiate(enemyPrefab, transform.position + randPos, Quaternion.identity);
            }
            spawnGroup = false;
            //if (spawnGroupCounter < spawnGroupSize)
            //{
            //    spawnGroupCounter++;
            //    GameObject enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            //}
            //else
            //{
            //    spawnGroupCounter = 0;
            //    spawnGroup = false;
            //}
        }
        
    }
}

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
    public logicManagerScript logic;
    public player_movement player;
    public int waveNumber;
    public float movespeedNewWave;
    public float healthNewWave;
    public float damageNewWave;
    public float expNewWave;
    public float baseEnemySpeed;
    public float baseEnemyHealth;
    public float baseEnemyDamage;
    public float baseEnemyExp;
    public int baseGroupSize;


    // Start is called before the first frame update
    void Start()
    {
        spawnSpeed = 1f;
        spawnCooldown = 30f;
        spawnTimer = 27f;
        spawnGroup = false;
        spawnGroupSize = 1;
        waveNumber = 0;


        baseEnemySpeed = 4;
        baseEnemyHealth = 30;
        baseEnemyDamage = 0.5f;
        baseEnemyExp = 5;
        baseGroupSize = 3;

        increaseWaveDifficulty(waveNumber);
        logic = GameObject.FindGameObjectWithTag("LogicManager").GetComponent<logicManagerScript>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<player_movement>();
    }

    // Update is called once per frame
    void Update()
    {

        if (spawnTimer < spawnCooldown)
        {
            spawnTimer += spawnSpeed * Time.deltaTime;
            if (waveNumber > 0 && GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                spawnGroupOfEnemies();
                spawnTimer = 0f;
            }
        }
        else
        {
            spawnGroupOfEnemies();
            spawnTimer = 0f;
        }
        


    }


    private void spawnGroupOfEnemies()
    {
        increaseWaveDifficulty(waveNumber);
        waveNumber++;
        logic.increaseWaveNumber();
        for(int k = 1; k < spawnGroupSize; k++)
        {
            Vector3 randPos = new Vector3(0, 0, 0);
            Vector3 distToPlayer = new Vector3(0, 0, 0);

            while (distToPlayer.magnitude < 3)
            {
                randPos = Random.insideUnitCircle * 20;
                distToPlayer = transform.position + randPos - player.transform.position;
            }

            GameObject enemy = Instantiate(enemyPrefab, transform.position + randPos, Quaternion.identity);
            if (enemy.TryGetComponent<enemy00_movement>(out enemy00_movement enemy_00))
            {
                enemy_00.setAttributes(movespeedNewWave, healthNewWave, damageNewWave, expNewWave);
            }

        }
    }

    private void increaseWaveDifficulty(int waveNumberIn)
    {
        spawnGroupSize = baseGroupSize + waveNumberIn;

        movespeedNewWave = waveNumberIn * 0.05f + baseEnemySpeed;
        healthNewWave = waveNumberIn * 0.3f + baseEnemyHealth;
        damageNewWave = waveNumberIn * 0.5f + baseEnemyDamage;
        expNewWave = waveNumberIn * 0.7f + baseEnemyExp;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickUpSpawner : MonoBehaviour
{
    public GameObject pickupPrefab;
    Vector3 pos;
    private float SpawnTime;
    private float Timer;
    private float healAmount;
    public player_movement PlayerMovement;
    public logicManagerScript logic;

    // Start is called before the first frame update
    void Start()
    {
        Timer = 0f;
        SpawnTime = 10f;
        PlayerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<player_movement>();
    }

    // Update is called once per frame
    void Update()
    {
        Timer += Time.deltaTime;

        if (Timer >= SpawnTime)
        {
            pos = new Vector3(0, 0, 0);
            pos = Random.insideUnitCircle * 7;
            GameObject test = Instantiate(pickupPrefab, PlayerMovement.transform.position + pos, Quaternion.identity);
            Timer = 0f;
        }


    }
}

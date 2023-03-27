using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickup : MonoBehaviour
{
    public GameObject pickupPrefab;
    Vector3 pos;
    private float SpawnTime;
    private float Timer;
    private float healAmount;
    public logicManagerScript logic;

    // Start is called before the first frame update
    void Start()
    {
        Timer = 0f;
        SpawnTime = 10f;
        logic = GameObject.FindGameObjectWithTag("LogicManager").GetComponent<logicManagerScript>();
        healAmount = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        Timer += Time.deltaTime;

        if (Timer>=SpawnTime)
        {
            pos = new Vector3(0, 0, 0);
            pos= Random.insideUnitCircle * 20;
            Instantiate(pickupPrefab, pos, Quaternion.identity);
            Debug.Log("Heal Spawned");
            Timer = 0f;
        }

        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            logic.changePlayerHealth(0,healAmount);
            Destroy(gameObject);
        }
    }
}
